using System;
using System.Threading;
using AlienJust.Support.Concurrent.Contracts;
using CustomModbusSlave.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Common.CommandHearedTimer.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.CommandHearedTimer {
	public class CommandHearedTimerThreadSafe : ICommandHearedTimer, IStartStoppable {
		private readonly ISerialChannel _serialChannel;
		private readonly TimeSpan _timeout;
		private readonly IThreadNotifier _eventNotifier;

		private DateTime _lastTimeAnyCommandWasHeared;
		private readonly object _lastTimeAnyCommandWasHearedSyncObj;

		private Thread _checkThread;

		private bool _stopCheckThreadNeeded;
		private readonly object _stopCheckThreadNeededSyncObj;

		public CommandHearedTimerThreadSafe(ISerialChannel serialChannel, TimeSpan timeout, IThreadNotifier eventNotifier) {
			_serialChannel = serialChannel;
			_timeout = timeout;
			_eventNotifier = eventNotifier;

			_lastTimeAnyCommandWasHearedSyncObj = new object();
			_stopCheckThreadNeededSyncObj = new object();
			Start();
		}

		private void SerialChannelOnCommandHeared(ICommandPart commandPart) {
			LastTimeAnyCommandWasHeared = DateTime.Now;
		}

		private void CheckTime() {
			if (DateTime.Now - LastTimeAnyCommandWasHeared > _timeout) {
				_eventNotifier.Notify(()=>NoAnyCommandWasHearedTooLong?.Invoke());
			}
			else {
				_eventNotifier.Notify(()=>SomeCommandWasHeared?.Invoke());
			}
		}

		public event SimplestDelegateEver NoAnyCommandWasHearedTooLong;
		public event SimplestDelegateEver SomeCommandWasHeared;

		public void Start() {
			LastTimeAnyCommandWasHeared = DateTime.Now;

			_checkThread = new Thread(PeriodicCheck) { IsBackground = true, Name = "Поток периодической проверки времени последней регистрации команды", Priority = ThreadPriority.BelowNormal };
			_checkThread.Start();

			_serialChannel.CommandHeared += SerialChannelOnCommandHeared;
		}

		private void PeriodicCheck() {
			while (true) {
				Thread.Sleep((int)(_timeout.TotalMilliseconds/4));
				if (StopCheckThreadNeeded) return;

				CheckTime(); // twice per _timeout check

				Thread.Sleep((int)(_timeout.TotalMilliseconds / 4));
				if (StopCheckThreadNeeded) return;
			}
		}

		public void Stop() {
			_serialChannel.CommandHeared -= SerialChannelOnCommandHeared;

			StopCheckThreadNeeded = true;
			_checkThread.Join();
			_checkThread = null;
		}

		public DateTime LastTimeAnyCommandWasHeared {
			get { lock (_lastTimeAnyCommandWasHearedSyncObj) { return _lastTimeAnyCommandWasHeared; } }
			set {
				lock (_lastTimeAnyCommandWasHearedSyncObj) {
					_lastTimeAnyCommandWasHeared = value;
				}
				CheckTime();
			}
		}
		public bool StopCheckThreadNeeded {
			get {
				lock (_stopCheckThreadNeededSyncObj) {
					return _stopCheckThreadNeeded;
				}
			}
			set {
				lock (_stopCheckThreadNeededSyncObj) {
					_stopCheckThreadNeeded = value;
				}
			}
		}
	}
}
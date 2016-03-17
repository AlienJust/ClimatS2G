﻿using System;
using System.Collections.Generic;
using System.Threading;
using AlienJust.Support.Concurrent;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Text;
using CustomModbusSlave.Contracts;

namespace CustomModbusSlave {
	public class SerialChannel : ISerialChannel, ICommandPartFoundListener {
		public static readonly ILogger Logger = new RelayActionLogger(Console.WriteLine, new DateTimeFormatter(" > "));
		public event CommandHearedDelegate CommandHeared;
		public event CommandHearedWithReplyPossibilityDelegate CommandHearedWithReplyPossibility;

		private readonly SingleThreadedRelayQueueWorker<Action> _backgroundWorker;
		private readonly IWorker<Action> _readDataScheduler;
		private readonly IWorker<Action> _notifyWorker;
		private readonly SynchronizedCollection<byte> _incomingBuffer;

		private readonly ISerialPortContainer _portContainer;
		private readonly ISendAbility _sendAbility;
		private readonly ICommandPartSearcher _commandPartSearcher;
		
		private bool _dontStop;
		private readonly object _dontStopSync;

		public SerialChannel(ICommandPartSearcher commandPartSearcher, ISerialPortContainer portContainer, ISendAbility sendAbility) {
			_commandPartSearcher = commandPartSearcher;
			_portContainer = portContainer;
			_sendAbility = sendAbility;

			_dontStop = true;
			_dontStopSync = new object();

			_incomingBuffer = new SynchronizedCollection<byte>();


			_backgroundWorker = new SingleThreadedRelayQueueWorker<Action>("SerialBackWorker", a => a(), ThreadPriority.BelowNormal, true, null, new RelayActionLogger(Console.WriteLine, new DateTimeFormatter(" > ")));
			_readDataScheduler = new SingleThreadedRelayQueueWorker<Action>("SerialDataReadScheduler", a => a(), ThreadPriority.BelowNormal, true, null, new RelayActionLogger(Console.WriteLine, new DateTimeFormatter(" > ")));
			_notifyWorker = new SingleThreadedRelayQueueWorker<Action>("NotifyWorker", a => a(), ThreadPriority.BelowNormal, true, null, new RelayActionLogger(Console.WriteLine, new DateTimeFormatter(" > ")));

			ScheduleReadDataInBackground();
		}

		public void SelectPortAsync(string portName, int baudRate, Action<Exception> comPortOpenedCallbackAction)
		{
			Logger.Log("Closing port and opening new port async, args: portName=" + portName + ", baudRate=" + baudRate);
			_backgroundWorker.AddWork(() => {
				Exception exception;
				try {
					_portContainer.SelectPort(portName, baudRate);
					exception = null;
				}
				catch (Exception ex) {
					exception = ex;
				}
				if (comPortOpenedCallbackAction != null) {
					_notifyWorker.AddWork(() => comPortOpenedCallbackAction(exception));
				}
			});
		}

		public void CloseCurrentPortAsync(Action<Exception> comPortClosedCallbackAction) {
			Logger.Log("Closing port async");
			_backgroundWorker.AddWork(() => {
				Exception exception;
				try {
					ClosePortSyncUnsafe();
					exception = null;
				}
				catch (Exception ex) {
					exception = ex;
				}
				if (comPortClosedCallbackAction != null) {
					_notifyWorker.AddWork(() => comPortClosedCallbackAction(exception));
				}
			});
		}

		private void ClosePortSyncUnsafe() {
			_portContainer.CloseCurrentPort();
			_incomingBuffer.Clear();
		}

		private void ScheduleReadDataInBackground() {
			_readDataScheduler.AddWork(() => {
				while (true) {
					var waiter = new AutoResetEvent(false);



					_backgroundWorker.AddWork(() => {
						try {
							Logger.Log("Reading bytes from port (8 bytes, timeout 1 second)...");
							var bytes = _portContainer.ReadBytes(8, TimeSpan.FromSeconds(1));
							Logger.Log("Readed bytes from port: " + bytes.ToText());
							lock (_incomingBuffer.SyncRoot) {
								foreach (var b in bytes) {
									_incomingBuffer.Add(b);
								}
							}
							AnalyzeIncomingBuffer(); // analyzing in io thread to have possibility to reply
						}
						catch (Exception ex) {
							Logger.Log(ex);
						}
						finally {
							waiter.Set();
						}
					});



					// back to read data scheduler thread:
					waiter.WaitOne();

					Logger.Log("INCOMING BUFFER BYTES COUNT = " + _incomingBuffer.Count);

					lock (_dontStopSync) {
						if (!_dontStop) return;
					}
					//Thread.Sleep(50);
					lock (_dontStopSync) {
						if (!_dontStop) return;
					}
				}
			});
		}

		private void AnalyzeIncomingBuffer() {
			lock (_incomingBuffer.SyncRoot) {
				_commandPartSearcher.SearchForCommands(_incomingBuffer, this);
			}
		}

		public void CommandPartFound(ICommandPart commandPart) {
			// ultra fast synchronious notification with possibility to reply:
			var commandHearedFast = CommandHearedWithReplyPossibility;
			if (commandHearedFast != null) commandHearedFast.Invoke(commandPart, _sendAbility);

			// slow async notification
			_notifyWorker.AddWork(() => {
				try {
					var commandHeared = CommandHeared;
					{
						if (commandHeared != null) {
							commandHeared.Invoke(commandPart);
						}
					}
				}
				catch (Exception ex) {
					Logger.Log(ex);
				}
			});
		}

		public void StopWork() {
			_backgroundWorker.StopAsync();
			string result = "-----------------------------------------------------------------------------------------" + Environment.NewLine;
			lock (_incomingBuffer.SyncRoot) {
				for (int i = 0; i < _incomingBuffer.Count; ++i) {
					result += _incomingBuffer[i].ToString("X2") + " ";
					if (i%16 == 0) result += Environment.NewLine;
					else if (i%8 == 0) result += "    ";
					else if (i%4 == 0) result += "  ";
				}
			}
			Logger.Log(result);
			lock (_dontStopSync)
				_dontStop = true;

		}
	}
}
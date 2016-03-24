using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;

namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	class BsSmReplyDataViewModel : ViewModelBase, ICommandListener, IBsSmDataCommand32Reply {
		private readonly IThreadNotifier _notifier;

		private readonly BsSmStateViewModel _bsSmStateVm;
		
		private IBsSmDataCommand32Reply _reply;
		private string _replyText;

		public BsSmReplyDataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;
			_bsSmStateVm = new BsSmStateViewModel();
		}

		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr != 0x06) return;
			if (code == 0x20 && data.Count == 20) { // reply
				_notifier.Notify(() => {
					ReplyText = data.ToText();
					Reply = new BsSmDataCommand32ReplyBuilderFromReplyDataBytes(data).Build(); // TODO try, if catch - null, and request too
					_bsSmStateVm.Update(_reply.BsSmState);
				});
			}
		}

		public IBsSmDataCommand32Reply Reply {
			get { return _reply; }
			set {
				if (_reply != value) {
					_reply = value;
					RaisePropertyChanged(() => TargetTemperatureInsideTheCabin);

					RaisePropertyChanged(() => FanSpeedLevel);
					RaisePropertyChanged(() => IsWarmFloorOn);

					RaisePropertyChanged(() => AstronomicTime);
					RaisePropertyChanged(() => DelayedStartTime);
					
					RaisePropertyChanged(() => TemperatureOutdoor);

					RaisePropertyChanged(() => DelayedStartTime);
					RaisePropertyChanged(() => BsSmVersionNumber);
				}
			}
		}

		public string ReplyText {
			get { return _replyText; }
			set {
				if (_replyText != value) {
					_replyText = value;
					RaisePropertyChanged(() => ReplyText);
				}
			}
		}

		public int TargetTemperatureInsideTheCabin {
			get {
				if (_reply == null) throw new TelemetryIsNullException();
				return _reply.TargetTemperatureInsideTheCabin;
			}
		}



		public int FanSpeedLevel {
			get {
				if (_reply == null) throw new TelemetryIsNullException();
				return _reply.FanSpeedLevel;
			}
		}

		public bool IsWarmFloorOn {
			get {
				if (_reply == null) throw new TelemetryIsNullException();
				return _reply.IsWarmFloorOn;
			}
		}


		public uint AstronomicTime {
			get {
				if (_reply == null) throw new TelemetryIsNullException();
				return _reply.AstronomicTime;
			}
		}

		public uint DelayedStartTime {
			get {
				if (_reply == null) throw new TelemetryIsNullException();
				return _reply.DelayedStartTime;
			}
		}

		public int TemperatureOutdoor {
			get {
				if (_reply == null) throw new TelemetryIsNullException();
				return _reply.TemperatureOutdoor;
			}
		}

		public IBsSmState BsSmState {
			get { return _bsSmStateVm; }
		}

		public int BsSmVersionNumber {
			get {
				if (_reply == null) throw new TelemetryIsNullException();
				return _reply.BsSmVersionNumber;
			}
		}
	}
}
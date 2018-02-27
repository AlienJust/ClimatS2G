using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Request32;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.BsSm;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Reply32 {
	class BsSmReply32DataViewModel : ViewModelBase, ICmdListenerStd, IBsSmReply32Data {
		private readonly IThreadNotifier _notifier;

		private readonly Shared.BsSm.State.ViewModel _bsSmState;
		
		private IBsSmReply32Data _reply;
		private string _replyText;

		public BsSmReply32DataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;
			_bsSmState = new Shared.BsSm.State.ViewModel();
		}

		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr != 0x06) return;
			if (code == 0x20 && data.Count == 20) { // reply
				_notifier.Notify(() => {
					ReplyText = data.ToText();
					Reply = new BsSmReply32BuilderFromReplyDataBytes(data).Build(); // TODO try, if catch - null, and request too
					_bsSmState.Update(_reply.BsSmState);
				});
			}
		}

		public IBsSmReply32Data Reply {
			get => _reply;
			set {
				if (_reply != value) {
					_reply = value;
					RaisePropertyChanged(() => TargetTemperatureInsideTheCabin);

					RaisePropertyChanged(() => FanSpeedLevel);
					RaisePropertyChanged(() => IsWarmFloorOn);

					RaisePropertyChanged(() => AstronomicTime);
					RaisePropertyChanged(() => DelayedStartTime);
					
					RaisePropertyChanged(() => TemperatureOutdoor);
					RaisePropertyChanged(() => TemperatureIndoor);
					RaisePropertyChanged(() => ClimaticWorkmode);
					RaisePropertyChanged(() => WorkMode);

					RaisePropertyChanged(() => DelayedStartTime);
					RaisePropertyChanged(() => BsSmVersionNumber);
				}
			}
		}

		public string ReplyText {
			get => _replyText;
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


		public int TemperatureIndoor {
			get {
				if (_reply == null) throw new TelemetryIsNullException();
				return _reply.TemperatureIndoor;
			}
		}
		public ClimaticSystemWorkMode ClimaticWorkmode {
			get {
				if (_reply == null) throw new TelemetryIsNullException();
				return _reply.ClimaticWorkmode;
			}
		}
		public IWorkMode WorkMode {
			get {
				if (_reply == null) throw new TelemetryIsNullException();
				return _reply.WorkMode;
			}
		}

		public Shared.BsSm.State.IContract BsSmState => _bsSmState;

		public int BsSmVersionNumber {
			get {
				if (_reply == null) throw new TelemetryIsNullException();
				return _reply.BsSmVersionNumber;
			}
		}

	}
}
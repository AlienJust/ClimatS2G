using System.Collections.Generic;
using System.Linq;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using CustomModbusSlave.Es2gClimatic;
using CustomModbusSlave.Es2gClimatic.BsSm;
using CustomModbusSlave.MicroclimatEs2gApp.Common;
using CustomModbusSlave.MicroclimatEs2gApp.Common.BsSm;

namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	class BsSmRequestDataViewModel : ViewModelBase, ICommandListener, IBsSmDataCommand32Request {
		private readonly IThreadNotifier _notifier;

		private IBsSmDataCommand32Request _request;
		private string _requestText;

		public BsSmRequestDataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;
		}

		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr != 0x06) return;
			if (code == 0x20 && data.Count == 21) { // request
				_notifier.Notify(() => {
					RequestText = data.ToText();
					Request = new BuilderBsSmDataCommand32RequestFromCommandPartDataBytes(data.Skip(2).Take(data.Count - 4).ToList()).Build();
				});
			}
		}

		public IBsSmDataCommand32Request Request {
			get { return _request; }
			set {
				if (_request != value) {
					_request = value;
					RaisePropertyChanged(() => Request);
					RaisePropertyChanged(() => TemperatureInsideTheCabin);
					RaisePropertyChanged(() => TemperatureOutdoor);
					RaisePropertyChanged(() => FanSpeed);
					RaisePropertyChanged(() => IsTunelModeOn);
					RaisePropertyChanged(() => IsWarmFloorOn);
					RaisePropertyChanged(() => CurrentClimaticWorkMode);
					RaisePropertyChanged(() => Fault1);
					RaisePropertyChanged(() => Fault2);
					RaisePropertyChanged(() => Fault3);
					RaisePropertyChanged(() => Fault4);
					RaisePropertyChanged(() => Fault5);
				}
			}
		}

		public string RequestText {
			get { return _requestText; }
			set {
				if (_requestText != value) {
					_requestText = value;
					RaisePropertyChanged(() => RequestText);
				}
			}
		}

		public int TemperatureInsideTheCabin {
			get {
				if (_request == null) throw new TelemetryIsNullException();
				return _request.TemperatureInsideTheCabin;
			}
		}

		public int TemperatureOutdoor {
			get {
				if (_request == null) throw new TelemetryIsNullException();
				return _request.TemperatureOutdoor;
			}
		}

		public int FanSpeed {
			get {
				if (_request == null) throw new TelemetryIsNullException();
				return _request.FanSpeed;
			}
		}

		public bool IsTunelModeOn {
			get {
				if (_request == null) throw new TelemetryIsNullException();
				return _request.IsTunelModeOn;
			}
		}

		public bool IsWarmFloorOn {
			get {
				if (_request == null) throw new TelemetryIsNullException();
				return _request.IsWarmFloorOn;
			}
		}

		public ClimaticSystemWorkMode CurrentClimaticWorkMode {
			get {
				if (_request == null) throw new TelemetryIsNullException();
				return _request.CurrentClimaticWorkMode;
			}
		}

		public int Fault1 {
			get {
				if (_request == null) throw new TelemetryIsNullException();
				return _request.Fault1;
			}
		}

		public int Fault2 {
			get {
				if (_request == null) throw new TelemetryIsNullException();
				return _request.Fault2;
			}
		}

		public int Fault3 {
			get {
				if (_request == null) throw new TelemetryIsNullException();
				return _request.Fault3;
			}
		}

		public int Fault4 {
			get {
				if (_request == null) throw new TelemetryIsNullException();
				return _request.Fault4;
			}
		}

		public int Fault5 {
			get {
				if (_request == null) throw new TelemetryIsNullException();
				return _request.Fault5;
			}
		}
	}
}
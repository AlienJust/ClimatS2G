using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;

namespace CustomModbusSlave.MicroclimatEs2gApp {
	internal class MukWarmFloorDataViewModel : ViewModelBase, ICommandListener {
		private readonly IThreadNotifier _notifier;
		private readonly string _header = "МУК вентилятора испарителя";

		private string _heatingPwm;
		private string _analogInput;
		private string _temperatureRegulatorWorkMode;
		private string _incomingSignals;
		private string _outgoingSignals;
		private string _automaticModeStage;
		private string _calculatedTemperatureSetting;

		private string _firmwareBuildNumber;
		private string _reply;
		private string _diagnostic1;
		private string _diagnostic2;

		public MukWarmFloorDataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;
		}

		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr != 0x05) return;
			if (code == 0x03 && data.Count == 31) {
				_notifier.Notify(() => {
					HeatingPwm = (data[3]*256.0 + data[4]).ToString("f2");

					AnalogInput = (new UshortTextPresenter(data[6], data[5], true)).PresentAsText();
					TemperatureRegulatorWorkMode = (new UshortTextPresenter(data[8], data[7], false)).PresentAsText();

					IncomingSignals = (new ByteTextPresenter(data[10], true)).PresentAsText();
					OutgoingSignals = (new ByteTextPresenter(data[12], true)).PresentAsText();

					AutomaticModeStage = (new UshortTextPresenter(data[14], data[13], false)).PresentAsText();
					CalculatedTemperatureSetting = (new UshortTextPresenter(data[16], data[15], false)).PresentAsText();

					Diagnostic1 = (new UshortTextPresenter(data[18], data[17], true)).PresentAsText();
					Diagnostic2 = (new UshortTextPresenter(data[20], data[19], true)).PresentAsText();

					FirmwareBuildNumber = (new DataDoubleTextPresenter(data[22], data[21], 1.0, 0)).PresentAsText();

					Reply = data.ToText();
				});
			}
		}

		public string HeatingPwm {
			get { return _heatingPwm; }
			set {
				if (_heatingPwm != value) {
					_heatingPwm = value;
					RaisePropertyChanged(() => HeatingPwm);
				}
			}
		}


		public string AnalogInput {
			get { return _analogInput; }
			set {
				if (_analogInput != value) {
					_analogInput = value;
					RaisePropertyChanged(() => AnalogInput);
				}
			}
		}

		public string TemperatureRegulatorWorkMode {
			get { return _temperatureRegulatorWorkMode; }
			set {
				if (_temperatureRegulatorWorkMode != value) {
					_temperatureRegulatorWorkMode = value;
					RaisePropertyChanged(() => TemperatureRegulatorWorkMode);
				}
			}
		}

		public string IncomingSignals {
			get { return _incomingSignals; }
			set {
				if (_incomingSignals != value) {
					_incomingSignals = value;
					RaisePropertyChanged(() => IncomingSignals);
				}
			}
		}

		public string OutgoingSignals {
			get { return _outgoingSignals; }
			set {
				if (_outgoingSignals != value) {
					_outgoingSignals = value;
					RaisePropertyChanged(() => OutgoingSignals);
				}
			}
		}

		public string AutomaticModeStage {
			get { return _automaticModeStage; }
			set {
				if (_automaticModeStage != value) {
					_automaticModeStage = value;
					RaisePropertyChanged(() => AutomaticModeStage);
				}
			}
		}

		public string CalculatedTemperatureSetting {
			get { return _calculatedTemperatureSetting; }
			set {
				if (_calculatedTemperatureSetting != value) {
					_calculatedTemperatureSetting = value;
					RaisePropertyChanged(() => CalculatedTemperatureSetting);
				}
			}
		}

		public string Diagnostic1 {
			get { return _diagnostic1; }
			set {
				if (_diagnostic1 != value) {
					_diagnostic1 = value;
					RaisePropertyChanged(() => Diagnostic1);
				}
			}
		}

		public string Diagnostic2 {
			get { return _diagnostic2; }
			set {
				if (_diagnostic2 != value) {
					_diagnostic2 = value;
					RaisePropertyChanged(() => Diagnostic2);
				}
			}
		}

		public string FirmwareBuildNumber {
			get { return _firmwareBuildNumber; }
			set {
				if (_firmwareBuildNumber != value) {
					_firmwareBuildNumber = value;
					RaisePropertyChanged(() => FirmwareBuildNumber);
				}
			}
		}

		public string Reply {
			get { return _reply; }
			set {
				if (_reply != value) {
					_reply = value;
					RaisePropertyChanged(() => Reply);
				}
			}
		}
	}
}

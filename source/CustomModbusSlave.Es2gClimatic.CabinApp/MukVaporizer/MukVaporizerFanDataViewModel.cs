using System.Collections.Generic;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukVaporizer.Request16;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukVaporizer.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.MukVaporizer.TemperatureRegulatorWorkMode;
using CustomModbusSlave.Es2gClimatic.Shared.TextPresenters;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukVaporizer {
	/// <summary>
	/// View model вентилятора испарителя
	/// </summary>
	class MukVaporizerFanDataViewModel : ViewModelBase, ICommandListener {
		private readonly IThreadNotifier _notifier;
		private readonly string _header = "МУК вентилятора испарителя";
		private string _fanPwm;
		private string _temperatureAddress1;
		private string _temperatureAddress2;
		private string _incomingSignals;
		private string _outgoingSignals;
		private string _analogInput;
		private string _heatingPwm;

		private string _firmwareBuildNumber;
		private string _reply;
		private string _diagnostic5;
		private string _diagnostic4;
		private string _diagnostic3;
		private string _diagnostic2;
		private string _diagnostic1;
		private string _fanSpeed;
		private string _calculatedTemperatureSetting;
		private ITemperatureRegulatorWorkMode _temperatureRegulatorWorkMode;
		private string _automaticModeStage;

		private IRequest16Data _request16Telemetry;
		public MukVaporizerFanDataViewModel(IThreadNotifier notifier, IParameterSetter parameterSetter) {
			_notifier = notifier;
			Request16TelemetryText = new AnyCommandPartViewModel();
			MukVaporizerSetParamsVm = new MukVaporizerSetParamsViewModel(notifier, parameterSetter);
		}

		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr != 0x03) return;
			if (code == 0x03 && data.Count == 41) {
				_notifier.Notify(() => {
					FanPwm = (data[3] * 256.0 + data[4]).ToString("f0");

					TemperatureAddress1 = new DataDoubleTextPresenter(data[6], data[5], 0.01, 2).PresentAsText();
					TemperatureAddress2 = new DataDoubleTextPresenter(data[8], data[7], 0.01, 2).PresentAsText();

					IncomingSignals = new ByteTextPresenter(data[10], false).PresentAsText();
					OutgoingSignals = new ByteTextPresenter(data[12], false).PresentAsText();
					AnalogInput = new UshortTextPresenter(data[14], data[13], false).PresentAsText();

					HeatingPwm = new UshortTextPresenter(data[16], data[15], false).PresentAsText(); // 3.6
					AutomaticModeStage = new UshortTextPresenter(data[18], data[17], false).PresentAsText();

					//TemperatureRegulatorWorkMode = new DataDoubleTextPresenter(data[20], data[19], 0.01, 2).PresentAsText(); // TODO: 3.8 present as bits
					TemperatureRegulatorWorkMode = new TemperatureRegulatorWorkModeBuilderReplied(new BytesPair(data[19], data[20])).Build();

					CalculatedTemperatureSetting = new DataDoubleTextPresenter(data[22], data[21], 0.01, 2).PresentAsText();
					FanSpeed = new UshortTextPresenter(data[24], data[23], false).PresentAsText();
					Diagnostic1 = new UshortTextPresenter(data[26], data[25], true).PresentAsText(); // TODO: present as bits
					Diagnostic2 = new UshortTextPresenter(data[28], data[27], true).PresentAsText(); // TODO: present as bits
					Diagnostic3 = new UshortTextPresenter(data[30], data[29], true).PresentAsText(); // TODO: present as bits
					Diagnostic4 = new UshortTextPresenter(data[32], data[31], true).PresentAsText(); // TODO: present as bits
					Diagnostic5 = new UshortTextPresenter(data[34], data[33], false).PresentAsText(); // TODO: present as bits

					FirmwareBuildNumber = (new DataDoubleTextPresenter(data[36], data[35], 1.0, 0)).PresentAsText();

					Reply = data.ToText();
				});
			}
			else if (code == 0x10 && data.Count == 21) {
				_notifier.Notify(() => {
					Request16TelemetryText.Update(data);
					Request16Telemetry = new Request16DataBuilder(data).Build();
				});
			}
		}

		public string AutomaticModeStage {
			get { return _automaticModeStage; }
			set { if (_automaticModeStage != value) { _automaticModeStage = value; RaisePropertyChanged(() => AutomaticModeStage); } }
		}

		public ITemperatureRegulatorWorkMode TemperatureRegulatorWorkMode {
			get { return _temperatureRegulatorWorkMode; }
			set { if (_temperatureRegulatorWorkMode != value) { _temperatureRegulatorWorkMode = value; RaisePropertyChanged(() => TemperatureRegulatorWorkMode); } }
		}

		public string CalculatedTemperatureSetting {
			get { return _calculatedTemperatureSetting; }
			set { if (_calculatedTemperatureSetting != value) { _calculatedTemperatureSetting = value; RaisePropertyChanged(() => CalculatedTemperatureSetting); } }
		}

		public string FanSpeed {
			get { return _fanSpeed; }
			set { if (_fanSpeed != value) { _fanSpeed = value; RaisePropertyChanged(() => FanSpeed); } }
		}

		public string Diagnostic1 {
			get { return _diagnostic1; }
			set { if (_diagnostic1 != value) { _diagnostic1 = value; RaisePropertyChanged(() => Diagnostic1); } }
		}

		public string Diagnostic2 {
			get { return _diagnostic2; }
			set { if (_diagnostic2 != value) { _diagnostic2 = value; RaisePropertyChanged(() => Diagnostic2); } }
		}

		public string Diagnostic3 {
			get { return _diagnostic3; }
			set { if (_diagnostic3 != value) { _diagnostic3 = value; RaisePropertyChanged(() => Diagnostic3); } }
		}

		public string Diagnostic4 {
			get { return _diagnostic4; }
			set { if (_diagnostic4 != value) { _diagnostic4 = value; RaisePropertyChanged(() => Diagnostic4); } }
		}

		public string Diagnostic5 {
			get { return _diagnostic5; }
			set { if (_diagnostic5 != value) { _diagnostic5 = value; RaisePropertyChanged(() => Diagnostic5); } }
		}

		public string FanPwm {
			get { return _fanPwm; }
			set {
				if (_fanPwm != value) {
					_fanPwm = value;
					RaisePropertyChanged(() => FanPwm);
				}
			}
		}

		public string TemperatureAddress1 {
			get { return _temperatureAddress1; }
			set {
				if (_temperatureAddress1 != value) {
					_temperatureAddress1 = value;
					RaisePropertyChanged(() => TemperatureAddress1);
				}
			}
		}

		public string TemperatureAddress2 {
			get { return _temperatureAddress2; }
			set {
				if (_temperatureAddress2 != value) {
					_temperatureAddress2 = value;
					RaisePropertyChanged(() => TemperatureAddress2);
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

		public string AnalogInput {
			get { return _analogInput; }
			set {
				if (_analogInput != value) {
					_analogInput = value;
					RaisePropertyChanged(() => AnalogInput);
				}
			}
		}

		public string HeatingPwm {
			get { return _heatingPwm; }
			set { if (_heatingPwm != value) { _heatingPwm = value; RaisePropertyChanged(() => HeatingPwm); } }
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

		public IRequest16Data Request16Telemetry {
			get { return _request16Telemetry; }
			set {
				if (_request16Telemetry != value) {
					_request16Telemetry = value;
					RaisePropertyChanged(() => Request16Telemetry);
				}
			}
		}
		public AnyCommandPartViewModel Request16TelemetryText { get; }

		public MukVaporizerSetParamsViewModel MukVaporizerSetParamsVm { get; }
	}
}

using System.Collections.Generic;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.TemperatureRegulatorWorkMode;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.ViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Common.UniversalParams.BytesPairConverters;
using ParamCentric.Common.Contracts;
using ParamCentric.Modbus.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer {
	public class MukVaporizerFanDataViewModelParamcentric : ViewModelBase, IGroup {
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IMukVaporizerFanReply03Telemetry> _cmdListenerMukVaporizerReply03;
		private readonly ICmdListener<IMukFanVaporizerRequest16Data> _cmdListenerMukVaporizerRequest16;
		//private readonly IReceiverModbusCustom _customReceiver;
		//private readonly IReceiverModbusRtu _rtuReceiver;
		private const string Header = "МУК вентилятора испарителя";
		private const string NoSensor = "Обрыв датчика";
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

		private IMukFanVaporizerRequest16Data _request16Telemetry;
		private readonly List<IGroupItem> _children;

		public AnyCommandPartViewModel Request16TelemetryText { get; }
		public MukVaporizerSetParamsViewModel MukVaporizerSetParamsVm { get; }

		public MukVaporizerFanDataViewModelParamcentric(IThreadNotifier notifier, 
			IParameterSetter parameterSetter, IReceiverModbusRtu rtuReceiver,
			ICmdListener<IMukVaporizerFanReply03Telemetry> cmdListenerMukVaporizerReply03,
			ICmdListener<IMukFanVaporizerRequest16Data> cmdListenerMukVaporizerRequest16) {

			_notifier = notifier;

			_cmdListenerMukVaporizerReply03 = cmdListenerMukVaporizerReply03;
			_cmdListenerMukVaporizerReply03.DataReceived += CmdListenerMukVaporizerReply03OnDataReceived;

			_cmdListenerMukVaporizerRequest16 = cmdListenerMukVaporizerRequest16;
			_cmdListenerMukVaporizerRequest16.DataReceived += CmdListenerMukVaporizerRequest16OnDataReceived;

			_children = new List<IGroupItem>();
			var pwmParameter = new ReceivableModbusRtuParameterSimpleViewModel("Уставка ШИМ на вентилятор", 3, 3, 0, new BytesPairNullableToStringThroughDoubleConverter(1.0, 0.0, false, true, "f0"));
			var t1Parameter = new ReceivableModbusRtuParameterSimpleViewModel("Температура 1wire адрес 1", 3, 3, 1, new BytesPairNullableToStringThroughOneWireConverter(0.01, 0.0, "f2", new BytesPair(0x85, 0x00)));
			var t2Parameter = new ReceivableModbusRtuParameterSimpleViewModel("Температура 1wire адрес 2", 3, 3, 2, new BytesPairNullableToStringThroughOneWireConverter(0.01, 0.0, "f2", new BytesPair(0x85, 0x00)));
			var inputSignals = new ReceivableModbusRtuParameterSimpleViewModel("Байт входных сигналов (резерв)", 3, 3, 3, new BytesPairNullableToStringThroughDoubleConverter(1.0, 0.0, false, true, "f0"));
			var outputSignals = new ReceivableModbusRtuParameterSimpleViewModel("Байт выходных сигналов (резерв)", 3, 3, 4, new BytesPairNullableToStringThroughDoubleConverter(1.0, 0.0, false, true, "f0"));
			var pwmCalorifer = new ReceivableModbusRtuParameterSimpleViewModel("ШИМ на калорифер", 3, 3, 5, new BytesPairNullableToStringThroughDoubleConverter(1.0, 0.0, false, true, "f0"));
			var workStage = new ReceivableModbusRtuParameterSimpleViewModel("Этап работы", 3, 3, 6, new BytesPairNullableToStringThroughVaporizerFanWorkstageConverter());
			//var pwmParameter 
			rtuReceiver.RegisterParamToReceive(pwmParameter);
			rtuReceiver.RegisterParamToReceive(t1Parameter);
			rtuReceiver.RegisterParamToReceive(t2Parameter);
			rtuReceiver.RegisterParamToReceive(inputSignals);
			rtuReceiver.RegisterParamToReceive(outputSignals);
			rtuReceiver.RegisterParamToReceive(pwmCalorifer);
			rtuReceiver.RegisterParamToReceive(workStage);

			_children.Add(pwmParameter);
			_children.Add(t1Parameter);
			_children.Add(t2Parameter);

			_children.Add(inputSignals);
			_children.Add(outputSignals);
			_children.Add(pwmCalorifer);
			_children.Add(workStage);

			Request16TelemetryText = new AnyCommandPartViewModel();
			MukVaporizerSetParamsVm = new MukVaporizerSetParamsViewModel(notifier, parameterSetter);
		}

		private void CmdListenerMukVaporizerReply03OnDataReceived(IList<byte> bytes, IMukVaporizerFanReply03Telemetry data) {
			_notifier.Notify(() => {
				FanPwm = data.FanPwm.ToString("f2");

				TemperatureAddress1 = data.TemperatureAddress1.NoLinkWithSensor ? NoSensor : data.TemperatureAddress1.Indication.ToString("f2");
				TemperatureAddress2 = data.TemperatureAddress1.NoLinkWithSensor ? NoSensor : data.TemperatureAddress2.Indication.ToString("f2");

				IncomingSignals = "0x" + data.IncomingSignals.ToString("X2");
				OutgoingSignals = "0x" + data.OutgoingSignals.ToString("X2");

				AnalogInput = "0x" + data.AnalogInput.ToString("X4");
				HeatingPwm = data.HeatingPwm.ToString();
				AutomaticModeStage = data.AutomaticModeStage.ToString();
				TemperatureRegulatorWorkMode = data.TemperatureRegulatorWorkMode;
				CalculatedTemperatureSetting = data.CalculatedTemperatureSetting.ToString("f2");
				FanSpeed = data.FanSpeed.ToString();
				Diagnostic1 = "0x" + data.Diagnostic1.ToString("X4");
				Diagnostic2 = "0x" + data.Diagnostic2.ToString("X4");
				Diagnostic3 = "0x" + data.Diagnostic3.ToString("X4");
				Diagnostic4 = "0x" + data.Diagnostic4.ToString("X4");
				Diagnostic5 = data.Diagnostic5.ToString();

				FirmwareBuildNumber = new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber);

				Reply = bytes.ToText();
			});
		}

		private void CmdListenerMukVaporizerRequest16OnDataReceived(IList<byte> bytes, IMukFanVaporizerRequest16Data data) {
			_notifier.Notify(() => {
				Request16TelemetryText.Update(bytes);
				Request16Telemetry = data;
			});
		}


		public string AutomaticModeStage {
			get { return _automaticModeStage; }
			set { if (_automaticModeStage != value) { _automaticModeStage = value; RaisePropertyChanged(() => AutomaticModeStage); } }
		}

		public ITemperatureRegulatorWorkMode TemperatureRegulatorWorkMode {
			get { return _temperatureRegulatorWorkMode; }
			set {
				if (_temperatureRegulatorWorkMode != value) {
					_temperatureRegulatorWorkMode = value;
					RaisePropertyChanged(() => TemperatureRegulatorWorkMode);
				}
			}
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


		public IMukFanVaporizerRequest16Data Request16Telemetry {
			get { return _request16Telemetry; }
			set {
				if (_request16Telemetry != value) {
					_request16Telemetry = value;
					RaisePropertyChanged(() => Request16Telemetry);
				}
			}
		}
		
		public string Name => Header;
		public IList<IGroupItem> Children => _children;
	}
}

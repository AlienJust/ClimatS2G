using System.Collections.Generic;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.ViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.BytesPairConverters.Nullable;
using ParamCentric.Common.Contracts;
using ParamCentric.Modbus.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator {
	public class MukVaporizerFanDataViewModelParamcentric : ViewModelBase, IGroup {
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IMukFanVaporizerDataReply03> _cmdListenerMukVaporizerReply03;
		private readonly ICmdListener<IMukFanVaporizerDataRequest16> _cmdListenerMukVaporizerRequest16;
		//private readonly IReceiverModbusCustom _customReceiver;
		//private readonly IReceiverModbusRtu _rtuReceiver;
		private const string Header = "МУК вентилятора испарителя";
		private const string NoSensor = "Обрыв датчика";
		
		private IMukFanVaporizerDataReply03 _mukFanVaporizerDataReply03;
		public AnyCommandPartViewModel MukFanVaporizerDataReply03Text { get; }

		private IMukFanVaporizerDataRequest16 _request16Telemetry;
		public AnyCommandPartViewModel Request16TelemetryText { get; }

		public MukVaporizerSetParamsViewModel MukVaporizerSetParamsVm { get; }

		private readonly List<IGroupItem> _children;

		public MukVaporizerFanDataViewModelParamcentric(IThreadNotifier notifier, 
			IParameterSetter parameterSetter, IReceiverModbusRtu rtuReceiver,
			ICmdListener<IMukFanVaporizerDataReply03> cmdListenerMukVaporizerReply03,
			ICmdListener<IMukFanVaporizerDataRequest16> cmdListenerMukVaporizerRequest16) {

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

			MukFanVaporizerDataReply03Text = new AnyCommandPartViewModel();
			Request16TelemetryText = new AnyCommandPartViewModel();
			MukVaporizerSetParamsVm = new MukVaporizerSetParamsViewModel(notifier, parameterSetter);
		}

		private void CmdListenerMukVaporizerReply03OnDataReceived(IList<byte> bytes, IMukFanVaporizerDataReply03 data) {
			_notifier.Notify(() => {
				MukFanVaporizerDataReply03Text.Update(bytes);
				MukFanVaporizerDataReply03 = data;
				/*
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
				*/
			});
		}

		private void CmdListenerMukVaporizerRequest16OnDataReceived(IList<byte> bytes, IMukFanVaporizerDataRequest16 data) {
			_notifier.Notify(() => {
				Request16TelemetryText.Update(bytes);
				Request16Telemetry = data;
			});
		}

		
		public IMukFanVaporizerDataReply03 MukFanVaporizerDataReply03 {
			get { return _mukFanVaporizerDataReply03; }
			set {
				if (_mukFanVaporizerDataReply03 != value) {
					_mukFanVaporizerDataReply03 = value;
					RaisePropertyChanged(() => MukFanVaporizerDataReply03);
				}
			}
		}


		public IMukFanVaporizerDataRequest16 Request16Telemetry {
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

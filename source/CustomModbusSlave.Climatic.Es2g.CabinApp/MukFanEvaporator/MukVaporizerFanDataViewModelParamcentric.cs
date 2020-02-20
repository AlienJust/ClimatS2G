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

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator
{
    public class MukVaporizerFanDataViewModelParamcentric : ViewModelBase, IGroup
    {
        private const string Header = "МУК вентилятора испарителя";
        private const string NoSensor = "Обрыв датчика";

        private readonly List<IGroupItem> _children;
        private readonly ICmdListener<IMukFanVaporizerDataReply03> _cmdListenerMukVaporizerReply03;
        private readonly ICmdListener<IMukFanVaporizerDataRequest16> _cmdListenerMukVaporizerRequest16;
        private readonly IThreadNotifier _notifier;

        private IMukFanVaporizerDataReply03 _mukFanVaporizerDataReply03;

        private IMukFanVaporizerDataRequest16 _request16Telemetry;

        public MukVaporizerFanDataViewModelParamcentric(IThreadNotifier notifier,
            IParameterSetter parameterSetter, IReceiverModbusRtu rtuReceiver,
            ICmdListener<IMukFanVaporizerDataReply03> cmdListenerMukVaporizerReply03,
            ICmdListener<IMukFanVaporizerDataRequest16> cmdListenerMukVaporizerRequest16)
        {
            _notifier = notifier;

            _cmdListenerMukVaporizerReply03 = cmdListenerMukVaporizerReply03;
            _cmdListenerMukVaporizerReply03.DataReceived += CmdListenerMukVaporizerReply03OnDataReceived;

            _cmdListenerMukVaporizerRequest16 = cmdListenerMukVaporizerRequest16;
            _cmdListenerMukVaporizerRequest16.DataReceived += CmdListenerMukVaporizerRequest16OnDataReceived;

            _children = new List<IGroupItem>();

            var pwmParameter = new ReceivableModbusRtuParameterSimpleViewModel("Уставка ШИМ на вентилятор", 3, 3, 0,
                new BytesPairNullableToStringThroughDoubleConverter(1.0, 0.0, false, true, "f0"));
            var t1Parameter = new ReceivableModbusRtuParameterSimpleViewModel("Температура 1wire адрес 1", 3, 3, 1,
                new BytesPairNullableToStringThroughOneWireConverter(0.01, 0.0, "f2", new BytesPair(0x85, 0x00)));
            var t2Parameter = new ReceivableModbusRtuParameterSimpleViewModel("Температура 1wire адрес 2", 3, 3, 2,
                new BytesPairNullableToStringThroughOneWireConverter(0.01, 0.0, "f2", new BytesPair(0x85, 0x00)));
            var inputSignals = new ReceivableModbusRtuParameterSimpleViewModel("Байт входных сигналов (резерв)", 3, 3,
                3, new BytesPairNullableToStringThroughDoubleConverter(1.0, 0.0, false, true, "f0"));
            var outputSignals = new ReceivableModbusRtuParameterSimpleViewModel("Байт выходных сигналов (резерв)", 3, 3,
                4, new BytesPairNullableToStringThroughDoubleConverter(1.0, 0.0, false, true, "f0"));
            var pwmCalorifer = new ReceivableModbusRtuParameterSimpleViewModel("ШИМ на калорифер", 3, 3, 5,
                new BytesPairNullableToStringThroughDoubleConverter(1.0, 0.0, false, true, "f0"));
            var workStage = new ReceivableModbusRtuParameterSimpleViewModel("Этап работы", 3, 3, 6,
                new BytesPairNullableToStringThroughVaporizerFanWorkstageConverter());


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

        public AnyCommandPartViewModel MukFanVaporizerDataReply03Text { get; }
        public AnyCommandPartViewModel Request16TelemetryText { get; }

        public MukVaporizerSetParamsViewModel MukVaporizerSetParamsVm { get; }


        public IMukFanVaporizerDataReply03 MukFanVaporizerDataReply03
        {
            get => _mukFanVaporizerDataReply03;
            set
            {
                if (_mukFanVaporizerDataReply03 != value)
                {
                    _mukFanVaporizerDataReply03 = value;
                    RaisePropertyChanged(() => MukFanVaporizerDataReply03);
                }
            }
        }


        public IMukFanVaporizerDataRequest16 Request16Telemetry
        {
            get => _request16Telemetry;
            set
            {
                if (_request16Telemetry != value)
                {
                    _request16Telemetry = value;
                    RaisePropertyChanged(() => Request16Telemetry);
                }
            }
        }

        public string Name => Header;
        public IList<IGroupItem> Children => _children;

        private void CmdListenerMukVaporizerReply03OnDataReceived(IList<byte> bytes, IMukFanVaporizerDataReply03 data)
        {
            _notifier.Notify(() =>
            {
                MukFanVaporizerDataReply03Text.Update(bytes);
                MukFanVaporizerDataReply03 = data;
            });
        }

        private void CmdListenerMukVaporizerRequest16OnDataReceived(IList<byte> bytes,
            IMukFanVaporizerDataRequest16 data)
        {
            _notifier.Notify(() =>
            {
                Request16TelemetryText.Update(bytes);
                Request16Telemetry = data;
            });
        }
    }
}
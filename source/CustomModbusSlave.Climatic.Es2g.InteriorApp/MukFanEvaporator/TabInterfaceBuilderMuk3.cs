using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Conversion.Contracts;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.OneWire;
using CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm;
using DrillingRig.ConfigApp.AppControl.ParamLogger;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator
{
    public sealed class TabInterfaceBuilderMuk3 : IBuilderManyToOne<IDisplayGroup>
    {
        //private readonly SearchViewModel _svm;
        private readonly ICmdListener<IMukFanVaporizerDataReply03> _cmd03ReplyListener;
        private readonly ICmdListener<IMukFanVaporizerDataRequest16> _cmdListener16Request;
        private readonly IParameterLogger _parameterLogger;
        private readonly IParameterSetter _parameterSetter;
        private readonly IThreadNotifier _uiNotifier;

        public TabInterfaceBuilderMuk3(
            IThreadNotifier mainWindowUiUiNotifier,
            ICmdListener<IMukFanVaporizerDataReply03> cmd03ReplyListener,
            ICmdListener<IMukFanVaporizerDataRequest16> cmdListener16Request,
            IParameterLogger parameterLogger,
            IParameterSetter parameterSetter)
        {
            _uiNotifier = mainWindowUiUiNotifier;
            _cmd03ReplyListener = cmd03ReplyListener;
            _cmdListener16Request = cmdListener16Request;

            _parameterLogger = parameterLogger;
            _parameterSetter = parameterSetter;
        }

        public IDisplayGroup Build()
        {
            var muk03Group = new GroupParamViewModel("МУК 3, вентилятор испарителя");
            var reply03Group = new GroupParamViewModel("Ответ на команду 3");


            IRecvParam<int> cmd03ReplyPwm =
                new RecvParam<int, IMukFanVaporizerDataReply03>("PWM", _cmd03ReplyListener, data => data.FanPwm);
            var cmd03ReplyPwmDisp = new DispParamViewModel<int, int>("Уставка ШИМ на вентилятор", cmd03ReplyPwm,
                _uiNotifier, pwmValue => pwmValue, 0, -1);

            var cmd03ReplyPwmChart = new ChartParamViewModel<int, int>(
                cmd03ReplyPwm,
                cmd03ReplyPwmDisp,
                data => data,
                ParameterLogType.Analogue,
                _parameterLogger,
                muk03Group.DisplayName,
                reply03Group.DisplayName);
            reply03Group.AddParameterOrGroup(cmd03ReplyPwmChart);


            IRecvParam<ISensorIndication<double>> cmd03ReplyT1W1 =
                new RecvParam<ISensorIndication<double>, IMukFanVaporizerDataReply03>("PWM", _cmd03ReplyListener,
                    data => data.TemperatureAddress1);
            var cmd03ReplyT1W1Disp = new DispParamViewModel<string, ISensorIndication<double>>("Температура 1w адрес 1",
                cmd03ReplyT1W1, _uiNotifier, t1W1 => t1W1.ToText("f2"), "Ошибка", "Неизвестно");

            var cmd03ReplyT1W1Chart = new ChartParamViewModel<ISensorIndication<double>, string>(
                cmd03ReplyT1W1,
                cmd03ReplyT1W1Disp,
                data => data.Indication,
                ParameterLogType.Analogue,
                _parameterLogger,
                muk03Group.DisplayName,
                reply03Group.DisplayName);
            reply03Group.AddParameterOrGroup(cmd03ReplyT1W1Chart);


            muk03Group.AddParameterOrGroup(reply03Group);
            return muk03Group;
        }
    }
}
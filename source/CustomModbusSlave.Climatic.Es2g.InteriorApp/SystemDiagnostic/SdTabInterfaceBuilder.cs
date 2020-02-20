using System;
using System.Collections.Generic;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Conversion.Contracts;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.AppWindow;
using CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm;
using DrillingRig.ConfigApp.AppControl.ParamLogger;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.SystemDiagnostic
{
    internal sealed class SdTabInterfaceBuilder : IBuilderManyToOne<IDisplayGroup>
    {
        private readonly IThreadNotifier _uiNotifier;

        private readonly ICmdListener<IList<BytesPair>> _cmdListenerKsmParams;
        //private readonly SearchViewModel _svm;

        private readonly IParameterLogger _parameterLogger;
        private readonly IParameterSetter _parameterSetter;
        private readonly AppVersion _version;

        public SdTabInterfaceBuilder(IThreadNotifier mainWindowUiUiNotifier, ICmdListener<IList<BytesPair>> cmdListenerKsmParams, IParameterLogger parameterLogger, IParameterSetter parameterSetter, AppVersion version)
        {
            _uiNotifier = mainWindowUiUiNotifier;
            _cmdListenerKsmParams = cmdListenerKsmParams;

            _parameterLogger = parameterLogger;
            _parameterSetter = parameterSetter;
            _version = version;
        }

        public IDisplayGroup Build()
        {
            var timeCountersGroup = new GroupParamViewModel("Моточасы: ");
            //const string reply03GroupName = "МУК 8, заслонка зима лето";



            // setting group of settable params
            //var setParamsGroup = new GroupParamViewModel("Параметры КСМ");

            var recvParam39 = new RecvParam<int, IList<BytesPair>>("39: Обеззараживатель, почасовой счетчик работы №1", _cmdListenerKsmParams, data => data[39].HighFirstUnsignedValue);
            var setParam39 = new SettParamViewModel<int>(recvParam39.ReceiveName, _uiNotifier, val =>
            {
                if (val < 0 || val > 65535) throw new ArgumentOutOfRangeException();
            }, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(39, (ushort)val, callback));

            var dispsetParameter39 = new DispParamSettableViewModel<int, int, int>(recvParam39.ReceiveName, recvParam39, _uiNotifier, i => i, 0, 0, val =>
            {
                if (val < 0 || val > 65535) throw new ArgumentOutOfRangeException();
            }, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(39, (ushort)val, callback));


            var recvParam50 = new RecvParam<int, IList<BytesPair>>("50: Обеззараживатель, почасовой счетчик работы №2", _cmdListenerKsmParams, data => data[50].HighFirstUnsignedValue);
            var setParam50 = new SettParamViewModel<int>(recvParam50.ReceiveName, _uiNotifier, val =>
            {
                if (val < 0 || val > 65535) throw new ArgumentOutOfRangeException();
            }, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(50, (ushort)val, callback));

            var dispsetParameter50 = new DispParamSettableViewModel<int, int, int>(recvParam50.ReceiveName, recvParam50, _uiNotifier, i => i, 0, 0, val =>
            {
                if (val < 0 || val > 65535) throw new ArgumentOutOfRangeException();
            }, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(50, (ushort)val, callback));

            var recvParam51 = new RecvParam<int, IList<BytesPair>>("51: Обеззараживатель, почасовой счетчик работы №3", _cmdListenerKsmParams, data => data[51].HighFirstUnsignedValue);
            var setParam51 = new SettParamViewModel<int>(recvParam51.ReceiveName, _uiNotifier, val =>
            {
                if (val < 0 || val > 65535) throw new ArgumentOutOfRangeException();
            }, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(51, (ushort)val, callback));

            var dispsetParameter51 = new DispParamSettableViewModel<int, int, int>(recvParam51.ReceiveName, recvParam51, _uiNotifier, i => i, 0, 0, val =>
            {
                if (val < 0 || val > 65535) throw new ArgumentOutOfRangeException();
            }, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(51, (ushort)val, callback));

            var recvParam52 = new RecvParam<int, IList<BytesPair>>("52: Обеззараживатель, почасовой счетчик работы №4", _cmdListenerKsmParams, data => data[52].HighFirstUnsignedValue);
            var setParam52 = new SettParamViewModel<int>(recvParam52.ReceiveName, _uiNotifier, val =>
            {
                if (val < 0 || val > 65535) throw new ArgumentOutOfRangeException();
            }, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(52, (ushort)val, callback));

            var dispsetParameter52 = new DispParamSettableViewModel<int, int, int>(recvParam52.ReceiveName, recvParam52, _uiNotifier, i => i, 0, 0, val =>
            {
                if (val < 0 || val > 65535) throw new ArgumentOutOfRangeException();
            }, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(52, (ushort)val, callback));


            // defining version of app, if full then using checkboxVm, else without checkbox:
            IDisplayParameter setParam39Vm = _version == AppVersion.Full ? (IDisplayParameter)new ChartParamViewModel<int, int>(recvParam39, dispsetParameter39, i => i, ParameterLogType.Analogue, _parameterLogger, timeCountersGroup.DisplayName) : new AggregateParamViewModel<int>(dispsetParameter39, timeCountersGroup.DisplayName);
            IDisplayParameter setParam50Vm = _version == AppVersion.Full ? (IDisplayParameter)new ChartParamViewModel<int, int>(recvParam50, dispsetParameter50, i => i, ParameterLogType.Analogue, _parameterLogger, timeCountersGroup.DisplayName) : new AggregateParamViewModel<int>(dispsetParameter50, timeCountersGroup.DisplayName);
            IDisplayParameter setParam51Vm = _version == AppVersion.Full ? (IDisplayParameter)new ChartParamViewModel<int, int>(recvParam51, dispsetParameter51, i => i, ParameterLogType.Analogue, _parameterLogger, timeCountersGroup.DisplayName) : new AggregateParamViewModel<int>(dispsetParameter51, timeCountersGroup.DisplayName);
            IDisplayParameter setParam52Vm = _version == AppVersion.Full ? (IDisplayParameter)new ChartParamViewModel<int, int>(recvParam52, dispsetParameter52, i => i, ParameterLogType.Analogue, _parameterLogger, timeCountersGroup.DisplayName) : new AggregateParamViewModel<int>(dispsetParameter52, timeCountersGroup.DisplayName);


            timeCountersGroup.AddParameterOrGroup(setParam39Vm);
            timeCountersGroup.AddParameterOrGroup(setParam50Vm);
            timeCountersGroup.AddParameterOrGroup(setParam51Vm);
            timeCountersGroup.AddParameterOrGroup(setParam52Vm);

            //timeCountersGroup.AddParameterOrGroup(timeCountersGroup);
            return timeCountersGroup;
        }
    }
}
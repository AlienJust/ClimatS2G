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

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.Ksm
{
    internal sealed class KsmTabInterfaceBuilder : IBuilderManyToOne<IDisplayGroup>
    {
        private readonly IThreadNotifier _uiNotifier;

        private readonly ICmdListener<IList<BytesPair>> _cmdListenerKsmParams;
        //private readonly SearchViewModel _svm;

        private readonly IParameterLogger _parameterLogger;
        private readonly IParameterSetter _parameterSetter;
        private readonly AppVersion _version;

        public KsmTabInterfaceBuilder(IThreadNotifier mainWindowUiUiNotifier, ICmdListener<IList<BytesPair>> cmdListenerKsmParams, IParameterLogger parameterLogger, IParameterSetter parameterSetter, AppVersion version)
        {
            _uiNotifier = mainWindowUiUiNotifier;
            _cmdListenerKsmParams = cmdListenerKsmParams;

            _parameterLogger = parameterLogger;
            _parameterSetter = parameterSetter;
            _version = version;
        }

        public IDisplayGroup Build()
        {
            var ksmGroup = new GroupParamViewModel("КСМ");


            // setting group of settable params
            var setParamsGroup = new GroupParamViewModel("Параметры КСМ");
            
            #region Param28
            var recvParam28 = new RecvParam<int, IList<BytesPair>>(
                "28: Максимальный ШИМ (PWMmax_cool)", _cmdListenerKsmParams,
                data => data[28].LowFirstUnsignedValue);
            var dispsetParam28 = new DispParamSettableViewModel<int, int, int>(recvParam28.ReceiveName, recvParam28,
                _uiNotifier, i => i, 0, 0, val =>
                {
                    if (val < 0 || val > 255) throw new ArgumentOutOfRangeException();
                }, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(28, (ushort) val, callback));
            var aggSetParam28 = new ChartParamViewModel<int, int>(recvParam28, dispsetParam28, i => i,
                ParameterLogType.Analogue, _parameterLogger, setParamsGroup.DisplayName);
            setParamsGroup.AddParameterOrGroup(aggSetParam28);
            #endregion

            #region Param29
            var recvParam29 = new RecvParam<int, IList<BytesPair>>(
                "29: Минимальный ШИМ (PWMmin_cool)", _cmdListenerKsmParams,
                data => data[29].LowFirstUnsignedValue);
            var dispsetParam29 = new DispParamSettableViewModel<int, int, int>(recvParam29.ReceiveName, recvParam29,
                _uiNotifier, i => i, 0, 0, val =>
                {
                    if (val < 0 || val > 255) throw new ArgumentOutOfRangeException();
                }, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(29, (ushort) val, callback));
            var aggSetParam29 = new ChartParamViewModel<int, int>(recvParam29, dispsetParam29, i => i,
                ParameterLogType.Analogue, _parameterLogger, setParamsGroup.DisplayName);
            setParamsGroup.AddParameterOrGroup(aggSetParam29);
            #endregion
            
            #region Param30
            var recvParam30 = new RecvParam<int, IList<BytesPair>>(
                "30: Дельта +  (plus_cool)", _cmdListenerKsmParams,
                data => data[30].LowFirstUnsignedValue);
            var dispsetParam30 = new DispParamSettableViewModel<int, int, int>(recvParam30.ReceiveName, recvParam30,
                _uiNotifier, i => i, 0, 0, val =>
                {
                    if (val < 0 || val > 65535) throw new ArgumentOutOfRangeException();
                }, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(30, (ushort) val, callback));
            var aggSetParam30 = new ChartParamViewModel<int, int>(recvParam30, dispsetParam30, i => i,
                ParameterLogType.Analogue, _parameterLogger, setParamsGroup.DisplayName);
            setParamsGroup.AddParameterOrGroup(aggSetParam30);
            #endregion
            
            #region Param31
            var recvParam31 = new RecvParam<int, IList<BytesPair>>(
                "31: Дельта - (minus_cool)", _cmdListenerKsmParams,
                data => data[31].LowFirstUnsignedValue);
            var dispsetParam31 = new DispParamSettableViewModel<int, int, int>(recvParam31.ReceiveName, recvParam31,
                _uiNotifier, i => i, 0, 0, val =>
                {
                    if (val < 0 || val > 65535) throw new ArgumentOutOfRangeException();
                }, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(31, (ushort) val, callback));
            var aggSetParam31 = new ChartParamViewModel<int, int>(recvParam31, dispsetParam31, i => i,
                ParameterLogType.Analogue, _parameterLogger, setParamsGroup.DisplayName);
            setParamsGroup.AddParameterOrGroup(aggSetParam31);
            #endregion
            
            
            #region Param36
            var recvParam36 = new RecvParam<int, IList<BytesPair>>(
                "36: Дельта уставки температуры, отладка", _cmdListenerKsmParams,
                data => data[36].LowFirstUnsignedValue);
            var dispsetParam36 = new DispParamSettableViewModel<string, int, double>(recvParam36.ReceiveName, recvParam36,
                _uiNotifier, i => (i *0.1 - 2.5).ToString("f1"), "ER", "?", val =>
                {
                    if (val < -2.5 || val > 2.5) throw new ArgumentOutOfRangeException();
                }, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(36, (ushort)((val + 2.5) * 10), callback));
            var aggSetParam36 = new ChartParamViewModel<int, string>(recvParam36, dispsetParam36, i => i,
                ParameterLogType.Analogue, _parameterLogger, setParamsGroup.DisplayName);
            setParamsGroup.AddParameterOrGroup(aggSetParam36);
            #endregion

            ksmGroup.AddParameterOrGroup(setParamsGroup);
            return ksmGroup;
        }
    }
}
using System;
using System.Collections.Generic;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Conversion.Contracts;
using AlienJust.Support.Numeric.Bits;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.AppWindow;
using CustomModbusSlave.Es2gClimatic.Shared.Ksm.WamOrCoolForcedModes;
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


            #region Param00

            var recvParam00 = new RecvParam<int, IList<BytesPair>>("00: Датчик в кабине машиниста",
                _cmdListenerKsmParams, data => data[00].HighFirstUnsignedValue);

            var dispParam00 =
                new DispParamViewModel<string, int>(recvParam00.ReceiveName, recvParam00,
                    _uiNotifier, data => (data * 0.01).ToString("f2"), "ER", "?");
            var chartParam00 = new ChartParamViewModel<int, string>(recvParam00,
                dispParam00, data => data * 0.01, ParameterLogType.Analogue, _parameterLogger,
                setParamsGroup.DisplayName);
            setParamsGroup.AddParameterOrGroup(chartParam00);

            #endregion

            #region Param01

            var recvParam01 = new RecvParam<int, IList<BytesPair>>("01: Датчик на трубке выхода испарителя",
                _cmdListenerKsmParams, data => data[01].HighFirstUnsignedValue);

            var dispParam01 =
                new DispParamViewModel<string, int>(recvParam01.ReceiveName, recvParam01,
                    _uiNotifier, data => (data * 0.01).ToString("f2"), "ER", "?");
            var chartParam01 = new ChartParamViewModel<int, string>(recvParam01,
                dispParam01, data => data * 0.01, ParameterLogType.Analogue, _parameterLogger,
                setParamsGroup.DisplayName);
            setParamsGroup.AddParameterOrGroup(chartParam01);

            #endregion

            #region Param02

            var recvParam02 = new RecvParam<int, IList<BytesPair>>("02: Датчик на трубке выхода конденсатора",
                _cmdListenerKsmParams, data => data[02].HighFirstUnsignedValue);

            var dispParam02 =
                new DispParamViewModel<string, int>(recvParam02.ReceiveName, recvParam02,
                    _uiNotifier, data => (data * 0.01).ToString("f2"), "ER", "?");
            var chartParam02 = new ChartParamViewModel<int, string>(recvParam02,
                dispParam02, data => data * 0.01, ParameterLogType.Analogue, _parameterLogger,
                setParamsGroup.DisplayName);
            setParamsGroup.AddParameterOrGroup(chartParam02);

            #endregion

            #region Param03

            var recvParam03 = new RecvParam<int, IList<BytesPair>>("03: Уставка температуры в кабине",
                _cmdListenerKsmParams, data => data[03].HighFirstUnsignedValue);

            var dispParam03 =
                new DispParamViewModel<string, int>(recvParam03.ReceiveName, recvParam03,
                    _uiNotifier, data => (data * 0.01).ToString("f2"), "ER", "?");
            var chartParam03 = new ChartParamViewModel<int, string>(recvParam03,
                dispParam03, data => data * 0.01, ParameterLogType.Analogue, _parameterLogger,
                setParamsGroup.DisplayName);
            setParamsGroup.AddParameterOrGroup(chartParam03);

            #endregion

            #region Param04

            var recvParam04 = new RecvParam<int, IList<BytesPair>>("04: Уставка объема подачи воздуха в кабину",
                _cmdListenerKsmParams, data => data[04].HighFirstUnsignedValue);

            var dispParam04 =
                new DispParamViewModel<string, int>(recvParam04.ReceiveName, recvParam04,
                    _uiNotifier, data => data.ToString(), "ER", "?");
            var chartParam04 = new ChartParamViewModel<int, string>(recvParam04,
                dispParam04, data => data, ParameterLogType.Analogue, _parameterLogger,
                setParamsGroup.DisplayName);
            setParamsGroup.AddParameterOrGroup(chartParam04);

            #endregion

            #region Param05

            var recvParam05 = new RecvParam<int, IList<BytesPair>>("05: PIC порт A",
                _cmdListenerKsmParams, data => data[05].HighFirstUnsignedValue);

            var groupParam05 = new GroupParamViewModel(recvParam05.ReceiveName);
            setParamsGroup.AddParameterOrGroup(groupParam05);


            var dispParam0504 = new DispParamViewModel<bool, int>(
                "PA.4=0 – включение 3ст.", recvParam05,
                _uiNotifier, incomingByte => !incomingByte.GetBit(4),
                false, false);
            var chartParam0504 = new ChartParamViewModel<int, bool>(
                recvParam05, dispParam0504,
                data => !data.GetBit(4) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                setParamsGroup.DisplayName, groupParam05.DisplayName);
            groupParam05.AddParameterOrGroup(chartParam0504);


            var dispParam0505 = new DispParamViewModel<bool, int>(
                "PA.5=0 – включение 4ст.", recvParam05,
                _uiNotifier, incomingByte => !incomingByte.GetBit(5),
                false, false);
            var chartParam0505 = new ChartParamViewModel<int, bool>(
                recvParam05, dispParam0505,
                data => !data.GetBit(5) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                setParamsGroup.DisplayName, groupParam05.DisplayName);
            groupParam05.AddParameterOrGroup(chartParam0505);

            #endregion


            #region Param06

            var recvParam06 = new RecvParam<int, IList<BytesPair>>("06: PIC порт C",
                _cmdListenerKsmParams, data => data[06].HighFirstUnsignedValue);

            var groupParam06 = new GroupParamViewModel(recvParam06.ReceiveName);
            setParamsGroup.AddParameterOrGroup(groupParam06);


            var dispParam0600 = new DispParamViewModel<bool, int>(
                "PС.0=0 – включение 1ст.", recvParam06,
                _uiNotifier, incomingByte => !incomingByte.GetBit(0),
                false, false);
            var chartParam0600 = new ChartParamViewModel<int, bool>(
                recvParam06, dispParam0600,
                data => !data.GetBit(0) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                setParamsGroup.DisplayName, groupParam06.DisplayName);
            groupParam06.AddParameterOrGroup(chartParam0600);


            var dispParam0601 = new DispParamViewModel<bool, int>(
                "PС.1=0 – включение клапана ЭРВ", recvParam06,
                _uiNotifier, incomingByte => !incomingByte.GetBit(1),
                false, false);
            var chartParam0601 = new ChartParamViewModel<int, bool>(
                recvParam06, dispParam0601,
                data => !data.GetBit(1) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                setParamsGroup.DisplayName, groupParam06.DisplayName);
            groupParam06.AddParameterOrGroup(chartParam0601);

            var dispParam0606 = new DispParamViewModel<bool, int>(
                "PС.6=1 – выключение реле разрешения запуска контактора компрессора", recvParam06,
                _uiNotifier, incomingByte => incomingByte.GetBit(6),
                false, false);
            var chartParam0606 = new ChartParamViewModel<int, bool>(
                recvParam06, dispParam0606,
                data => data.GetBit(6) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                setParamsGroup.DisplayName, groupParam06.DisplayName);
            groupParam06.AddParameterOrGroup(chartParam0606);


            var dispParam0607 = new DispParamViewModel<bool, int>(
                "PС.7=0 – включение 2ст.", recvParam06,
                _uiNotifier, incomingByte => !incomingByte.GetBit(7),
                false, false);
            var chartParam0607 = new ChartParamViewModel<int, bool>(
                recvParam06, dispParam0607,
                data => !data.GetBit(7) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                setParamsGroup.DisplayName, groupParam06.DisplayName);
            groupParam06.AddParameterOrGroup(chartParam0607);

            #endregion


            #region Param10

            var recvParam10 = new RecvParam<int, IList<BytesPair>>("10: PIC порт B",
                _cmdListenerKsmParams, data => data[10].HighFirstUnsignedValue);

            var groupParam10 = new GroupParamViewModel(recvParam10.ReceiveName);
            setParamsGroup.AddParameterOrGroup(groupParam10);


            var dispParam1000 = new DispParamViewModel<bool, int>(
                "PB.0=1 – авария компрессора по максимальному давлению", recvParam10,
                _uiNotifier, incomingByte => incomingByte.GetBit(0),
                false, false);
            var chartParam1000 = new ChartParamViewModel<int, bool>(
                recvParam10, dispParam1000,
                data => data.GetBit(0) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                setParamsGroup.DisplayName, groupParam10.DisplayName);
            groupParam10.AddParameterOrGroup(chartParam1000);


            var dispParam1001 = new DispParamViewModel<bool, int>(
                "PB.1=1 – авария компрессора по максимальному давлению", recvParam10,
                _uiNotifier, incomingByte => incomingByte.GetBit(1),
                false, false);
            var chartParam1001 = new ChartParamViewModel<int, bool>(
                recvParam10, dispParam1001,
                data => data.GetBit(1) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                setParamsGroup.DisplayName, groupParam10.DisplayName);
            groupParam10.AddParameterOrGroup(chartParam1001);


            var dispParam1002 = new DispParamViewModel<bool, int>(
                "PB.2=1 – авария двигателя компрессора", recvParam10,
                _uiNotifier, incomingByte => incomingByte.GetBit(2),
                false, false);
            var chartParam1002 = new ChartParamViewModel<int, bool>(
                recvParam10, dispParam1002,
                data => data.GetBit(2) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                setParamsGroup.DisplayName, groupParam10.DisplayName);
            groupParam10.AddParameterOrGroup(chartParam1002);

            var dispParam1003 = new DispParamViewModel<bool, int>(
                "PB.3=0 – команда на включение кондиционера", recvParam10,
                _uiNotifier, incomingByte => !incomingByte.GetBit(3),
                false, false);
            var chartParam1003 = new ChartParamViewModel<int, bool>(
                recvParam10, dispParam1003,
                data => !data.GetBit(3) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                setParamsGroup.DisplayName, groupParam10.DisplayName);
            groupParam10.AddParameterOrGroup(chartParam1003);

            var dispParam1004 = new DispParamViewModel<bool, int>(
                "PB.4=0 – команда на включение кондиционера в режиме обогрев/охлаждение", recvParam10,
                _uiNotifier, incomingByte => !incomingByte.GetBit(4),
                false, false);
            var chartParam1004 = new ChartParamViewModel<int, bool>(
                recvParam10, dispParam1004,
                data => !data.GetBit(4) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                setParamsGroup.DisplayName, groupParam10.DisplayName);
            groupParam10.AddParameterOrGroup(chartParam1004);

            var dispParam1005 = new DispParamViewModel<bool, int>(
                "PB.5=0 – компрессор включен", recvParam10,
                _uiNotifier, incomingByte => !incomingByte.GetBit(5),
                false, false);
            var chartParam1005 = new ChartParamViewModel<int, bool>(
                recvParam10, dispParam1005,
                data => !data.GetBit(5) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                setParamsGroup.DisplayName, groupParam10.DisplayName);
            groupParam10.AddParameterOrGroup(chartParam1005);

            #endregion
            
            #region Param11

            var recvParam11 = new RecvParam<int, IList<BytesPair>>("11: Давление в контуре низкого давления, бар",
                _cmdListenerKsmParams, data => data[11].HighFirstSignedValue);

            var dispParam11 =
                new DispParamViewModel<string, int>(recvParam11.ReceiveName, recvParam11,
                    _uiNotifier, data => (data * 0.1).ToString("f1"), "ER", "?");
            var chartParam11 = new ChartParamViewModel<int, string>(recvParam11,
                dispParam11, data => data * 0.1, ParameterLogType.Analogue, _parameterLogger,
                setParamsGroup.DisplayName);
            setParamsGroup.AddParameterOrGroup(chartParam11);

            #endregion
            
            #region Param12

            var recvParam12 = new RecvParam<int, IList<BytesPair>>("12: Давление в контуре высокого давления, бар",
                _cmdListenerKsmParams, data => data[12].HighFirstSignedValue);

            var dispParam12 =
                new DispParamViewModel<string, int>(recvParam12.ReceiveName, recvParam12,
                    _uiNotifier, data => (data * 0.1).ToString("f1"), "ER", "?");
            var chartParam12 = new ChartParamViewModel<int, string>(recvParam12,
                dispParam12, data => data * 0.1, ParameterLogType.Analogue, _parameterLogger,
                setParamsGroup.DisplayName);
            setParamsGroup.AddParameterOrGroup(chartParam12);

            #endregion
            
            #region Param13

            var recvParam13 = new RecvParam<int, IList<BytesPair>>("13: Температура в контуре низкого давления, град",
                _cmdListenerKsmParams, data => (sbyte)data[13].Second);

            var dispParam13 =
                new DispParamViewModel<string, int>(recvParam13.ReceiveName, recvParam13,
                    _uiNotifier, data => (data * 0.1).ToString("f1"), "ER", "?");
            var chartParam13 = new ChartParamViewModel<int, string>(recvParam13,
                dispParam13, data => data * 0.1, ParameterLogType.Analogue, _parameterLogger,
                setParamsGroup.DisplayName);
            setParamsGroup.AddParameterOrGroup(chartParam13);

            #endregion
            
            #region Param14

            var recvParam14 = new RecvParam<int, IList<BytesPair>>("14: Перегрев хладагента, град",
                _cmdListenerKsmParams, data => (sbyte)data[14].Second);

            var dispParam14 =
                new DispParamViewModel<string, int>(recvParam14.ReceiveName, recvParam14,
                    _uiNotifier, data => data.ToString(), "ER", "?");
            var chartParam14 = new ChartParamViewModel<int, string>(recvParam14,
                dispParam14, data => data, ParameterLogType.Analogue, _parameterLogger,
                setParamsGroup.DisplayName);
            setParamsGroup.AddParameterOrGroup(chartParam14);

            #endregion

            
            #region Param28

            var recvParam28 = new RecvParam<int, IList<BytesPair>>(
                "28: Максимальный ШИМ (PWMmax_cool)", _cmdListenerKsmParams,
                data => data[28].HighFirstUnsignedValue);
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
                data => data[29].HighFirstUnsignedValue);
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
                data => data[30].HighFirstUnsignedValue);
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
                data => data[31].HighFirstUnsignedValue);
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
                data => data[36].HighFirstUnsignedValue);
            var dispsetParam36 = new DispParamSettableViewModel<string, int, double>(recvParam36.ReceiveName, recvParam36,
                _uiNotifier, i => (i * 0.1 - 2.5).ToString("f1"), "ER", "?", val =>
                {
                    if (val < -2.5 || val > 2.5) throw new ArgumentOutOfRangeException();
                }, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(36, (ushort) ((val + 2.5) * 10), callback));
            var aggSetParam36 = new ChartParamViewModel<int, string>(recvParam36, dispsetParam36, i => i,
                ParameterLogType.Analogue, _parameterLogger, setParamsGroup.DisplayName);
            setParamsGroup.AddParameterOrGroup(aggSetParam36);

            #endregion


            #region Param37

            var recvParam37 = new RecvParam<int, IList<BytesPair>>(
                "37: Принудительный режим обогрев/охлаждение", _cmdListenerKsmParams,
                data => data[37].HighFirstUnsignedValue);
            var dispsetParam37 = new DispParamSettableViewModel<string, int, int>(recvParam37.ReceiveName, recvParam37,
                _uiNotifier, i => new WarmOrCoolForcedModeToStringConverter().Build((WarmOrCoolForcedMode) i), "ER",
                "?", val =>
                {
                    if (val < 0 || val > 10) throw new ArgumentOutOfRangeException();
                }, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(37, (ushort) val, callback));
            var aggSetParam37 = new ChartParamViewModel<int, string>(recvParam37, dispsetParam37, i => i,
                ParameterLogType.Analogue, _parameterLogger, setParamsGroup.DisplayName);
            setParamsGroup.AddParameterOrGroup(aggSetParam37);

            #endregion


            ksmGroup.AddParameterOrGroup(setParamsGroup);
            return ksmGroup;
        }
    }
}
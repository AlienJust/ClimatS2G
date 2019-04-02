using System;
using System.Collections.Generic;
using System.Linq;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Conversion.Contracts;
using AlienJust.Support.Numeric.Bits;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.Reply03;
using CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.Request16;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.OneWire.Diagnostic;
using CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm;
using DrillingRig.ConfigApp.AppControl.ParamLogger;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator
{
    internal sealed class TabInterfaceBuilder : IBuilderManyToOne<IDisplayGroup>
    {
        private readonly IThreadNotifier _uiNotifier;

        private readonly ICmdListener<IMukFanVaporizerDataReply03> _cmdListenerEvaporator03Reply;
        private readonly ICmdListener<IMukFanVaporizerDataRequest16> _cmdListenerEvaporator16Request;
        private readonly IParameterLogger _parameterLogger;
        private readonly IParameterSetter _parameterSetter;

        public TabInterfaceBuilder(
            IThreadNotifier mainWindowUiUiNotifier,
            ICmdListener<IMukFanVaporizerDataReply03> cmdListenerEvaporator03Reply,
            ICmdListener<IMukFanVaporizerDataRequest16> cmdListenerEvaporator16Request,
            IParameterLogger parameterLogger,
            IParameterSetter parameterSetter)
        {
            _uiNotifier = mainWindowUiUiNotifier;
            _cmdListenerEvaporator03Reply = cmdListenerEvaporator03Reply;
            _cmdListenerEvaporator16Request = cmdListenerEvaporator16Request;
            _parameterLogger = parameterLogger;
            _parameterSetter = parameterSetter;
        }

        public IDisplayGroup Build()
        {
            var muk03Group = new GroupParamViewModel("МУК 3, вентилятор испарителя");
            //const string reply03GroupName = "МУК 8, заслонка зима лето";
            var reply03Group = new GroupParamViewModel("Ответ на команду 3");


            IRecvParam<IMukFanVaporizerDataReply03> mukFanData = new RecvParam<IMukFanVaporizerDataReply03, IMukFanVaporizerDataReply03>("Muk0303Rp",
                _cmdListenerEvaporator03Reply, data => data);

            var mukFlapWinterSummerPwmDisplay =
                new DispParamViewModel<int, IMukFanVaporizerDataReply03>("Уставка ШИМ на клапан", mukFanData,
                    _uiNotifier, data => data.FanPwm, 0, -1);
            var mukFlapWinterSummerPwmChart = new ChartParamViewModel<IMukFanVaporizerDataReply03, int>(mukFanData,
                mukFlapWinterSummerPwmDisplay, data => data.FanPwm, ParameterLogType.Analogue, _parameterLogger,
                muk03Group.DisplayName, reply03Group.DisplayName);
            reply03Group.AddParameterOrGroup(mukFlapWinterSummerPwmChart);


            //IRecvParam<IList<byte>> mukFlapWinterTemperatureOneWireAddr1 = new RecvParam<IList<byte>, IList<byte>>("T1W1", _cmdListenerEvaporator03Reply, bytes => bytes.Skip(3).Take(2).ToList());
            var mukFlapWinterTemperatureOneWireAddr1Display = new DispParamViewModel<string, IMukFanVaporizerDataReply03>(
                "Температура 1w адрес 1", mukFanData, _uiNotifier,
                data => data.TemperatureAddress1.ToString(d => d.ToString("f2")), "ER", "??");
            var mukFlapWinterTemperatureOneWireAddr1Chart = new ChartParamViewModel<IMukFanVaporizerDataReply03, string>(
                mukFanData, mukFlapWinterTemperatureOneWireAddr1Display,
                data => data.TemperatureAddress1.Indication, ParameterLogType.Analogue, _parameterLogger,
                muk03Group.DisplayName, reply03Group.DisplayName);
            reply03Group.AddParameterOrGroup(mukFlapWinterTemperatureOneWireAddr1Chart);


            var mukFlapWinterTemperatureOneWireAddr2Display = new DispParamViewModel<string, IMukFanVaporizerDataReply03>(
                "Температура 1w адрес 2", mukFanData, _uiNotifier,
                data => data.TemperatureAddress1.ToString(d => d.ToString("f2")), "ER", "??");

            var mukFlapWinterTemperatureOneWireAddr2Chart =
                new ChartParamViewModel<IMukFanVaporizerDataReply03, string>(mukFanData,
                    mukFlapWinterTemperatureOneWireAddr2Display,
                    data => data.TemperatureAddress2.Indication, ParameterLogType.Analogue, _parameterLogger,
                    muk03Group.DisplayName, reply03Group.DisplayName);
            reply03Group.AddParameterOrGroup(mukFlapWinterTemperatureOneWireAddr2Chart);


            IRecvParam<byte> mukFlapWinterIncomingSignals = new RecvParam<byte, IMukFanVaporizerDataReply03>(
                "Байт входных сигналов", _cmdListenerEvaporator03Reply, data => data.IncomingSignals);
            var groupIncomingSignals = new GroupParamViewModel(mukFlapWinterIncomingSignals.ReceiveName);
            reply03Group.AddParameterOrGroup(groupIncomingSignals);


            var mukFanVaporizerIsFaultFuse = new DispParamViewModel<bool, byte>(
                "Нарушение целостности предохранителя вентилятора испарителя", mukFlapWinterIncomingSignals,
                _uiNotifier, incomingByte => incomingByte.GetBit(0),
                false, false);
            var mukFanVaporizerIsFaultFuseChart = new ChartParamViewModel<byte, bool>(
                mukFlapWinterIncomingSignals, mukFanVaporizerIsFaultFuse,
                data => data.GetBit(0) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                muk03Group.DisplayName, reply03Group.DisplayName, groupIncomingSignals.DisplayName);
            groupIncomingSignals.AddParameterOrGroup(mukFanVaporizerIsFaultFuseChart);


            var mukFanVaporizerIsFaultTemp = new DispParamViewModel<bool, byte>(
                "Превышение по температуре", mukFlapWinterIncomingSignals,
                _uiNotifier, incomingByte => incomingByte.GetBit(1),
                false, false);
            var mukFanVaporizerIsFaultTempChart = new ChartParamViewModel<byte, bool>(
                mukFlapWinterIncomingSignals, mukFanVaporizerIsFaultTemp,
                data => data.GetBit(1) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                muk03Group.DisplayName, reply03Group.DisplayName, groupIncomingSignals.DisplayName);
            groupIncomingSignals.AddParameterOrGroup(mukFanVaporizerIsFaultTempChart);


            var mukFanVaporizerIsFaultHeaterOn = new DispParamViewModel<bool, byte>(
                "Калорифер включен", mukFlapWinterIncomingSignals,
                _uiNotifier, incomingByte => incomingByte.GetBit(2),
                false, false);
            var mukFanVaporizerIsFaultHeaterOnChart = new ChartParamViewModel<byte, bool>(
                mukFlapWinterIncomingSignals, mukFanVaporizerIsFaultHeaterOn,
                data => data.GetBit(2) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                muk03Group.DisplayName, reply03Group.DisplayName, groupIncomingSignals.DisplayName);
            groupIncomingSignals.AddParameterOrGroup(mukFanVaporizerIsFaultHeaterOnChart);


            IRecvParam<byte> mukFlapWinterOutgoingSignals = new RecvParam<byte, IMukFanVaporizerDataReply03>(
                "Байт выходных сигналов", _cmdListenerEvaporator03Reply, data => data.IncomingSignals);
            var groupOutgoingSignals = new GroupParamViewModel(mukFlapWinterOutgoingSignals.ReceiveName);
            reply03Group.AddParameterOrGroup(groupOutgoingSignals);


            var mukFanVaporizerOsFaultHeaterOn = new DispParamViewModel<bool, byte>(
                "Включение калорифера", mukFlapWinterIncomingSignals,
                _uiNotifier, incomingByte => incomingByte.GetBit(0),
                false, false);
            var mukFanVaporizerOsFaultHeaterOnChart = new ChartParamViewModel<byte, bool>(
                mukFlapWinterOutgoingSignals, mukFanVaporizerOsFaultHeaterOn,
                data => data.GetBit(0) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                muk03Group.DisplayName, reply03Group.DisplayName, groupOutgoingSignals.DisplayName);
            groupOutgoingSignals.AddParameterOrGroup(mukFanVaporizerOsFaultHeaterOnChart);


            IRecvParam<ushort> analogueInputRecvParam =
                new RecvParam<ushort, IMukFanVaporizerDataReply03>("Аналоговый вход от заслонки",
                    _cmdListenerEvaporator03Reply, data => data.AnalogInput);
            var analogueInputDispParam = new DispParamViewModel<string, ushort>(
                analogueInputRecvParam.ReceiveName, analogueInputRecvParam, _uiNotifier,
                data => (data * 0.1).ToString("f1"), "ER", "??");
            var analogueInputChartParam = new ChartParamViewModel<ushort, string>(analogueInputRecvParam,
                analogueInputDispParam, data => data * 0.1, ParameterLogType.Analogue,
                _parameterLogger, muk03Group.DisplayName, reply03Group.DisplayName);
            reply03Group.AddParameterOrGroup(analogueInputChartParam);


            IRecvParam<ushort> flapPwmRecvParam =
                new RecvParam<ushort, IMukFanVaporizerDataReply03>("Уставка ШИМ на заслонку",
                    _cmdListenerEvaporator03Reply, data => data.FlapPwm);
            var flapPwmDispParam = new DispParamViewModel<string, ushort>(
                flapPwmRecvParam.ReceiveName, flapPwmRecvParam, _uiNotifier,
                data => data.ToString(), "ER", "??");
            var flapPwmChartParam = new ChartParamViewModel<ushort, string>(flapPwmRecvParam, flapPwmDispParam,
                data => data, ParameterLogType.Analogue, _parameterLogger, muk03Group.DisplayName,
                reply03Group.DisplayName);
            reply03Group.AddParameterOrGroup(flapPwmChartParam);


            /*IRecvParam<MukFanEvaporatorWorkstage> autoModeStageRecvParam =
                new RecvParam<MukFanEvaporatorWorkstage, IMukFanVaporizerDataReply03>("Этап работы",
                    _cmdListenerEvaporator03Reply, data => data.AutomaticModeStageParsed);*/
            var automaticModeStageDispParam = new DispParamViewModel<string, IMukFanVaporizerDataReply03>(
                "Этап работы", mukFanData, _uiNotifier,
                data => data.AutomaticModeStageParsed.ToText(), "ER", "??");
            var automaticModeStageChartParam = new ChartParamViewModel<IMukFanVaporizerDataReply03, string>(
                mukFanData, automaticModeStageDispParam, data => data.AutomaticModeStage,
                ParameterLogType.Analogue, _parameterLogger, muk03Group.DisplayName, reply03Group.DisplayName);
            reply03Group.AddParameterOrGroup(automaticModeStageChartParam);


            IRecvParam<ushort> fanSpeedRecvParam =
                new RecvParam<ushort, IMukFanVaporizerDataReply03>("Обороты вентилятора",
                    _cmdListenerEvaporator03Reply, data => data.FanSpeed);
            var fanSpeedDispParam = new DispParamViewModel<string, ushort>(
                fanSpeedRecvParam.ReceiveName, fanSpeedRecvParam, _uiNotifier,
                data => data.ToString(), "ER", "??");
            var fanSpeedChartParam = new ChartParamViewModel<ushort, string>(fanSpeedRecvParam, fanSpeedDispParam,
                data => data, ParameterLogType.Analogue, _parameterLogger, muk03Group.DisplayName,
                reply03Group.DisplayName);
            reply03Group.AddParameterOrGroup(fanSpeedChartParam);


            var groupDiagnostic1 = new GroupParamViewModel("Диагностика 1");
            reply03Group.AddParameterOrGroup(groupDiagnostic1);

            IRecvParam<IMukFanVaporizerDataReply03Diagnostic1> diagnostic1RecvParam =
                new RecvParam<IMukFanVaporizerDataReply03Diagnostic1, IMukFanVaporizerDataReply03>(
                    groupDiagnostic1.DisplayName, _cmdListenerEvaporator03Reply, data => data.Diagnostic1Parsed);

            var diag1NoIoWithControllerDispParam = new DispParamViewModel<bool, IMukFanVaporizerDataReply03Diagnostic1>(
                "Отсутствие обмена с контроллером вентилятора",
                diagnostic1RecvParam, _uiNotifier, data => data.FanControllerLinkLost, false, false);
            var diag1NoIoWithControllerChartParam = new ChartParamViewModel<IMukFanVaporizerDataReply03Diagnostic1, bool>(
                diagnostic1RecvParam,
                diag1NoIoWithControllerDispParam, data => data.FanControllerLinkLost ? 1.0 : 0.0, ParameterLogType.Discrete,
                _parameterLogger, muk03Group.DisplayName, reply03Group.DisplayName, groupDiagnostic1.DisplayName);
            groupDiagnostic1.ParametersAndGroups.Add(diag1NoIoWithControllerChartParam);

            var diag1FanErrorByDisreteInputDispParam = new DispParamViewModel<bool, IMukFanVaporizerDataReply03Diagnostic1>(
                "Ошибка вентилятора (по дискретному входу)",
                diagnostic1RecvParam, _uiNotifier, data => data.FanErrorByDiscreteInput, false, false);
            var diag1FanErrorByDisreteInputChartParam = new ChartParamViewModel<IMukFanVaporizerDataReply03Diagnostic1, bool>(
                diagnostic1RecvParam,
                diag1FanErrorByDisreteInputDispParam, data => data.FanErrorByDiscreteInput ? 1.0 : 0.0, ParameterLogType.Discrete,
                _parameterLogger, muk03Group.DisplayName, reply03Group.DisplayName, groupDiagnostic1.DisplayName);
            groupDiagnostic1.ParametersAndGroups.Add(diag1FanErrorByDisreteInputChartParam);


            var diag1OneWire1ErrorDispParam = new DispParamViewModel<bool, IMukFanVaporizerDataReply03Diagnostic1>(
                "Ошибка датчика 1w №1",
                diagnostic1RecvParam, _uiNotifier, data => data.ErrorOneWireSensor1, false, false);
            var diag1OneWire1ErrorChartParam = new ChartParamViewModel<IMukFanVaporizerDataReply03Diagnostic1, bool>(
                diagnostic1RecvParam,
                diag1OneWire1ErrorDispParam, data => data.ErrorOneWireSensor1 ? 1.0 : 0.0, ParameterLogType.Discrete,
                _parameterLogger, muk03Group.DisplayName, reply03Group.DisplayName, groupDiagnostic1.DisplayName);
            groupDiagnostic1.ParametersAndGroups.Add(diag1OneWire1ErrorChartParam);


            var diag1OneWire2ErrorDispParam = new DispParamViewModel<bool, IMukFanVaporizerDataReply03Diagnostic1>(
                "Ошибка датчика 1w №2", diagnostic1RecvParam, _uiNotifier, data => data.ErrorOneWireSensor2, false, false);
            var diag1OneWire2ErrorChartParam = new ChartParamViewModel<IMukFanVaporizerDataReply03Diagnostic1, bool>(
                diagnostic1RecvParam, diag1OneWire2ErrorDispParam, data => data.ErrorOneWireSensor2 ? 1.0 : 0.0,
                ParameterLogType.Discrete, _parameterLogger, muk03Group.DisplayName, reply03Group.DisplayName,
                groupDiagnostic1.DisplayName);
            groupDiagnostic1.ParametersAndGroups.Add(diag1OneWire2ErrorChartParam);


            IRecvParam<IMukFanVaporizerDataReply03Diagnostic2> diag2RecvParam =
                new RecvParam<IMukFanVaporizerDataReply03Diagnostic2, IMukFanVaporizerDataReply03>("Диагностика 2",
                    _cmdListenerEvaporator03Reply, data => data.Diagnostic2Parsed);
            var groupDiagnostic2 = new GroupParamViewModel(diag2RecvParam.ReceiveName);
            reply03Group.AddParameterOrGroup(groupDiagnostic2);

            var diag2PhaseErrorOrNoPowerDispParam = new DispParamViewModel<bool, IMukFanVaporizerDataReply03Diagnostic2>(
                "Ошибка фазы или отсутствие питание", diag2RecvParam, _uiNotifier, diag2 => diag2.PhaseErrorOrPowerLost,
                false, false);
            
            /*
            var diagnostic2NotReachingLimitChart = new ChartParamViewModel<IList<byte>, bool>(diag2RecvParam, diag2PhaseErrorOrNoPowerDispParam, data => data[1].GetBit(5) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger, muk08Group.DisplayName, reply03Group.DisplayName, groupDiagnostic2.DisplayName);
            groupDiagnostic2.AddParameterOrGroup(diagnostic2NotReachingLimitChart);

            var diagnostic2NotCoveringHalfOfTheRangeDisp = new DispParamViewModel<bool, IList<byte>>("Заслонка не проходит 50% диапазона", diag2RecvParam, _uiNotifier, bytes => bytes[1].GetBit(6), false, false);
            var diagnostic2NotCoveringHalfOfTheRangeChart = new ChartParamViewModel<IList<byte>, bool>(diag2RecvParam, diagnostic2NotCoveringHalfOfTheRangeDisp, data => data[1].GetBit(6) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger, muk08Group.DisplayName, reply03Group.DisplayName, groupDiagnostic2.DisplayName);
            groupDiagnostic2.AddParameterOrGroup(diagnostic2NotCoveringHalfOfTheRangeChart);


            IRecvParam<IMukFlapDiagnosticOneWireSensor> diagnostic3Recv = new RecvParam<IMukFlapDiagnosticOneWireSensor, IList<byte>>("Диагностика 3, 1w адрес 1", _cmdListenerEvaporator03Reply, bytes => new MukFlapDiagnosticOneWireSensorBuilder(bytes[20]).Build());
            var groupDiagnostic3 = new GroupParamViewModel(diagnostic3Recv.ReceiveName);
            reply03Group.AddParameterOrGroup(groupDiagnostic3);
            var diagnostic3SensorErrorDisp = new DispParamViewModel<bool, IMukFlapDiagnosticOneWireSensor>("Ошибка датчика", diagnostic3Recv, _uiNotifier, d => d.OneWireSensorError, false, false);
            var diagnostic3SensorErrorChart = new ChartParamViewModel<IMukFlapDiagnosticOneWireSensor, bool>(diagnostic3Recv, diagnostic3SensorErrorDisp, diag => diag.OneWireSensorError ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger, muk08Group.DisplayName, reply03Group.DisplayName, groupDiagnostic3.DisplayName);
            groupDiagnostic3.AddParameterOrGroup(diagnostic3SensorErrorChart);


            var diagnostic3SensorErrorCodeDisp = new DispParamViewModel<string, IMukFlapDiagnosticOneWireSensor>("Код ошибки датчика", diagnostic3Recv, _uiNotifier, diag => diag.ErrorCode.AbsoluteValue + " - " + diag.ErrorCode.KnownValue.ToText(), "ER", "?");
            var diagnostic3SensorErrorCodeChart = new ChartParamViewModel<IMukFlapDiagnosticOneWireSensor, string>(diagnostic3Recv, diagnostic3SensorErrorCodeDisp, diag => diag.ErrorCode.AbsoluteValue, ParameterLogType.Analogue, _parameterLogger, muk08Group.DisplayName, reply03Group.DisplayName, groupDiagnostic3.DisplayName);
            groupDiagnostic3.AddParameterOrGroup(diagnostic3SensorErrorCodeChart);


            var diagnostic3SensorErrorsCountDisp = new DispParamViewModel<string, IMukFlapDiagnosticOneWireSensor>("Количество ошибок связи", diagnostic3Recv, _uiNotifier, diag => diag.LinkErrorCount.Value.ToString(), "ER", "?");
            var diagnostic3SensorErrorsCountChart = new ChartParamViewModel<IMukFlapDiagnosticOneWireSensor, string>(diagnostic3Recv, diagnostic3SensorErrorsCountDisp, diag => diag.LinkErrorCount.Value, ParameterLogType.Analogue, _parameterLogger, muk08Group.DisplayName, reply03Group.DisplayName, groupDiagnostic1.DisplayName);
            groupDiagnostic3.AddParameterOrGroup(diagnostic3SensorErrorsCountChart);


            IRecvParam<IMukFlapDiagnosticOneWireSensor> diagnostic4Recv = new RecvParam<IMukFlapDiagnosticOneWireSensor, IList<byte>>("Диагностика 4, 1w адрес 2", _cmdListenerEvaporator03Reply, bytes => new MukFlapDiagnosticOneWireSensorBuilder(bytes[22]).Build());
            var groupDiagnostic4 = new GroupParamViewModel(diagnostic4Recv.ReceiveName);
            reply03Group.AddParameterOrGroup(groupDiagnostic4);
            var diagnostic4SensorErrorDisp = new DispParamViewModel<bool, IMukFlapDiagnosticOneWireSensor>("Ошибка датчика", diagnostic4Recv, _uiNotifier, d => d.OneWireSensorError, false, false);
            var diagnostic4SensorErrorChart = new ChartParamViewModel<IMukFlapDiagnosticOneWireSensor, bool>(diagnostic4Recv, diagnostic4SensorErrorDisp, diag => diag.OneWireSensorError ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger, muk08Group.DisplayName, reply03Group.DisplayName, groupDiagnostic4.DisplayName);
            groupDiagnostic4.AddParameterOrGroup(diagnostic4SensorErrorChart);


            var diagnostic4SensorErrorCodeDisp = new DispParamViewModel<string, IMukFlapDiagnosticOneWireSensor>("Код ошибки датчика", diagnostic4Recv, _uiNotifier, diag => diag.ErrorCode.AbsoluteValue + " - " + diag.ErrorCode.KnownValue.ToText(), "ER", "?");
            var diagnostic4SensorErrorCodeChart = new ChartParamViewModel<IMukFlapDiagnosticOneWireSensor, string>(diagnostic4Recv, diagnostic4SensorErrorCodeDisp, diag => diag.ErrorCode.AbsoluteValue, ParameterLogType.Analogue, _parameterLogger, muk08Group.DisplayName, reply03Group.DisplayName, groupDiagnostic4.DisplayName);
            groupDiagnostic4.AddParameterOrGroup(diagnostic4SensorErrorCodeChart);


            var diagnostic4SensorErrorsCountDisp = new DispParamViewModel<string, IMukFlapDiagnosticOneWireSensor>("Количество ошибок связи", diagnostic4Recv, _uiNotifier, diag => diag.LinkErrorCount.Value.ToString(), "ER", "?");
            var diagnostic4SensorErrorsCountChart = new ChartParamViewModel<IMukFlapDiagnosticOneWireSensor, string>(diagnostic4Recv, diagnostic4SensorErrorsCountDisp, diag => diag.LinkErrorCount.Value, ParameterLogType.Analogue, _parameterLogger, muk08Group.DisplayName, reply03Group.DisplayName, groupDiagnostic4.DisplayName);
            groupDiagnostic4.AddParameterOrGroup(diagnostic4SensorErrorsCountChart);


            IRecvParam<IList<byte>> mukFlapWinterSummerFwVer = new RecvParam<IList<byte>, IList<byte>>("Версия ПО МУК заслонки", _cmdListenerEvaporator03Reply, bytes => bytes.Skip(39).Take(2).ToList());
            var mukFlapWinterSummerFwVerDisplay = new DispParamViewModel<int, IList<byte>>(mukFlapWinterSummerFwVer.ReceiveName, mukFlapWinterSummerFwVer, _uiNotifier, bytes => bytes[0] * 256 + bytes[1], 0, -1);
            var mukFlapWinterSummerFwVerChart = new ChartParamViewModel<IList<byte>, int>(mukFlapWinterSummerFwVer, mukFlapWinterSummerFwVerDisplay, data => data[0] * 256.0 + data[1], ParameterLogType.Analogue, _parameterLogger, muk08Group.DisplayName, reply03Group.DisplayName);
            reply03Group.AddParameterOrGroup(mukFlapWinterSummerFwVerChart);


            // setting group of settable params
            var setParamsGroup = new GroupParamViewModel("Уставка параметров");

            var setParam827 = new SettParamViewModel<bool>("827: Ручной/Автоматический режим", _uiNotifier, val => { }, false, false, (val, callback) => _parameterSetter.SetParameterAsync(827, (ushort) (val ? 1 : 0), callback));
            var aggSetParam827 = new AggregateParamViewModel<bool>(setParam827, muk08Group.DisplayName, setParamsGroup.DisplayName);


            var setParam828 = new SettParamViewModel<int>("828: Уставка ШИМ на заслонку в ручном режиме", _uiNotifier, val =>
            {
                if (val < 0 || val > 255) throw new ArgumentOutOfRangeException();
            }, 0, 0, (val, callback) => _parameterSetter.SetParameterAsync(828, (ushort) val, callback));
            var aggSetParam828 = new AggregateParamViewModel<int>(setParam828, muk08Group.DisplayName, setParamsGroup.DisplayName);


            setParamsGroup.AddParameterOrGroup(aggSetParam827);
            setParamsGroup.AddParameterOrGroup(aggSetParam828);

            */


            muk03Group.AddParameterOrGroup(reply03Group);
            //muk08Group.AddParameterOrGroup(setParamsGroup);
            return muk03Group;
        }
    }
}
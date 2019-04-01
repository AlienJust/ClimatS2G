using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Conversion.Contracts;
using AlienJust.Support.Numeric.Bits;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Request16;
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
            var muk08Group = new GroupParamViewModel("МУК 3, вентилятор испарителя");
            //const string reply03GroupName = "МУК 8, заслонка зима лето";
            var reply03Group = new GroupParamViewModel("Ответ на команду 3");


            IRecvParam<IMukFanVaporizerDataReply03> mukFanData = new RecvParam<IMukFanVaporizerDataReply03, IMukFanVaporizerDataReply03>("Muk0303Rp",
                _cmdListenerEvaporator03Reply, data => data);
            
            var mukFlapWinterSummerPwmDisplay =
                new DispParamViewModel<int, IMukFanVaporizerDataReply03>("Уставка ШИМ на клапан", mukFanData,
                    _uiNotifier, data => data.FanPwm, 0, -1);
            var mukFlapWinterSummerPwmChart = new ChartParamViewModel<IMukFanVaporizerDataReply03, int>(mukFanData,
                mukFlapWinterSummerPwmDisplay, data => data.FanPwm, ParameterLogType.Analogue, _parameterLogger,
                muk08Group.DisplayName, reply03Group.DisplayName);
            reply03Group.AddParameterOrGroup(mukFlapWinterSummerPwmChart);


            //IRecvParam<IList<byte>> mukFlapWinterTemperatureOneWireAddr1 = new RecvParam<IList<byte>, IList<byte>>("T1W1", _cmdListenerEvaporator03Reply, bytes => bytes.Skip(3).Take(2).ToList());
            var mukFlapWinterTemperatureOneWireAddr1Display = new DispParamViewModel<string, IMukFanVaporizerDataReply03>(
                "Температура 1w адрес 1", mukFanData, _uiNotifier,
                data => data.TemperatureAddress1.ToString(d => d.ToString("f2")), "ER", "??");
            var mukFlapWinterTemperatureOneWireAddr1Chart = new ChartParamViewModel<IMukFanVaporizerDataReply03, string>(
                mukFanData, mukFlapWinterTemperatureOneWireAddr1Display,
                data => data.TemperatureAddress1.Indication, ParameterLogType.Analogue, _parameterLogger,
                muk08Group.DisplayName, reply03Group.DisplayName);
            reply03Group.AddParameterOrGroup(mukFlapWinterTemperatureOneWireAddr1Chart);


            //IRecvParam<IList<byte>> mukFlapWinterTemperatureOneWireAddr2 = new RecvParam<IList<byte>, IList<byte>>("T1W2", _cmdListenerEvaporator03Reply, bytes => bytes.Skip(5).Take(2).ToList());
            var mukFlapWinterTemperatureOneWireAddr2Display = new DispParamViewModel<string, IMukFanVaporizerDataReply03>(
                "Температура 1w адрес 2", mukFanData, _uiNotifier,
                data => data.TemperatureAddress1.ToString(d => d.ToString("f2")), "ER", "??");
            
            var mukFlapWinterTemperatureOneWireAddr2Chart =
                new ChartParamViewModel<IMukFanVaporizerDataReply03, string>(mukFanData,
                    mukFlapWinterTemperatureOneWireAddr2Display,
                    data => data.TemperatureAddress2.Indication, ParameterLogType.Analogue, _parameterLogger,
                    muk08Group.DisplayName, reply03Group.DisplayName);
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
                muk08Group.DisplayName, reply03Group.DisplayName, groupIncomingSignals.DisplayName);
            groupIncomingSignals.AddParameterOrGroup(mukFanVaporizerIsFaultFuseChart);


            var mukFanVaporizerIsFaultTemp = new DispParamViewModel<bool, byte>(
                "Нарушение целостности предохранителя вентилятора испарителя", mukFlapWinterIncomingSignals,
                _uiNotifier, incomingByte => incomingByte.GetBit(1),
                false, false);
            var mukFanVaporizerIsFaultTempChart = new ChartParamViewModel<byte, bool>(
                mukFlapWinterIncomingSignals, mukFanVaporizerIsFaultTemp,
                data => data.GetBit(1) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                muk08Group.DisplayName, reply03Group.DisplayName, groupIncomingSignals.DisplayName);
            groupIncomingSignals.AddParameterOrGroup(mukFanVaporizerIsFaultTempChart);
            
            
            var mukFanVaporizerIsFaultHeaterOn = new DispParamViewModel<bool, byte>(
                "Нарушение целостности предохранителя вентилятора испарителя", mukFlapWinterIncomingSignals,
                _uiNotifier, incomingByte => incomingByte.GetBit(1),
                false, false);
            var mukFanVaporizerIsFaultHeaterOnChart = new ChartParamViewModel<byte, bool>(
                mukFlapWinterIncomingSignals, mukFanVaporizerIsFaultHeaterOn,
                data => data.GetBit(1) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger,
                muk08Group.DisplayName, reply03Group.DisplayName, groupIncomingSignals.DisplayName);
            groupIncomingSignals.AddParameterOrGroup(mukFanVaporizerIsFaultHeaterOnChart);
            

            /*

            IRecvParam<IList<byte>> mukFlapWinterOutgoingSignals = new RecvParam<IList<byte>, IList<byte>>("Байт выходных сигналов", _cmdListenerEvaporator03Reply, bytes => bytes.Skip(9).Take(1).ToList());
            var mukFlapWinterOutgoingSignalsDisplay = new DispParamViewModel<int, IList<byte>>(mukFlapWinterOutgoingSignals.ReceiveName, mukFlapWinterOutgoingSignals, _uiNotifier, bytes => bytes[0], 0, -1);
            var mukFlapWinterOutgoingSignalsChart = new ChartParamViewModel<IList<byte>, int>(mukFlapWinterOutgoingSignals, mukFlapWinterOutgoingSignalsDisplay, data => data[0], ParameterLogType.Analogue, _parameterLogger, muk08Group.DisplayName, reply03Group.DisplayName);
            reply03Group.AddParameterOrGroup(mukFlapWinterOutgoingSignalsChart);


            IRecvParam<IList<byte>> mukFlapWinterSummerAnalogueInput = new RecvParam<IList<byte>, IList<byte>>("Аналоговый вход от заслонки", _cmdListenerEvaporator03Reply, bytes => bytes.Skip(11).Take(2).ToList());
            var mukFlapWinterSummerAnalogueInputDisplay = new DispParamViewModel<string, IList<byte>>(mukFlapWinterSummerAnalogueInput.ReceiveName, mukFlapWinterSummerAnalogueInput, _uiNotifier, data => ((data[0] * 256 + data[1]) * 0.1).ToString("f1"), "ER", "??");
            var mukFlapWinterSummerAnalogueInputChart = new ChartParamViewModel<IList<byte>, string>(mukFlapWinterSummerAnalogueInput, mukFlapWinterSummerAnalogueInputDisplay, data => (data[0] * 256 + data[1]) * 0.1, ParameterLogType.Analogue, _parameterLogger, muk08Group.DisplayName, reply03Group.DisplayName);
            reply03Group.AddParameterOrGroup(mukFlapWinterSummerAnalogueInputChart);


            IRecvParam<IList<byte>> mukFlapWinterSummerAutomaticModeStage = new RecvParam<IList<byte>, IList<byte>>("Этап работы", _cmdListenerEvaporator03Reply, bytes => bytes.Skip(13).Take(2).ToList());
            var mukFlapWinterSummerAutomaticModeStageDisplay = new DispParamViewModel<string, IList<byte>>(mukFlapWinterSummerAutomaticModeStage.ReceiveName, mukFlapWinterSummerAutomaticModeStage, _uiNotifier, data => new MukFlapWorkmodeStageBuilder(data[0] * 256 + data[1]).Build().ToText(), "ER", "??");
            var mukFlapWinterSummerAutomaticModeStageChart = new ChartParamViewModel<IList<byte>, string>(mukFlapWinterSummerAutomaticModeStage, mukFlapWinterSummerAutomaticModeStageDisplay, data => data[0] * 256 + data[1], ParameterLogType.Analogue, _parameterLogger, muk08Group.DisplayName, reply03Group.DisplayName);
            reply03Group.AddParameterOrGroup(mukFlapWinterSummerAutomaticModeStageChart);


            var groupDiagnostic1 = new GroupParamViewModel("Диагностика 1");
            reply03Group.AddParameterOrGroup(groupDiagnostic1);
            IRecvParam<IList<byte>> mukFlapWinterSummerDiagnostic1 = new RecvParam<IList<byte>, IList<byte>>(groupDiagnostic1.DisplayName, _cmdListenerEvaporator03Reply, bytes => bytes.Skip(15).Take(2).ToList());

            
            var diagnostic1OneWire1ErrorDisplay = new DispParamViewModel<bool, IList<byte>>("Ошибка датчика 1w №1", mukFlapWinterSummerDiagnostic1, _uiNotifier, bytes => bytes[1].GetBit(6), false, false);
            var diagnostic1OneWire1ErrorChart = new ChartParamViewModel<IList<byte>, bool>(mukFlapWinterSummerDiagnostic1, diagnostic1OneWire1ErrorDisplay, data => data[1].GetBit(6) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger, muk08Group.DisplayName, reply03Group.DisplayName, groupDiagnostic1.DisplayName);
            groupDiagnostic1.ParametersAndGroups.Add(diagnostic1OneWire1ErrorChart);


            var diagnostic1OneWire2ErrorDisplay = new DispParamViewModel<bool, IList<byte>>("Ошибка датчика 1w №2", mukFlapWinterSummerDiagnostic1, _uiNotifier, bytes => bytes[1].GetBit(7), false, false);
            var diagnostic1OneWire2ErrorChart = new ChartParamViewModel<IList<byte>, bool>(mukFlapWinterSummerDiagnostic1, diagnostic1OneWire2ErrorDisplay, data => data[1].GetBit(7) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger, muk08Group.DisplayName, reply03Group.DisplayName, groupDiagnostic1.DisplayName);
            groupDiagnostic1.ParametersAndGroups.Add(diagnostic1OneWire2ErrorChart);


            IRecvParam<IList<byte>> diagnostic2Recv = new RecvParam<IList<byte>, IList<byte>>("Диагностика 2", _cmdListenerEvaporator03Reply, bytes => bytes.Skip(17).Take(2).ToList());
            var groupDiagnostic2 = new GroupParamViewModel(diagnostic2Recv.ReceiveName);
            reply03Group.AddParameterOrGroup(groupDiagnostic2);

            var diagnostic2NotReachingLimitDisp = new DispParamViewModel<bool, IList<byte>>("Заслонка не встает в крайние положения", diagnostic2Recv, _uiNotifier, bytes => bytes[1].GetBit(5), false, false);
            var diagnostic2NotReachingLimitChart = new ChartParamViewModel<IList<byte>, bool>(diagnostic2Recv, diagnostic2NotReachingLimitDisp, data => data[1].GetBit(5) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger, muk08Group.DisplayName, reply03Group.DisplayName, groupDiagnostic2.DisplayName);
            groupDiagnostic2.AddParameterOrGroup(diagnostic2NotReachingLimitChart);

            var diagnostic2NotCoveringHalfOfTheRangeDisp = new DispParamViewModel<bool, IList<byte>>("Заслонка не проходит 50% диапазона", diagnostic2Recv, _uiNotifier, bytes => bytes[1].GetBit(6), false, false);
            var diagnostic2NotCoveringHalfOfTheRangeChart = new ChartParamViewModel<IList<byte>, bool>(diagnostic2Recv, diagnostic2NotCoveringHalfOfTheRangeDisp, data => data[1].GetBit(6) ? 1.0 : 0.0, ParameterLogType.Discrete, _parameterLogger, muk08Group.DisplayName, reply03Group.DisplayName, groupDiagnostic2.DisplayName);
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


            muk08Group.AddParameterOrGroup(reply03Group);
            //muk08Group.AddParameterOrGroup(setParamsGroup);
            return muk08Group;
        }
    }
}
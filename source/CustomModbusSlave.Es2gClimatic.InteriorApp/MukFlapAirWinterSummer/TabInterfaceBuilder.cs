using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Conversion.Contracts;
using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.OneWire;
using CustomModbusSlave.Es2gClimatic.Shared.OneWire.Diagnostic;
using CustomModbusSlave.Es2gClimatic.Shared.Search;
using CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm;
using DrillingRig.ConfigApp.AppControl.ParamLogger;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirWinterSummer {
	public class TabInterfaceBuilder : IBuilderManyToOne<IDisplayGroup> {
		private readonly IThreadNotifier _uiNotifier;
		private readonly SearchViewModel _svm;
		private readonly ICmdListener<IList<byte>> cmdListenerWinSum03Reply;
		private readonly IParameterLogger ParameterLogger;

		public TabInterfaceBuilder(IThreadNotifier mainWindowUiUiNotifier, ICmdListener<IList<byte>> cmdListenerWinSum03Reply, IParameterLogger parameterLogger) {
			_uiNotifier = mainWindowUiUiNotifier;
			this.cmdListenerWinSum03Reply = cmdListenerWinSum03Reply;
			ParameterLogger = parameterLogger;
		}

		public IDisplayGroup Build() {
			const string muk8GroupName = "МУК 8, заслонка зима лето";
			var groupMuk8 = new GroupParamViewModel(muk8GroupName);
			
			

			IRecvParam<IList<byte>> mukFlapWinterSummerPwm = new RecvParam<IList<byte>, IList<byte>>("PWM", cmdListenerWinSum03Reply, bytes => bytes.Skip(1).Take(2).ToList());
			var mukFlapWinterSummerPwmDisplay = new DispParamViewModel<int, IList<byte>>("Уставка ШИМ на клапан", mukFlapWinterSummerPwm, _uiNotifier, bytes => bytes[0] * 256 + bytes[1], 0, -1);
			var mukFlapWinterSummerPwmChart = new ChartParamViewModel<IList<byte>, int>(muk8GroupName, mukFlapWinterSummerPwm, mukFlapWinterSummerPwmDisplay, data => data[0] * 256.0 + data[1], ParameterLogType.Analogue, ParameterLogger);
			groupMuk8.AddParameterOrGroup(mukFlapWinterSummerPwmChart);


			
			IRecvParam<IList<byte>> mukFlapWinterTemperatureOneWireAddr1 = new RecvParam<IList<byte>, IList<byte>>("T1W1", cmdListenerWinSum03Reply, bytes => bytes.Skip(3).Take(2).ToList());
			var mukFlapWinterTemperatureOneWireAddr1Display = new DispParamViewModel<string, IList<byte>>("Температура 1w адрес 1", mukFlapWinterTemperatureOneWireAddr1, _uiNotifier, bytes => new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(bytes[0], bytes[1]), 0.01, 0.0, new BytesPair(0x85, 0x00)).ToString(d => d.ToString("f2")), "ER", "??");
			var mukFlapWinterTemperatureOneWireAddr1Chart = new ChartParamViewModel<IList<byte>, string>(muk8GroupName, mukFlapWinterTemperatureOneWireAddr1, mukFlapWinterTemperatureOneWireAddr1Display, data => new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(data[0], data[1]), 0.01, 0.0, new BytesPair(0x85, 0x00)).Indication, ParameterLogType.Analogue, ParameterLogger);
			groupMuk8.AddParameterOrGroup(mukFlapWinterTemperatureOneWireAddr1Chart);


			
			IRecvParam<IList<byte>> mukFlapWinterTemperatureOneWireAddr2 = new RecvParam<IList<byte>, IList<byte>>("T1W2", cmdListenerWinSum03Reply, bytes => bytes.Skip(5).Take(2).ToList());
			var mukFlapWinterTemperatureOneWireAddr2Display = new DispParamViewModel<string, IList<byte>>("Температура 1w адрес 2", mukFlapWinterTemperatureOneWireAddr2, _uiNotifier, bytes => new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(bytes[0], bytes[1]), 0.01, 0.0, new BytesPair(0x85, 0x00)).ToString(d => d.ToString("f2")), "ER", "??");
			var mukFlapWinterTemperatureOneWireAddr2Chart = new ChartParamViewModel<IList<byte>, string>(muk8GroupName, mukFlapWinterTemperatureOneWireAddr2, mukFlapWinterTemperatureOneWireAddr2Display, data => new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(data[0], data[1]), 0.01, 0.0, new BytesPair(0x85, 0x00)).Indication, ParameterLogType.Analogue, ParameterLogger);
			groupMuk8.AddParameterOrGroup(mukFlapWinterTemperatureOneWireAddr2Chart);

			
			
			IRecvParam<IList<byte>> mukFlapWinterIncomingSignals = new RecvParam<IList<byte>, IList<byte>>("Байт входных сигналов", cmdListenerWinSum03Reply, bytes => bytes.Skip(7).Take(1).ToList());
			var groupIncomingSignals = new GroupParamViewModel(mukFlapWinterIncomingSignals.ReceiveName);
			groupMuk8.AddParameterOrGroup(groupIncomingSignals);
			
			var mukFlapWinterIsigTurnEmrson1OnDisplay = new DispParamViewModel<bool, IList<byte>>("Сигнал на включение Emersion1", mukFlapWinterIncomingSignals, _uiNotifier, bytes => bytes[0].GetBit(6), false, false);
			var mukFlapWinterIsigTurnEmrson1OnChart = new ChartParamViewModel<IList<byte>, bool>(muk8GroupName + ", " + groupIncomingSignals.DisplayName, mukFlapWinterIncomingSignals, mukFlapWinterIsigTurnEmrson1OnDisplay, data => data[0].GetBit(6) ? 1.0 : 0.0, ParameterLogType.Discrete, ParameterLogger);
			groupIncomingSignals.AddParameterOrGroup(mukFlapWinterIsigTurnEmrson1OnChart);
			


			var mukFlapWinterIsigTurnEmrson2OnDisplay = new DispParamViewModel<bool, IList<byte>>("Сигнал на включение Emersion2", mukFlapWinterIncomingSignals, _uiNotifier, bytes => bytes[0].GetBit(7), false, false);
			var mukFlapWinterIsigTurnEmrson2OnChart = new ChartParamViewModel<IList<byte>, bool>(muk8GroupName + ", " + groupIncomingSignals.DisplayName, mukFlapWinterIncomingSignals, mukFlapWinterIsigTurnEmrson2OnDisplay, data => data[0].GetBit(7) ? 1.0 : 0.0, ParameterLogType.Discrete, ParameterLogger);
			groupIncomingSignals.AddParameterOrGroup(mukFlapWinterIsigTurnEmrson2OnChart);

			
			

			IRecvParam<IList<byte>> mukFlapWinterOutgoingSignals = new RecvParam<IList<byte>, IList<byte>>("Байт выходных сигналов", cmdListenerWinSum03Reply, bytes => bytes.Skip(9).Take(1).ToList());
			var mukFlapWinterOutgoingSignalsDisplay = new DispParamViewModel<int, IList<byte>>(mukFlapWinterOutgoingSignals.ReceiveName, mukFlapWinterOutgoingSignals, _uiNotifier, bytes => bytes[0], 0, -1);
			var mukFlapWinterOutgoingSignalsChart = new ChartParamViewModel<IList<byte>, int>(muk8GroupName, mukFlapWinterOutgoingSignals, mukFlapWinterOutgoingSignalsDisplay, data => data[0], ParameterLogType.Analogue, ParameterLogger);
			groupMuk8.AddParameterOrGroup(mukFlapWinterOutgoingSignalsChart);


			
			IRecvParam<IList<byte>> mukFlapWinterSummerAnalogueInput = new RecvParam<IList<byte>, IList<byte>>("Аналоговый вход от заслонки", cmdListenerWinSum03Reply, bytes => bytes.Skip(11).Take(2).ToList());
			var mukFlapWinterSummerAnalogueInputDisplay = new DispParamViewModel<string, IList<byte>>(mukFlapWinterSummerAnalogueInput.ReceiveName, mukFlapWinterSummerAnalogueInput, _uiNotifier, data => ((data[0] * 256 + data[1]) * 0.1).ToString("f1"), "ER", "??");
			var mukFlapWinterSummerAnalogueInputChart = new ChartParamViewModel<IList<byte>, string>(muk8GroupName, mukFlapWinterSummerAnalogueInput, mukFlapWinterSummerAnalogueInputDisplay, data => (data[0] * 256 + data[1]) * 0.1, ParameterLogType.Analogue, ParameterLogger);
			groupMuk8.AddParameterOrGroup(mukFlapWinterSummerAnalogueInputChart);


			
			IRecvParam<IList<byte>> mukFlapWinterSummerAutomaticModeStage = new RecvParam<IList<byte>, IList<byte>>("Этап работы", cmdListenerWinSum03Reply, bytes => bytes.Skip(13).Take(2).ToList());
			var mukFlapWinterSummerAutomaticModeStageDisplay = new DispParamViewModel<string, IList<byte>>(mukFlapWinterSummerAutomaticModeStage.ReceiveName, mukFlapWinterSummerAutomaticModeStage, _uiNotifier, data => new MukFlapWorkmodeStageBuilder(data[0] * 256 + data[1]).Build().ToText(), "ER", "??");
			var mukFlapWinterSummerAutomaticModeStageChart = new ChartParamViewModel<IList<byte>, string>(muk8GroupName, mukFlapWinterSummerAutomaticModeStage, mukFlapWinterSummerAutomaticModeStageDisplay, data => data[0] * 256 + data[1], ParameterLogType.Analogue, ParameterLogger);
			groupMuk8.AddParameterOrGroup(mukFlapWinterSummerAutomaticModeStageChart);

			
			
			var groupDiagnostic1 = new GroupParamViewModel("Диагностика 1");
			groupMuk8.AddParameterOrGroup(groupDiagnostic1);
			IRecvParam<IList<byte>> mukFlapWinterSummerDiagnostic1 = new RecvParam<IList<byte>, IList<byte>>(groupDiagnostic1.DisplayName, cmdListenerWinSum03Reply, bytes => bytes.Skip(15).Take(2).ToList());
			var diagnostic1NoConnectionToEmersonDisplay = new DispParamViewModel<bool, IList<byte>>("Нет связи с Emerson", mukFlapWinterSummerDiagnostic1, _uiNotifier, bytes => bytes[1].GetBit(4), false, false);
			var diagnostic1NoConnectionToEmersonChart = new ChartParamViewModel<IList<byte>, bool>(muk8GroupName + ", " + groupDiagnostic1.DisplayName, mukFlapWinterSummerDiagnostic1, diagnostic1NoConnectionToEmersonDisplay, data => data[1].GetBit(4) ? 1.0 : 0.0, ParameterLogType.Discrete, ParameterLogger);
			groupDiagnostic1.ParametersAndGroups.Add(diagnostic1NoConnectionToEmersonChart);


			
			var diagnostic1OneWire1ErrorDisplay = new DispParamViewModel<bool, IList<byte>>("Ошибка датчика 1w №1", mukFlapWinterSummerDiagnostic1, _uiNotifier, bytes => bytes[1].GetBit(6), false, false);
			var diagnostic1OneWire1ErrorChart = new ChartParamViewModel<IList<byte>, bool>(muk8GroupName + ", " + groupDiagnostic1.DisplayName, mukFlapWinterSummerDiagnostic1, diagnostic1OneWire1ErrorDisplay, data => data[1].GetBit(6) ? 1.0 : 0.0, ParameterLogType.Discrete, ParameterLogger);
			groupDiagnostic1.ParametersAndGroups.Add(diagnostic1OneWire1ErrorChart);

			

			var diagnostic1OneWire2ErrorDisplay = new DispParamViewModel<bool, IList<byte>>("Ошибка датчика 1w №2", mukFlapWinterSummerDiagnostic1, _uiNotifier, bytes => bytes[1].GetBit(7), false, false);
			var diagnostic1OneWire2ErrorChart = new ChartParamViewModel<IList<byte>, bool>(muk8GroupName + ", " + groupDiagnostic1.DisplayName, mukFlapWinterSummerDiagnostic1, diagnostic1OneWire2ErrorDisplay, data => data[1].GetBit(7) ? 1.0 : 0.0, ParameterLogType.Discrete, ParameterLogger);
			groupDiagnostic1.ParametersAndGroups.Add(diagnostic1OneWire2ErrorChart);

			
			
			IRecvParam<IList<byte>> diagnostic2Recv = new RecvParam<IList<byte>, IList<byte>>("Диагностика 2", cmdListenerWinSum03Reply, bytes => bytes.Skip(17).Take(2).ToList());
			var groupDiagnostic2 = new GroupParamViewModel(diagnostic2Recv.ReceiveName);
			groupMuk8.AddParameterOrGroup(groupDiagnostic2);
			
			var diagnostic2NotReachingLimitDisp = new DispParamViewModel<bool, IList<byte>>("Заслонка не встает в крайние положения", diagnostic2Recv, _uiNotifier, bytes => bytes[1].GetBit(5), false, false);
			var diagnostic2NotReachingLimitChart = new ChartParamViewModel<IList<byte>, bool>(muk8GroupName + ", " + groupDiagnostic2.DisplayName, diagnostic2Recv, diagnostic2NotReachingLimitDisp, data => data[1].GetBit(5) ? 1.0 : 0.0, ParameterLogType.Discrete, ParameterLogger);
			groupDiagnostic2.AddParameterOrGroup(diagnostic2NotReachingLimitChart);
			
			var diagnostic2NotCoveringHalfOfTheRangeDisp = new DispParamViewModel<bool, IList<byte>>("Заслонка не встает в крайние положения", diagnostic2Recv, _uiNotifier, bytes => bytes[1].GetBit(6), false, false);
			var diagnostic2NotCoveringHalfOfTheRangeChart = new ChartParamViewModel<IList<byte>, bool>(muk8GroupName + ", " + groupDiagnostic2.DisplayName, diagnostic2Recv, diagnostic2NotCoveringHalfOfTheRangeDisp, data => data[1].GetBit(6) ? 1.0 : 0.0, ParameterLogType.Discrete, ParameterLogger);
			groupDiagnostic2.AddParameterOrGroup(diagnostic2NotCoveringHalfOfTheRangeChart);
			
			
			
			
			
			IRecvParam<IMukFlapDiagnosticOneWireSensor> diagnostic3Recv = new RecvParam<IMukFlapDiagnosticOneWireSensor, IList<byte>>("Диагностика 3, 1w адрес 1", cmdListenerWinSum03Reply, bytes => new MukFlapDiagnosticOneWireSensorBuilder(bytes[20]).Build());
			var groupDiagnostic3 = new GroupParamViewModel(diagnostic3Recv.ReceiveName);
			groupMuk8.AddParameterOrGroup(groupDiagnostic3);
			var diagnostic3SensorErrorDisp = new DispParamViewModel<bool, IMukFlapDiagnosticOneWireSensor>("Ошибка датчика", diagnostic3Recv, _uiNotifier, d => d.OneWireSensorError, false, false);
			var diagnostic3SensorErrorChart = new ChartParamViewModel<IMukFlapDiagnosticOneWireSensor, bool>(muk8GroupName + ", " + groupDiagnostic3.DisplayName, diagnostic3Recv, diagnostic3SensorErrorDisp, diag => diag.OneWireSensorError ? 1.0 : 0.0, ParameterLogType.Discrete, ParameterLogger);
			groupDiagnostic3.AddParameterOrGroup(diagnostic3SensorErrorChart);
			
			
			var diagnostic3SensorErrorCodeDisp = new DispParamViewModel<string, IMukFlapDiagnosticOneWireSensor>("Код ошибки датчика", diagnostic3Recv, _uiNotifier, diag => diag.ErrorCode.AbsoluteValue + " - " + diag.ErrorCode.KnownValue.ToText(), "ER", "?");
			var diagnostic3SensorErrorCodeChart = new ChartParamViewModel<IMukFlapDiagnosticOneWireSensor, string>(muk8GroupName + ", " + groupDiagnostic3.DisplayName, diagnostic3Recv, diagnostic3SensorErrorCodeDisp, diag => diag.ErrorCode.AbsoluteValue, ParameterLogType.Analogue, ParameterLogger);
			groupDiagnostic3.AddParameterOrGroup(diagnostic3SensorErrorCodeChart);
			
			
			var diagnostic3SensorErrorsCountDisp = new DispParamViewModel<string, IMukFlapDiagnosticOneWireSensor>("Количество ошибок связи", diagnostic3Recv, _uiNotifier, diag => diag.LinkErrorCount.Value.ToString(), "ER", "?");
			var diagnostic3SensorErrorsCountChart = new ChartParamViewModel<IMukFlapDiagnosticOneWireSensor, string>(muk8GroupName + ", " + groupDiagnostic3.DisplayName, diagnostic3Recv, diagnostic3SensorErrorsCountDisp, diag => diag.LinkErrorCount.Value, ParameterLogType.Analogue, ParameterLogger);
			groupDiagnostic3.AddParameterOrGroup(diagnostic3SensorErrorsCountChart);
			
			
			
			
			IRecvParam<IMukFlapDiagnosticOneWireSensor> diagnostic4Recv = new RecvParam<IMukFlapDiagnosticOneWireSensor, IList<byte>>("Диагностика 4, 1w адрес 2", cmdListenerWinSum03Reply, bytes => new MukFlapDiagnosticOneWireSensorBuilder(bytes[22]).Build());
			var groupDiagnostic4 = new GroupParamViewModel(diagnostic4Recv.ReceiveName);
			groupMuk8.AddParameterOrGroup(groupDiagnostic4);
			var diagnostic4SensorErrorDisp = new DispParamViewModel<bool, IMukFlapDiagnosticOneWireSensor>("Ошибка датчика", diagnostic4Recv, _uiNotifier, d => d.OneWireSensorError, false, false);
			var diagnostic4SensorErrorChart = new ChartParamViewModel<IMukFlapDiagnosticOneWireSensor, bool>(muk8GroupName + ", " + groupDiagnostic4.DisplayName, diagnostic4Recv, diagnostic4SensorErrorDisp, diag => diag.OneWireSensorError ? 1.0 : 0.0, ParameterLogType.Discrete, ParameterLogger);
			groupDiagnostic4.AddParameterOrGroup(diagnostic4SensorErrorChart);
			
			
			var diagnostic4SensorErrorCodeDisp = new DispParamViewModel<string, IMukFlapDiagnosticOneWireSensor>("Код ошибки датчика", diagnostic4Recv, _uiNotifier, diag => diag.ErrorCode.AbsoluteValue + " - " + diag.ErrorCode.KnownValue.ToText(), "ER", "?");
			var diagnostic4SensorErrorCodeChart = new ChartParamViewModel<IMukFlapDiagnosticOneWireSensor, string>(muk8GroupName + ", " + groupDiagnostic4.DisplayName, diagnostic4Recv, diagnostic4SensorErrorCodeDisp, diag => diag.ErrorCode.AbsoluteValue, ParameterLogType.Analogue, ParameterLogger);
			groupDiagnostic4.AddParameterOrGroup(diagnostic4SensorErrorCodeChart);
			
			
			var diagnostic4SensorErrorsCountDisp = new DispParamViewModel<string, IMukFlapDiagnosticOneWireSensor>("Количество ошибок связи", diagnostic4Recv, _uiNotifier, diag => diag.LinkErrorCount.Value.ToString(), "ER", "?");
			var diagnostic4SensorErrorsCountChart = new ChartParamViewModel<IMukFlapDiagnosticOneWireSensor, string>(muk8GroupName + ", " + groupDiagnostic4.DisplayName, diagnostic4Recv, diagnostic4SensorErrorsCountDisp, diag => diag.LinkErrorCount.Value, ParameterLogType.Analogue, ParameterLogger);
			groupDiagnostic4.AddParameterOrGroup(diagnostic4SensorErrorsCountChart);
			
			
			
			
			IRecvParam<IList<byte>> mukFlapWinterSummerFwVer = new RecvParam<IList<byte>, IList<byte>>("Версия ПО МУК заслонки", cmdListenerWinSum03Reply, bytes => bytes.Skip(39).Take(2).ToList());
			var mukFlapWinterSummerFwVerDisplay = new DispParamViewModel<int, IList<byte>>(mukFlapWinterSummerFwVer.ReceiveName, mukFlapWinterSummerFwVer, _uiNotifier, bytes => bytes[0] * 256 + bytes[1], 0, -1);
			var mukFlapWinterSummerFwVerChart = new ChartParamViewModel<IList<byte>, int>(muk8GroupName, mukFlapWinterSummerFwVer, mukFlapWinterSummerFwVerDisplay, data => data[0] * 256.0 + data[1], ParameterLogType.Analogue, ParameterLogger);
			groupMuk8.AddParameterOrGroup(mukFlapWinterSummerFwVerChart);
			



			return groupMuk8;
		}
	}
}
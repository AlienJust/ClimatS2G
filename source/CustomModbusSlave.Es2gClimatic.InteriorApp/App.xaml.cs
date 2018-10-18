using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm;
using CustomModbusSlave.Es2gClimatic.InteriorApp.Bvs;
using CustomModbusSlave.Es2gClimatic.InteriorApp.Bvs2;
using CustomModbusSlave.Es2gClimatic.InteriorApp.Ksm;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.View;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.ViewModel;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirWinterSummer;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirWinterSummer.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirWinterSummer.Views;
using CustomModbusSlave.Es2gClimatic.InteriorApp.SystemDiagnostic;
using CustomModbusSlave.Es2gClimatic.InteriorApp.TestSys;
using CustomModbusSlave.Es2gClimatic.InteriorApp.TopContent;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.AppWindow;
using CustomModbusSlave.Es2gClimatic.Shared.Bvs;
using CustomModbusSlave.Es2gClimatic.Shared.Chart;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.OneWire;
using CustomModbusSlave.Es2gClimatic.Shared.Oscilloscope;
using CustomModbusSlave.Es2gClimatic.Shared.Search;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm;
using CustomModbusSlave.Es2gClimatic.Shared.TestSystems;
using CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm;
using ParamControls.Vm;
using Timer = System.Timers.Timer;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp {
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {
		private void App_OnStartup(object sender, StartupEventArgs e) {
			IWorker<Action> sharedTestWorker = new SingleThreadedRelayQueueWorkerProceedAllItemsBeforeStopNoLog<Action>("Shared Test Worker", a => {
				try {
					a();
				}
				catch (Exception exception) {
					Console.WriteLine(exception);
				}
			}, ThreadPriority.BelowNormal, true, ApartmentState.Unknown);
			var tt = new Timer(1000);
			tt.AutoReset = true;
			tt.Start();


			var colorsForGraphics = new List<Color> {
				Colors.LawnGreen,
				Colors.Red,
				Colors.Cyan,
				Colors.Yellow,
				Colors.Coral,
				Colors.LightGreen,
				Colors.HotPink,
				Colors.DeepSkyBlue,
				Colors.Gold,
				Colors.Orange,
				Colors.Violet,
				Colors.White,
				Colors.Fuchsia,
				Colors.LightSkyBlue,
				Colors.LightGray,
				Colors.Khaki,
				Colors.SpringGreen,
				Colors.Tomato,
				Colors.LightCyan,
				Colors.Goldenrod,
				Colors.SlateBlue,
				Colors.Cornsilk,
				Colors.MediumPurple,
				Colors.RoyalBlue,
				Colors.MediumVioletRed,
				Colors.MediumTurquoise
			};


			var appFactory = new AppFactory("psn.S2G-climatic-interior.xml");
			var appAbilities = appFactory.Abilities;

			// Заслонка наруж. воздуха
			var cmdListenerMukFlapOuterAirReply03 = new CmdListenerMukFlapOuterAirReply03(2, 3, 47);
			var cmdListenerMukFlapOuterAirRequest16 = new CmdListenerMukFlapOuterAirRequest16(2, 16, 21);

			// Вентилятор испарителя
			var cmdListenerMukVaporizerReply03 = new CmdListenerMukVaporizerReply03(3, 3, 41);
			var cmdListenerMukVaporizerRequest16 = new CmdListenerMukVaporizerRequest16(3, 16, 21);

			// Вентилятор конденсатора
			var cmdListenerMukCondenserFanReply03 = new CmdListenerMukCondenserFanReply03(4, 3, 29);
			var cmdListenerMukCondenserRequest16 = new CmdListenerMukCondenserFanRequest16(4, 16, 15);

			// МУК вытяжного вентилятора
			var cmdListenerMukAirExhausterReply03 = new CmdListenerMukAirExhausterReply03(6, 3, 31);
			var cmdListenerMukAirExhausterRequest16 = new CmdListenerMukAirExhausterRequest16(6, 16, 21);

			// МУК рециркуляционной заслонки
			var cmdListenerMukFlapReturnAirReply03 = new CmdListenerMukFlapReturnAirReply03(7, 3, 43);
			var cmdListenerMukFlapAirRecycleRequest16 = new CmdListenerMukFlapAirRecycleRequest16(7, 16, 21);

			// МУК заслонки зима-лето
			var cmdListenerMukFlapWinterSummerReply03 = new CmdListenerMukFlapWinterSummerReply03(8, 3, 47);

			var cmdListenerMukAirFlapWinterSummerRequest16 = new CmdListenerMukFlapAirWinterSummerRequest16(8, 16, 21);

			var cmdListenerBsSmReply32 = new CmdListenerBsSmReply32(10, 32, 47);
			var cmdListenerBsSmRequest32 = new CmdListenerBsSmRequest32(10, 32, 27);

			var cmdListenerBvs1Reply65 = new CmdListenerBvsReply65(0x1E, 65, 7);
			var cmdListenerBvs2Reply65 = new CmdListenerBvsReply65(0x1D, 65, 7);

			var cmdListenerKsmParams = new CmdListenerKsmParams(20, 16, 129);

			var cmdListenerWinSum = new CmdListenerBytes(8, 3, 47);

			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukFlapOuterAirReply03);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukFlapOuterAirRequest16);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukVaporizerReply03);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukVaporizerRequest16);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukCondenserFanReply03);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukCondenserRequest16);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukAirExhausterReply03);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukAirExhausterRequest16);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukFlapReturnAirReply03);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukFlapAirRecycleRequest16);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukFlapWinterSummerReply03);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukAirFlapWinterSummerRequest16);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerBsSmReply32);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerBsSmRequest32);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerBvs1Reply65);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerBvs2Reply65);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerKsmParams);

			appAbilities.CmdNotifierStd.AddListener(cmdListenerWinSum);


			if (appAbilities.Version == AppVersion.Full) {
				appFactory.ShowChildWindowInOwnThread(uiNotifier => {
					var chartVm = new ChartViewModel(uiNotifier, colorsForGraphics);
					appAbilities.ParamLoggerRegistrationPoint.RegisterLoggegr(chartVm); // TODO: REG on point
					var chartWindow = new WindowChart();
					chartVm.SetUpdatable(chartWindow);

					return new WindowAndClosableViewModel(chartWindow, new WindowChartViewModel(chartVm));
				});

				appFactory.ShowChildWindowInOwnThread(uiNotifier => {
					Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " > oscilloscopeWindow will be created in next line");
					var oscilloscopeWindow = new OscilloscopeWindow(colorsForGraphics);
					Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " > oscilloscopeWindow was created");
					var vm = new OscilloscopeWindowSciVm();
					appAbilities.ParamLoggerRegistrationPoint.RegisterLoggegr(oscilloscopeWindow);
					return new WindowAndClosableViewModel(oscilloscopeWindow, vm);
				});
			}

			appFactory.ShowMainWindowInOwnThread("Технический абонент, салон", appAbilities, mainVm => {
				var channel = mainVm.AddChannel("Single channel");
				mainVm.TopContent = new TopContentView {DataContext = new TopContentViewModel(mainVm.Notifier, cmdListenerBsSmReply32)};
				mainVm.AddTab(new TabItemViewModel {FullHeader = "Диагностика системы", ShortHeader = "ДС", Content = new SystemDiagnosticView {DataContext = new SystemDiagnosticViewModel(appAbilities.Version == AppVersion.Full, appAbilities.Version == AppVersion.Half || appAbilities.Version == AppVersion.Full, appAbilities.IsHourCountersVisible, mainVm.Notifier, cmdListenerMukFlapOuterAirReply03, cmdListenerMukVaporizerReply03, cmdListenerMukVaporizerRequest16, cmdListenerMukCondenserFanReply03, cmdListenerMukAirExhausterReply03, cmdListenerMukFlapReturnAirReply03, cmdListenerMukFlapWinterSummerReply03, cmdListenerBsSmRequest32, cmdListenerBsSmReply32, cmdListenerKsmParams, cmdListenerBvs1Reply65, cmdListenerBvs2Reply65)}});

				mainVm.AddTab(new TabItemViewModel {FullHeader = "Тестирование системы", ShortHeader = "ТЕСТ", Content = new TestSystemView {DataContext = new TestSystemViewModel(mainVm.Notifier, "Охлаждение 100%", new Freeze100Test(sharedTestWorker, cmdListenerMukVaporizerReply03, tt, channel.Channel.ParamSetter), AlienJust.Adaptation.WindowsPresentation.Converters.Colors.Transparent)}});
				
				var searchVm = new SearchViewModel();
				mainVm.AddTab(new TabItemViewModel {FullHeader = "Глобальный поиск параметров", ShortHeader = "ПОИСК", Content = new SearchView {DataContext = searchVm}});

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel {FullHeader = "МУК заслонки наружного воздуха", ShortHeader = "МУК 2", Content = new MukFlapDataView {DataContext = new MukFlapDataViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerMukFlapOuterAirReply03, cmdListenerMukFlapOuterAirRequest16)}});

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel {FullHeader = "МУК вентилятора испарителя", ShortHeader = "МУК 3", Content = new MukVaporizerFanDataView {DataContext = new MukVaporizerFanDataViewModelParamcentric(mainVm.Notifier, channel.Channel.ParamSetter, appAbilities.RtuParamReceiver, cmdListenerMukVaporizerReply03, cmdListenerMukVaporizerRequest16)}});

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel {FullHeader = "МУК вентилятора конденсатора", ShortHeader = "МУК 4", Content = new MukFridgeFanDataView {DataContext = new MukFridgeFanDataViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerMukCondenserFanReply03, cmdListenerMukCondenserRequest16)}});

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel {FullHeader = "МУК вытяжного вентилятора пола", ShortHeader = "МУК 6", Content = new AirExhausterDataView {DataContext = new MukAirExhausterDataViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerMukAirExhausterReply03, cmdListenerMukAirExhausterRequest16)}});

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel {FullHeader = "МУК заслонки рециркуляц. воздуха", ShortHeader = "МУК 7", Content = new MukFlapReturnAirDataView {DataContext = new MukFlapReturnAirViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerMukFlapReturnAirReply03, cmdListenerMukFlapAirRecycleRequest16)}});

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel {FullHeader = "МУК заслонки лето зима", ShortHeader = "МУК 8", Content = new MukFlapWinterSummerDataView {DataContext = new MukFlapWinterSummerViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerMukFlapWinterSummerReply03, cmdListenerMukAirFlapWinterSummerRequest16)}});

				if (appAbilities.Version == AppVersion.Full) {
					IRecvParam<IList<byte>> mukFlapWinterSummerPwm = new RecvParam<IList<byte>, IList<byte>>("PWM", cmdListenerWinSum, bytes => bytes.Skip(1).Take(2).ToList());
					IRecvParam<IList<byte>> mukFlapWinterTemperatureOneWireAddr1 = new RecvParam<IList<byte>, IList<byte>>("T1W1", cmdListenerWinSum, bytes => bytes.Skip(3).Take(2).ToList());
					IRecvParam<IList<byte>> mukFlapWinterTemperatureOneWireAddr2 = new RecvParam<IList<byte>, IList<byte>>("T1W2", cmdListenerWinSum, bytes => bytes.Skip(5).Take(2).ToList());
					IRecvParam<IList<byte>> mukFlapWinterIncomingSignals = new RecvParam<IList<byte>, IList<byte>>("ISigs", cmdListenerWinSum, bytes => bytes.Skip(7).Take(1).ToList());
					IRecvParam<IList<byte>> mukFlapWinterOutgoingSignals = new RecvParam<IList<byte>, IList<byte>>("OSigs", cmdListenerWinSum, bytes => bytes.Skip(9).Take(1).ToList());
					IRecvParam<IList<byte>> mukFlapWinterSummerAnalogueInput = new RecvParam<IList<byte>, IList<byte>>("AInput", cmdListenerWinSum, bytes => bytes.Skip(11).Take(2).ToList());
					IRecvParam<IList<byte>> mukFlapWinterSummerAutomaticModeStage = new RecvParam<IList<byte>, IList<byte>>("AModeStage", cmdListenerWinSum, bytes => bytes.Skip(13).Take(2).ToList());
					IRecvParam<IList<byte>> mukFlapWinterSummerDiagnostic1 = new RecvParam<IList<byte>, IList<byte>>("Diag1", cmdListenerWinSum, bytes => bytes.Skip(15).Take(2).ToList());
					IRecvParam<IList<byte>> mukFlapWinterSummerDiagnostic2 = new RecvParam<IList<byte>, IList<byte>>("Diag2", cmdListenerWinSum, bytes => bytes.Skip(17).Take(2).ToList());
					IRecvParam<IList<byte>> mukFlapWinterSummerDiagnostic3 = new RecvParam<IList<byte>, IList<byte>>("Diag3", cmdListenerWinSum, bytes => bytes.Skip(19).Take(2).ToList());
					IRecvParam<IList<byte>> mukFlapWinterSummerDiagnostic4 = new RecvParam<IList<byte>, IList<byte>>("Diag4", cmdListenerWinSum, bytes => bytes.Skip(21).Take(2).ToList());


					const string muk8GroupName = "МУК8";
					var mukFlapWinterSummerPwmDisplay = new DispParamViewModel<int, IList<byte>>("Уставка ШИМ на клапан", mukFlapWinterSummerPwm, mainVm.Notifier, bytes => bytes[0] * 256 + bytes[1], 0, -1);

					var mukFlapWinterTemperatureOneWireAddr1Display = new DispParamViewModel<string, IList<byte>>("Температура 1w адрес 1", mukFlapWinterTemperatureOneWireAddr1, mainVm.Notifier, bytes => new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(bytes[0], bytes[1]), 0.01, 0.0, new BytesPair(0x85, 0x00)).ToString(d => d.ToString("f2")), "ER", "??");

					var mukFlapWinterTemperatureOneWireAddr2Display = new DispParamViewModel<string, IList<byte>>("Температура 1w адрес 2", mukFlapWinterTemperatureOneWireAddr2, mainVm.Notifier, bytes => new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(bytes[0], bytes[1]), 0.01, 0.0, new BytesPair(0x85, 0x00)).ToString(d => d.ToString("f2")), "ER", "??");

					var mukFlapWinterOutgoingSignalsDisplay = new DispParamViewModel<int, IList<byte>>("Байт выходных сигналов", mukFlapWinterOutgoingSignals, mainVm.Notifier, bytes => bytes[0], 0, -1);

					var mukFlapWinterSummerAnalogueInputDisplay = new DispParamViewModel<string, IList<byte>>("Аналоговый вход от заслонки", mukFlapWinterSummerAnalogueInput, mainVm.Notifier, data => ((data[0] * 256 + data[1]) * 0.1).ToString("f1"), "ER", "??");

					var mukFlapWinterSummerAutomaticModeStageDisplay = new DispParamViewModel<string, IList<byte>>("Этап работы", mukFlapWinterSummerAutomaticModeStage, mainVm.Notifier, data => new MukFlapWorkmodeStageBuilder(data[0] * 256 + data[1]).Build().ToText(), "ER", "??");


					var groupIncomingSignals = new GroupParamViewModel("Байт входных сигналов", new ObservableCollection<IChartReadyParameterViewModel>());

					var mukFlapWinterIsigTurnEmrson1On = new DispParamViewModel<bool, IList<byte>>("Сигнал на включение Emersion1", mukFlapWinterIncomingSignals, mainVm.Notifier, bytes => bytes[0].GetBit(6), false, false);
					var mukFlapWinterIsigTurnEmrson2On = new DispParamViewModel<bool, IList<byte>>("Сигнал на включение Emersion2", mukFlapWinterIncomingSignals, mainVm.Notifier, bytes => bytes[0].GetBit(7), false, false);


					groupIncomingSignals.GroupItems.Add(new ChartParamViewModel<IList<byte>, bool>(muk8GroupName + ", " + groupIncomingSignals.DisplayName, mukFlapWinterIncomingSignals, mukFlapWinterIsigTurnEmrson1On, data => data[0].GetBit(6) ? 1.0 : 0.0, ParameterLogType.Discrete, appAbilities.ParameterLogger));
					groupIncomingSignals.GroupItems.Add(new ChartParamViewModel<IList<byte>, bool>(muk8GroupName + ", " + groupIncomingSignals.DisplayName, mukFlapWinterIncomingSignals, mukFlapWinterIsigTurnEmrson2On, data => data[0].GetBit(7) ? 1.0 : 0.0, ParameterLogType.Discrete, appAbilities.ParameterLogger));

					var groupDiagnostic1 = new GroupParamViewModel("Диагностика 1", new ObservableCollection<IChartReadyParameterViewModel>());
					var diagnostic1NoConnectionToEmerson = new DispParamViewModel<bool, IList<byte>>("Нет связи с Emerson", mukFlapWinterSummerDiagnostic1, mainVm.Notifier, bytes => bytes[1].GetBit(4), false, false);
					var diagnostic1OneWire1Error = new DispParamViewModel<bool, IList<byte>>("Ошибка датчика 1w №1", mukFlapWinterSummerDiagnostic1, mainVm.Notifier, bytes => bytes[1].GetBit(6), false, false);
					var diagnostic1OneWire2Error = new DispParamViewModel<bool, IList<byte>>("Ошибка датчика 1w №2", mukFlapWinterSummerDiagnostic1, mainVm.Notifier, bytes => bytes[1].GetBit(7), false, false);

					groupDiagnostic1.GroupItems.Add(new ChartParamViewModel<IList<byte>, bool>(muk8GroupName + ", " + groupIncomingSignals.DisplayName, mukFlapWinterSummerDiagnostic1, diagnostic1NoConnectionToEmerson, data => data[1].GetBit(4) ? 1.0 : 0.0, ParameterLogType.Discrete, appAbilities.ParameterLogger));
					groupDiagnostic1.GroupItems.Add(new ChartParamViewModel<IList<byte>, bool>(muk8GroupName + ", " + groupIncomingSignals.DisplayName, mukFlapWinterSummerDiagnostic1, diagnostic1OneWire1Error, data => data[1].GetBit(6) ? 1.0 : 0.0, ParameterLogType.Discrete, appAbilities.ParameterLogger));
					groupDiagnostic1.GroupItems.Add(new ChartParamViewModel<IList<byte>, bool>(muk8GroupName + ", " + groupIncomingSignals.DisplayName, mukFlapWinterSummerDiagnostic1, diagnostic1OneWire2Error, data => data[1].GetBit(7) ? 1.0 : 0.0, ParameterLogType.Discrete, appAbilities.ParameterLogger));


					var groupDiagnostic2 = new GroupParamViewModel("Диагностика 2", null);
					var groupDiagnostic3 = new GroupParamViewModel("Диагностика 3", null);
					var groupDiagnostic4 = new GroupParamViewModel("Диагностика 4", null);

					mainVm.AddTab(new TabItemViewModel {
						FullHeader = "МУК заслонки лето зима",
						ShortHeader = "МУК 8",
						Content = new ParametersListView {
							DataContext = new GroupParamViewModel("МУК 8", new ObservableCollection<IDisplayParameter> {
								new ChartParamViewModel<IList<byte>, int>(muk8GroupName, mukFlapWinterSummerPwm, mukFlapWinterSummerPwmDisplay, data => data[0] * 256.0 + data[1], ParameterLogType.Analogue, appAbilities.ParameterLogger),
								new ChartParamViewModel<IList<byte>, string>(muk8GroupName, mukFlapWinterTemperatureOneWireAddr1, mukFlapWinterTemperatureOneWireAddr1Display, data => new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(data[0], data[1]), 0.01, 0.0, new BytesPair(0x85, 0x00)).Indication, ParameterLogType.Analogue, appAbilities.ParameterLogger),
								new ChartParamViewModel<IList<byte>, string>(muk8GroupName, mukFlapWinterTemperatureOneWireAddr2, mukFlapWinterTemperatureOneWireAddr2Display, data => new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(data[0], data[1]), 0.01, 0.0, new BytesPair(0x85, 0x00)).Indication, ParameterLogType.Analogue, appAbilities.ParameterLogger),
								groupIncomingSignals,
								new ChartParamViewModel<IList<byte>, int>(muk8GroupName, mukFlapWinterOutgoingSignals, mukFlapWinterOutgoingSignalsDisplay, data => data[0], ParameterLogType.Analogue, appAbilities.ParameterLogger),
								new ChartParamViewModel<IList<byte>, string>(muk8GroupName, mukFlapWinterSummerAnalogueInput, mukFlapWinterSummerAnalogueInputDisplay, data => (data[0] * 256 + data[1]) * 0.1, ParameterLogType.Analogue, appAbilities.ParameterLogger),
								new ChartParamViewModel<IList<byte>, string>(muk8GroupName, mukFlapWinterSummerAutomaticModeStage, mukFlapWinterSummerAutomaticModeStageDisplay, data => data[0] * 256 + data[1], ParameterLogType.Analogue, appAbilities.ParameterLogger),
								groupDiagnostic1,
								groupDiagnostic2,
								groupDiagnostic3,
								groupDiagnostic4
							})
						}
					});
				}

				if (appAbilities.Version == AppVersion.Full || appAbilities.Version == AppVersion.Half)
					mainVm.AddTab(new TabItemViewModel {FullHeader = "БС-СМ", ShortHeader = "БС-СМ", Content = new BsSmDataView {DataContext = new BsSmDataViewModel(mainVm.Notifier, cmdListenerBsSmRequest32, cmdListenerBsSmReply32)}});

				if (appAbilities.Version == AppVersion.Full || appAbilities.Version == AppVersion.Half)
					mainVm.AddTab(new TabItemViewModel {FullHeader = "БВС1", ShortHeader = "БВС1", Content = new BvsDataView {DataContext = new BvsDataViewModel(mainVm.Notifier, cmdListenerBvs1Reply65)}});

				if (appAbilities.Version == AppVersion.Full || appAbilities.Version == AppVersion.Half)
					mainVm.AddTab(new TabItemViewModel {FullHeader = "БВС2", ShortHeader = "БВС2", Content = new Bvs2DataView {DataContext = new BvsDataViewModel(mainVm.Notifier, cmdListenerBvs2Reply65)}});

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel {FullHeader = "КСМ", ShortHeader = "КСМ", Content = new KsmDataView {DataContext = new KsmDataViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerKsmParams)}});
			});
		}
	}
}
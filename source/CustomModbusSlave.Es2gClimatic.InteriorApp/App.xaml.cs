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
				
				/*
				appFactory.ShowChildWindowInOwnThread(uiNotifier => {
					Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " > oscilloscopeWindow will be created in next line");
					var oscilloscopeWindow = new OscilloscopeWindow(colorsForGraphics);
					Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " > oscilloscopeWindow was created");
					var vm = new OscilloscopeWindowSciVm();
					appAbilities.ParamLoggerRegistrationPoint.RegisterLoggegr(oscilloscopeWindow);
					return new WindowAndClosableViewModel(oscilloscopeWindow, vm);
				});
				*/
			}

			appFactory.ShowMainWindowInOwnThread("Технический абонент, салон", appAbilities, mainVm => {
				var channel = mainVm.AddChannel("Single channel");
				mainVm.TopContent = new TopContentView { DataContext = new TopContentViewModel(mainVm.Notifier, cmdListenerBsSmReply32) };
				
				var searchVm = new SearchViewModel(mainVm.Notifier); // Search View Model inited first to add items in future
				
				var tabsBuilderSd = new SdTabInterfaceBuilder(mainVm.Notifier, cmdListenerKsmParams, appAbilities.ParameterLogger, channel.Channel.ParamSetter, appAbilities.Version);
				var tabVmSd = tabsBuilderSd.Build();
				
				mainVm.AddTab(new TabItemViewModel { FullHeader = "Диагностика системы", ShortHeader = "ДС", Content = new SystemDiagnosticView { DataContext = new SystemDiagnosticViewModel(appAbilities.Version == AppVersion.Full, appAbilities.Version == AppVersion.Half || appAbilities.Version == AppVersion.Full, appAbilities.IsHourCountersVisible, mainVm.Notifier, cmdListenerMukFlapOuterAirReply03, cmdListenerMukVaporizerReply03, cmdListenerMukVaporizerRequest16, cmdListenerMukCondenserFanReply03, cmdListenerMukAirExhausterReply03, cmdListenerMukFlapReturnAirReply03, cmdListenerMukFlapWinterSummerReply03, cmdListenerBsSmRequest32, cmdListenerBsSmReply32, cmdListenerKsmParams, cmdListenerBvs1Reply65, cmdListenerBvs2Reply65, tabVmSd) } });

				if (appAbilities.Version == AppVersion.Full) {


					mainVm.AddTab(new TabItemViewModel {FullHeader = "Тестирование системы", ShortHeader = "ТЕСТ", Content = new TestSystemView {DataContext = new TestSystemViewModel(mainVm.Notifier, "Охлаждение 100%", new Freeze100Test(sharedTestWorker, cmdListenerMukVaporizerReply03, tt, channel.Channel.ParamSetter), AlienJust.Adaptation.WindowsPresentation.Converters.Colors.Transparent)}});
				}


				if (appAbilities.Version == AppVersion.Full) {
					mainVm.AddTab(new TabItemViewModel {FullHeader = "Глобальный поиск параметров", ShortHeader = "ПОИСК", Content = new SearchView {DataContext = searchVm}});
				}

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel { FullHeader = "МУК заслонки наружного воздуха", ShortHeader = "МУК 2", Content = new MukFlapDataView { DataContext = new MukFlapDataViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerMukFlapOuterAirReply03, cmdListenerMukFlapOuterAirRequest16) } });

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel { FullHeader = "МУК вентилятора испарителя", ShortHeader = "МУК 3", Content = new MukVaporizerFanDataView { DataContext = new MukVaporizerFanDataViewModelParamcentric(mainVm.Notifier, channel.Channel.ParamSetter, appAbilities.RtuParamReceiver, cmdListenerMukVaporizerReply03, cmdListenerMukVaporizerRequest16) } });

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel { FullHeader = "МУК вентилятора конденсатора", ShortHeader = "МУК 4", Content = new MukFridgeFanDataView { DataContext = new MukFridgeFanDataViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerMukCondenserFanReply03, cmdListenerMukCondenserRequest16) } });

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel { FullHeader = "МУК вытяжного вентилятора пола", ShortHeader = "МУК 6", Content = new AirExhausterDataView { DataContext = new MukAirExhausterDataViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerMukAirExhausterReply03, cmdListenerMukAirExhausterRequest16) } });

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel { FullHeader = "МУК заслонки рециркуляц. воздуха", ShortHeader = "МУК 7", Content = new MukFlapReturnAirDataView { DataContext = new MukFlapReturnAirViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerMukFlapReturnAirReply03, cmdListenerMukFlapAirRecycleRequest16) } });

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel { FullHeader = "МУК заслонки лето зима", ShortHeader = "МУК 8", Content = new MukFlapWinterSummerDataView { DataContext = new MukFlapWinterSummerViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerMukFlapWinterSummerReply03, cmdListenerMukAirFlapWinterSummerRequest16) } });

				if (appAbilities.Version == AppVersion.Full) {
					var tabsBuilderMuk8 = new MukFlapAirWinterSummer.TabInterfaceBuilder(mainVm.Notifier, cmdListenerWinSum, cmdListenerMukAirFlapWinterSummerRequest16, appAbilities.ParameterLogger, channel.Channel.ParamSetter);
					var tabVmMuk8 = tabsBuilderMuk8.Build();
					searchVm.RegisterTopLevelGroup(tabVmMuk8);
					mainVm.AddTab(new TabItemViewModel {FullHeader = "МУК заслонки лето зима", ShortHeader = "МУК 8", Content = new ParametersListView {DataContext = tabVmMuk8}});
				}
				
				if (appAbilities.Version == AppVersion.Full) {
					var tabsBuilderKsm = new Ksm.KsmTabInterfaceBuilder(mainVm.Notifier, cmdListenerKsmParams, appAbilities.ParameterLogger, channel.Channel.ParamSetter, appAbilities.Version);
					var tabVmKsm = tabsBuilderKsm.Build();
					searchVm.RegisterTopLevelGroup(tabVmKsm);
					mainVm.AddTab(new TabItemViewModel {FullHeader = "КСМ и в Африке КСМ", ShortHeader = "КСМ", Content = new ParametersListView {DataContext = tabVmKsm}});
				}
				

				if (appAbilities.Version == AppVersion.Full || appAbilities.Version == AppVersion.Half)
					mainVm.AddTab(new TabItemViewModel {
						FullHeader = "БС-СМ", ShortHeader = "БС-СМ", Content = new BsSmDataView { DataContext = new BsSmDataViewModel(mainVm.Notifier, cmdListenerBsSmRequest32, cmdListenerBsSmReply32) }
					});

				if (appAbilities.Version == AppVersion.Full || appAbilities.Version == AppVersion.Half)
					mainVm.AddTab(new TabItemViewModel { FullHeader = "БВС1", ShortHeader = "БВС1", Content = new BvsDataView { DataContext = new BvsDataViewModel(mainVm.Notifier, cmdListenerBvs1Reply65) } });

				if (appAbilities.Version == AppVersion.Full || appAbilities.Version == AppVersion.Half)
					mainVm.AddTab(new TabItemViewModel { FullHeader = "БВС2", ShortHeader = "БВС2", Content = new Bvs2DataView { DataContext = new BvsDataViewModel(mainVm.Notifier, cmdListenerBvs2Reply65) } });

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel { FullHeader = "КСМ", ShortHeader = "КСМ", Content = new KsmDataView { DataContext = new KsmDataViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerKsmParams) } });
			});
		}
	}
}
using System.Windows;
using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm;
using CustomModbusSlave.Es2gClimatic.InteriorApp.Bvs;
using CustomModbusSlave.Es2gClimatic.InteriorApp.Bvs2;
using CustomModbusSlave.Es2gClimatic.InteriorApp.Ksm;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.View;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.ViewModel;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirWinterSummer.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer.Views;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFridge;
using CustomModbusSlave.Es2gClimatic.InteriorApp.SystemDiagnostic;
using CustomModbusSlave.Es2gClimatic.InteriorApp.TopContent;
using CustomModbusSlave.Es2gClimatic.Shared.AppWindow;
using CustomModbusSlave.Es2gClimatic.Shared.Bvs;
using CustomModbusSlave.Es2gClimatic.Shared.MukCondenser.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp {
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {
		private void App_OnStartup(object sender, StartupEventArgs e) {
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

			appFactory.ShowMainWindowInOwnThread("Технический абонент, салон", appAbilities, mainVm => {
				var channel = mainVm.AddChannel("Single channel");

				mainVm.TopContent = new TopContentView {DataContext = new TopContentViewModel(mainVm.Notifier, cmdListenerBsSmReply32) };

				mainVm.AddTab(new TabItemViewModel {
					FullHeader = "Диагностика системы",
					ShortHeader = "ДС",
					Content = new SystemDiagnosticView {
						DataContext = new SystemDiagnosticViewModel(
							appAbilities.Version == AppVersion.Full,
							appAbilities.Version == AppVersion.Half || appAbilities.Version == AppVersion.Full,
								mainVm.Notifier,
								cmdListenerMukFlapOuterAirReply03,
								cmdListenerMukVaporizerReply03,
								cmdListenerMukVaporizerRequest16,
								cmdListenerMukCondenserFanReply03,
								cmdListenerMukAirExhausterReply03,
								cmdListenerMukFlapReturnAirReply03,
								cmdListenerMukFlapWinterSummerReply03,
								cmdListenerBsSmReply32,
								cmdListenerKsmParams,
								cmdListenerBvs1Reply65,
								cmdListenerBvs2Reply65)
					}
				});

				if (appAbilities.Version == AppVersion.Full)
				mainVm.AddTab(new TabItemViewModel {
					FullHeader = "МУК заслонки наружного воздуха",
					ShortHeader = "МУК 2",
					Content = new MukFlapDataView { DataContext = new MukFlapDataViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerMukFlapOuterAirReply03, cmdListenerMukFlapOuterAirRequest16) }
				});

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel {
					FullHeader = "МУК вентилятора испарителя",
					ShortHeader = "МУК 3",
					Content = new MukVaporizerFanDataView {
						DataContext = new MukVaporizerFanDataViewModelParamcentric(
							mainVm.Notifier, channel.Channel.ParamSetter,
							appAbilities.RtuParamReceiver,
							cmdListenerMukVaporizerReply03,
							cmdListenerMukVaporizerRequest16
						)
					}
				});

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel {
					FullHeader = "МУК вентилятора конденсатора",
					ShortHeader = "МУК 4",
					Content = new MukFridgeFanDataView {
						DataContext = new MukFridgeFanDataViewModel(
							mainVm.Notifier, channel.Channel.ParamSetter,
							cmdListenerMukCondenserFanReply03,
							cmdListenerMukCondenserRequest16
						)
					}
				});

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel {
					FullHeader = "МУК вытяжного вентилятора пола",
					ShortHeader = "МУК 6",
					Content = new AirExhausterDataView {
						DataContext = new MukAirExhausterDataViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerMukAirExhausterReply03, cmdListenerMukAirExhausterRequest16)
					}
				});

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel {
					FullHeader = "МУК заслонки рециркуляц. воздуха",
					ShortHeader = "МУК 7",
					Content = new MukFlapReturnAirDataView {
						DataContext = new MukFlapReturnAirViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerMukFlapReturnAirReply03, cmdListenerMukFlapAirRecycleRequest16)
					}
				});

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel {
					FullHeader = "МУК заслонки лето зима",
					ShortHeader = "МУК 8",
					Content = new MukFlapWinterSummerDataView {
						DataContext = new MukFlapWinterSummerViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerMukFlapWinterSummerReply03, cmdListenerMukAirFlapWinterSummerRequest16)
					}
				});

				if (appAbilities.Version == AppVersion.Full || appAbilities.Version == AppVersion.Half)
					mainVm.AddTab(new TabItemViewModel {
					FullHeader = "БС-СМ",
					ShortHeader = "БС-СМ",
					Content = new BsSmDataView {
						DataContext = new BsSmDataViewModel(
							mainVm.Notifier, cmdListenerBsSmRequest32, cmdListenerBsSmReply32)
					}
				});

				if (appAbilities.Version == AppVersion.Full || appAbilities.Version == AppVersion.Half)
					mainVm.AddTab(new TabItemViewModel {
					FullHeader = "БВС1",
					ShortHeader = "БВС1",
					Content = new BvsDataView {
						DataContext = new BvsDataViewModel(mainVm.Notifier, cmdListenerBvs1Reply65)
					}
				});

				if (appAbilities.Version == AppVersion.Full || appAbilities.Version == AppVersion.Half)
					mainVm.AddTab(new TabItemViewModel {
					FullHeader = "БВС2",
					ShortHeader = "БВС2",
					Content = new Bvs2DataView {
						DataContext = new BvsDataViewModel(mainVm.Notifier, cmdListenerBvs2Reply65)
					}
				});

				if (appAbilities.Version == AppVersion.Full)
					mainVm.AddTab(new TabItemViewModel {
					FullHeader = "КСМ",
					ShortHeader = "КСМ",
					Content = new KsmDataView {
						DataContext = new KsmDataViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerKsmParams)
					}
				});
			});
		}
	}
}

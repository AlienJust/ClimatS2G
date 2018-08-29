using System;
using System.Windows;
using CustomModbusSlave.Es2gClimatic.CabinApp.BsSm;
using CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Reply32;
using CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Request32;
using CustomModbusSlave.Es2gClimatic.CabinApp.Bvs;
using CustomModbusSlave.Es2gClimatic.CabinApp.Ksm;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Request16;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFridge;
using CustomModbusSlave.Es2gClimatic.Shared.AppWindow;
using CustomModbusSlave.Es2gClimatic.Shared.Bvs;
using CustomModbusSlave.Es2gClimatic.Shared.MukCondenser.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm;
using CustomModbusSlave.MicroclimatEs2gApp.MukWarmFloor;
using MahApps.Metro;

namespace CustomModbusSlave.Es2gClimatic.CabinApp {
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {
		protected override void OnStartup(StartupEventArgs e) {
			// get the current app style (theme and accent) from the application
			// you can then use the current theme and custom accent instead set a new theme
			Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);

			// now set the Green accent and dark theme
			ThemeManager.ChangeAppStyle(Application.Current,
				ThemeManager.GetAccent("Green"),
				//ThemeManager.GetAppTheme("BaseDark")); // or appStyle.Item1
				ThemeManager.GetAppTheme("BaseLight")); // or appStyle.Item1

			base.OnStartup(e);
		}

		private void App_OnStartup(object sender, StartupEventArgs e) {
			var appFactory = new AppFactory("psn.S2G-climatic-cabin.xml");
			var appAbilities = appFactory.Abilities;

			var cmdListenerMukFlapAirReply03 = new CmdListenerMukFlapAirReply03(2, 3, 39);
			var cmdListenerMukFlapAirRequest16 = new CmdListenerMukFlapAirRequest16(2, 16, 21);

			var cmdListenerMukVaporizerReply03 = new CmdListenerMukVaporizerReply03(3, 3, 41);
			var cmdListenerMukVaporizerRequest16 = new CmdListenerMukVaporizerRequest16(3, 16, 21);

			var cmdListenerMukCondenserFanReply03 = new CmdListenerMukCondenserFanReply03(4, 3, 29);
			var cmdListenerMukCondenserRequest16 = new CmdListenerMukCondenserFanRequest16(4, 16, 15);

			var cmdListenerMukWarmFloorReply03 = new CmdListenerMukWarmFloorReply03(5, 3, 31);
			var cmdListenerMukWarmFloorRequest16 = new CmdListenerMukWarmFloorRequest16(5, 16, 21);

			var cmdListenerBsSmRequest32 = new CmdListenerBsSmRequest32(6,32,21);
			var cmdListenerBsSmReply32 = new CmdListenerBsSmReply32(6, 32, 20);

			var cmdListenerBvsReply65 = new CmdListenerBvsReply65(0x1E, 65, 7);
			var cmdListenerKsm50Params = new CmdListenerKsmParams(20, 16, 109);

			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukFlapAirReply03);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukFlapAirRequest16);

			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukVaporizerReply03);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukVaporizerRequest16);

			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukCondenserFanReply03);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukCondenserRequest16);

			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukWarmFloorReply03);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerMukWarmFloorRequest16);

			appAbilities.CmdNotifierStd.AddListener(cmdListenerBsSmRequest32);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerBsSmReply32);

			appAbilities.CmdNotifierStd.AddListener(cmdListenerBvsReply65);
			appAbilities.CmdNotifierStd.AddListener(cmdListenerKsm50Params);

			//var channel = appAbilities.CreateChannel("Cabin the only one channel");

			appFactory.ShowMainWindowInOwnThread("Технический абонент, кабина", appAbilities, mainVm => {
				var channel = mainVm.AddChannel("Single channel");
				//var channel1 = mainVm.AddChannel("Single 2 channel");
				mainVm.AddTab(new TabItemViewModel {
					FullHeader = "Диагностика системы",
					ShortHeader = "ДС"
				});

				mainVm.AddTab(new TabItemViewModel {
					FullHeader = "МУК заслонки",
					ShortHeader = "МУК 2",
					Content = new MukFlapDataView { DataContext = new MukFlapDataViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerMukFlapAirReply03, cmdListenerMukFlapAirRequest16) }
				});

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

				mainVm.AddTab(new TabItemViewModel {
					FullHeader = "МУК тёплого пола",
					ShortHeader = "МУК 5",
					Content = new MukWarmFloorDataView {
						DataContext = new MukWarmFloorDataViewModel(
							mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerMukWarmFloorReply03, cmdListenerMukWarmFloorRequest16)
					}
				});

				mainVm.AddTab(new TabItemViewModel {
					FullHeader = "БС-СМ",
					ShortHeader = "БС-СМ",
					Content = new BsSmDataView {
						DataContext = new BsSmDataViewModel(
							mainVm.Notifier, cmdListenerBsSmRequest32, cmdListenerBsSmReply32)
					}
				});

				mainVm.AddTab(new TabItemViewModel {
					FullHeader = "БВС",
					ShortHeader = "БВС",
					Content = new BvsDataView {
						DataContext = new BvsDataViewModel(mainVm.Notifier, cmdListenerBvsReply65)
					}
				});

				mainVm.AddTab(new TabItemViewModel {
					FullHeader = "КСМ",
					ShortHeader = "КСМ",
					Content = new KsmDataView {
						DataContext = new KsmDataViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerKsm50Params)
					}
				});
			});
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using AlienJust.Adaptation.ConsoleLogger;
using AlienJust.Adaptation.WindowsPresentation;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Text;
using AlienJust.Support.Text.Contracts;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbus.Slave.FastReply.Queued;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.AppWindow;
using DataAbstractionLevel.Low.PsnConfig;
using MahApps.Metro;

namespace CustomModbusSlave.Es2gClimatic.CabinApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
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

		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			var af = new AppFactory("psn.Микроклимат-ЭС2ГП-кабина.xml");
			var ab = af.Abilities;
			af.ShowMainWindowInOwnThread("Технический абонент, кабина", ab, mainVm =>
			{
				var tab1 = new TabItemViewModel
				{
					FullHeader = "МУК 123456",
					ShortHeader = "МУК 1",
					Content = new MukFlapDataView {DataContext = new MukFlapDataViewModel(mainVm.Notifier, ab.ParamSetter)}
				};
				mainVm.AddTab(tab1);
			});

			//var appThreadNotifier = new WpfUiNotifierAsync(System.Windows.Threading.Dispatcher.CurrentDispatcher);
		}
	}
}

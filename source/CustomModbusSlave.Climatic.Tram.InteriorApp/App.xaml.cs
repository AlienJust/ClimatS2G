using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using CustomModbusSlave.Climatic.Tram.InteriorApp;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.AppWindow;
using CustomModbusSlave.Es2gClimatic.Shared.Chart;
using CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation;
using CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var appFactory = new AppFactory("psn.TRAM-climatic-interior.xml");
            var appAbilities = appFactory.Abilities;

            var paramPresenter = appAbilities.GetParametersPresentation("Tram.Iterior.Tabs.xml");

            /*
            var listeners = new List<Tuple<IPsnProtocolCommandPartConfiguration, ICmdListener<ICmdPartConfigAndBytes>>>();
            foreach (var cmdPart in appAbilities.PsnProtocolConfiguration.CommandParts)
            {
                var listener = new CmdListenerCmdPartAndBytes(cmdPart);
                listeners.Add(new Tuple<IPsnProtocolCommandPartConfiguration, ICmdListener<ICmdPartConfigAndBytes>>(cmdPart, listener));
                appAbilities.CmdNotifierStd.AddListener(listener);
            }
            */



            if (appAbilities.Version == AppVersion.Full)
            {
                appFactory.ShowChildWindowInOwnThread(uiNotifier =>
                {
                    var colorsForGraphics = new List<Color>
                    {
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

                    var chartVm = new ChartViewModel(uiNotifier, colorsForGraphics);
                    appAbilities.ParamLoggerRegistrationPoint.RegisterLoggegr(chartVm); // TODO: REG on point
                    var chartWindow = new WindowChart();
                    chartVm.SetUpdatable(chartWindow);

                    return new WindowAndClosableViewModel(chartWindow, new WindowChartViewModel(chartVm));
                });
            }


            appFactory.ShowMainWindowInOwnThread("Технический абонент, салон трамвая", appAbilities, mainVm =>
            {
                try
                {
                    var channel = mainVm.AddChannel("Single channel");
                    foreach (var paramViewAndKey in paramPresenter.Parameters)
                    {
                        mainVm.AddParameter(paramViewAndKey.Key, paramViewAndKey.Value, appAbilities.PsnProtocolConfigurationParams[paramViewAndKey.Value.Identifier]);
                    }

                    //var mainContentVm = new MainContentViewModel(mainVm.Parameters, mainVm.Notifier, mainVm);
                    //mainVm.MainContent = new MainContentView { DataContext = mainContentVm };

                    mainVm.MainContent = new MainContentView();
                    mainVm.TopContent = new UserControl1();
                    //mainVm.MainContent = new UserControl1();
                    //mainVm.TopContent = new MainContentView();
                    Console.WriteLine("mainVm.MainContent was setted up");
                    //ParametersPresenterXmlSerializer.Serialize("123.xml", appAbilities.PsnProtocolConfiguration, false);
                    //mainVm.MainContent = new MainContentView { DataContext = mainVm };
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
        }
    }
}
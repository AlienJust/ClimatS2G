using CustomModbusSlave.Es2gClimatic.Shared.AppWindow;
using CustomModbusSlave.Es2gClimatic.Shared.Chart;
using CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation;
using DataAbstractionLevel.Low.PsnConfig.Contracts;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace CustomModbusSlave.Climatic.TramUkvzM2App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var appFactory = new AppFactory("psn.TRAM-Horizon-Interior.xml");
            var appAbilities = appFactory.Abilities;
            var paramPresenter = appAbilities.GetParametersPresentation("tabs.tramhorizoninterior.xml");

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


            appFactory.ShowMainWindowInOwnThread("Технический абонент, Трамвай Горизонт салон", appAbilities, mainVm =>
            {
                try
                {
                    ParametersPresenterXmlSerializer.Serialize("123_tramhorizoninter.xml", appAbilities.PsnProtocolConfiguration, false);

                    mainVm.UseCustomContent = true;
                    var channel = mainVm.AddChannel("Single channel");
                    foreach (var paramDescriptionAndKey in paramPresenter.Parameters)
                    {
                        mainVm.AddParameter(
                            paramDescriptionAndKey.Key,
                            paramDescriptionAndKey.Value,
                            appAbilities.PsnProtocolConfigurationParams[paramDescriptionAndKey.Value.Identifier],
                            channel.ParameterSetter);
                    }

                    foreach (var cmdPartCfg in appAbilities.PsnProtocolConfiguration.CommandParts)
                    {
                        int address = (int)cmdPartCfg.Address.DefinedValue;
                        int command = (int)cmdPartCfg.CommandCode.DefinedValue;
                        var key = "cmdpart_" + address.ToString("d3") + "_" + command.ToString("d3") +
                            (cmdPartCfg.PartType == PsnProtocolCommandPartType.Request ? "_request" : "_reply");
                        mainVm.AddCommandPart(key, cmdPartCfg);
                        Console.WriteLine(key);
                    }

                    mainVm.MainContent = new MainContentView { DataContext = mainVm };
                    Console.WriteLine("mainVm.MainContent was setted up");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
        }
    }
}
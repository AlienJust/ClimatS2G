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
using DataAbstractionLevel.Low.PsnConfig.Contracts;

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
                        mainVm.AddCommandPart(key,cmdPartCfg);
                        Console.WriteLine(key);
                    }

                    mainVm.MainContent = new MainContentView { DataContext = mainVm };
                    Console.WriteLine("mainVm.MainContent was setted up");
                    //ParametersPresenterXmlSerializer.Serialize("123.xml", appAbilities.PsnProtocolConfiguration, false);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
        }
    }
}
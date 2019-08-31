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
                var channel = mainVm.AddChannel("Single channel");


                foreach (var paramViewAndKey in paramPresenter.Parameters)
                {
                    mainVm.AddParameter(paramViewAndKey.Key, paramViewAndKey.Value, appAbilities.PsnProtocolConfigurationParams[paramViewAndKey.Value.Identifier]);
                }
                /*

                foreach (var device in appAbilities.PsnProtocolConfiguration.PsnDevices)
                {
                    //var commandPartsForCurrentDevice = listeners.Where(t => (byte)t.Item1.Address.DefinedValue == device.Address);
                    var devGroup = new GroupParamViewModel(device.Name);

                    foreach (var cmdPartAndListener in commandPartsForCurrentDevice)
                    {
                        // each cmdpart is group:
                        var cmdPartGroup = new GroupParamViewModel(cmdPartAndListener.Item1.PartName);

                        int goodSignalsCount = 0;
                        foreach (var param in cmdPartAndListener.Item1.VarParams)
                        {
                            if (param.Name.StartsWith("#")) continue;
                            goodSignalsCount++;
                            var recvParam00 = new RecvParam<double, ICmdPartConfigAndBytes>(
                                        param.Name,
                                        cmdPartAndListener.Item2,
                                        data => data.CmdPartConfig.VarParams.First(p => p.Id.IdentyString == param.Id.IdentyString).GetValue(data.DataBytes.ToArray(), 0));

                            if (param.ParameterView != null && param.ParameterView.Count > 0)
                            {
                                // TODO: more types handling, also need to implement writable params
                                foreach (var view in param.ParameterView)
                                {
                                    if (!param.IsBitSignal)
                                    {
                                        var dispParam00 = new DispParamViewModel<string, double>(recvParam00.ReceiveName, recvParam00,
                                            mainVm.Notifier, data => view.GetText(data), "ER", "?"); // TODO: string format from props

                                        var chartParam00 = new ChartParamViewModel<double, string>(recvParam00,
                                        dispParam00, data => data, ParameterLogType.Analogue, appAbilities.ParameterLogger,
                                        cmdPartGroup.DisplayName);
                                        cmdPartGroup.AddParameterOrGroup(chartParam00);
                                    }
                                    else
                                    {
                                        var dispParam00 =
                                        new DispParamViewModel<string, double>(recvParam00.ReceiveName, recvParam00,
                                        mainVm.Notifier, data => view.GetText(data), "ER", "?"); // TODO: string format from props
                                        var chartParam00 = new ChartParamViewModel<double, string>(recvParam00,
                                            dispParam00, data => data, ParameterLogType.Discrete, appAbilities.ParameterLogger,
                                            cmdPartGroup.DisplayName);
                                        cmdPartGroup.AddParameterOrGroup(chartParam00);
                                    }
                                }
                            }
                            else
                            {
                                if (!param.IsBitSignal)
                                {
                                    var dispParam00 =
                                    new DispParamViewModel<string, double>(recvParam00.ReceiveName, recvParam00,
                                        mainVm.Notifier, data => data.ToString("f2"), "ER", "?"); // TODO: string format from props
                                    var chartParam00 = new ChartParamViewModel<double, string>(recvParam00,
                                        dispParam00, data => data, ParameterLogType.Analogue, appAbilities.ParameterLogger,
                                        cmdPartGroup.DisplayName);
                                    cmdPartGroup.AddParameterOrGroup(chartParam00);
                                }
                                else
                                {
                                    var dispParam00 =
                                        new DispParamViewModel<string, double>(recvParam00.ReceiveName, recvParam00,
                                            mainVm.Notifier, data => data > 0.5 ? "True" : "False", "ER", "?"); // TODO: string format from props
                                    var chartParam00 = new ChartParamViewModel<double, string>(recvParam00,
                                        dispParam00, data => data, ParameterLogType.Discrete, appAbilities.ParameterLogger,
                                        cmdPartGroup.DisplayName);
                                    cmdPartGroup.AddParameterOrGroup(chartParam00);
                                }
                            }
                        }
                        if (goodSignalsCount > 0)
                            devGroup.AddParameterOrGroup(cmdPartGroup);
                        
                    }
                    mainVm.AddTab(new TabItemViewModel { FullHeader = device.Name, ShortHeader = "0x" + device.Address.ToString("X2"), Content = new ParametersListView { DataContext = (IDisplayGroup)devGroup } });
                    
                }*/
                var mainContentVm = new MainContentViewModel(mainVm.Parameters, mainVm.Notifier, mainVm);
                mainVm.MainContent = new MainContentView { DataContext = mainContentVm };

                //ParametersPresenterXmlSerializer.Serialize("123.xml", appAbilities.PsnProtocolConfiguration, false);
                //mainVm.MainContent = new MainContentView { DataContext = mainVm };
            });
        }
    }
}
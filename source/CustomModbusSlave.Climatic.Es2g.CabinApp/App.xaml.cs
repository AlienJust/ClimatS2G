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
using CustomModbusSlave.Es2gClimatic.CabinApp.SystemDiagnostic;
using CustomModbusSlave.Es2gClimatic.CabinApp.TopContent;
using CustomModbusSlave.Es2gClimatic.Shared.AppWindow;
using CustomModbusSlave.Es2gClimatic.Shared.Bvs;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm;

namespace CustomModbusSlave.Es2gClimatic.CabinApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var appFactory = new AppFactory("psn.S2G-climatic-cabin.xml");
            var appAbilities = appFactory.Abilities;

            var cmdListenerMukFlapAirReply03 = new CmdListenerMukFlapAirReply03(2, 3, 39);
            var cmdListenerMukFlapAirRequest16 = new CmdListenerMukFlapAirRequest16(2, 16, 21);

            var cmdListenerMukVaporizerReply03 = new CmdListenerMukVaporizerReply03(3, 3, 41);
            var cmdListenerMukVaporizerRequest16 = new CmdListenerMukVaporizerRequest16(3, 16, 21);

            var cmdListenerMukWarmFloorReply03 = new CmdListenerMukWarmFloorReply03(5, 3, 31);
            var cmdListenerMukWarmFloorRequest16 = new CmdListenerMukWarmFloorRequest16(5, 16, 21);

            var cmdListenerBsSmRequest32 = new CmdListenerBsSmRequest32(6, 32, 21);
            var cmdListenerBsSmReply32 = new CmdListenerBsSmReply32(6, 32, 20);

            var cmdListenerBvsReply65 = new CmdListenerBvsReply65(0x1E, 65, 7);
            var cmdListenerKsm50Params = new CmdListenerKsmParams(20, 16, 109);

            appAbilities.CmdNotifierStd.AddListener(cmdListenerMukFlapAirReply03);
            appAbilities.CmdNotifierStd.AddListener(cmdListenerMukFlapAirRequest16);

            appAbilities.CmdNotifierStd.AddListener(cmdListenerMukVaporizerReply03);
            appAbilities.CmdNotifierStd.AddListener(cmdListenerMukVaporizerRequest16);

            appAbilities.CmdNotifierStd.AddListener(cmdListenerMukWarmFloorReply03);
            appAbilities.CmdNotifierStd.AddListener(cmdListenerMukWarmFloorRequest16);

            appAbilities.CmdNotifierStd.AddListener(cmdListenerBsSmRequest32);
            appAbilities.CmdNotifierStd.AddListener(cmdListenerBsSmReply32);

            appAbilities.CmdNotifierStd.AddListener(cmdListenerBvsReply65);
            appAbilities.CmdNotifierStd.AddListener(cmdListenerKsm50Params);


            appFactory.ShowMainWindowInOwnThread("Технический абонент, кабина", appAbilities, mainVm =>
            {
                var channel = mainVm.AddChannel("Single channel");
                mainVm.TopContent = new TopContentView { DataContext = new TopContentViewModel(mainVm.Notifier, cmdListenerBsSmReply32) };

                mainVm.AddTab(new TabItemViewModel
                {
                    FullHeader = "Диагностика системы",
                    ShortHeader = "ДС",
                    Content = new SystemDiagnosticView
                    {
                        DataContext = new SystemDiagCabinViewModel(
                            appAbilities.Version == AppVersion.Full,
                            appAbilities.Version == AppVersion.Half || appAbilities.Version == AppVersion.Full,
                            appAbilities.IsHourCountersVisible, mainVm.Notifier, cmdListenerMukFlapAirReply03,
                            cmdListenerMukVaporizerReply03, cmdListenerMukVaporizerRequest16,
                            cmdListenerMukWarmFloorReply03,
                            cmdListenerKsm50Params,
                            cmdListenerBsSmRequest32,
                            cmdListenerBsSmReply32,
                            cmdListenerBvsReply65)
                    }
                });

                if (appAbilities.Version == AppVersion.Full)
                {
                    mainVm.AddTab(new TabItemViewModel
                    {
                        FullHeader = "МУК заслонки",
                        ShortHeader = "МУК 2",
                        Content = new MukFlapDataView
                        {
                            DataContext = new MukFlapDataViewModel(mainVm.Notifier, channel.Channel.ParamSetter,
                                cmdListenerMukFlapAirReply03, cmdListenerMukFlapAirRequest16)
                        }
                    });

                    mainVm.AddTab(new TabItemViewModel
                    {
                        FullHeader = "МУК вентилятора испарителя",
                        ShortHeader = "МУК 3",
                        Content = new MukVaporizerFanDataView
                        {
                            DataContext = new MukVaporizerFanDataViewModelParamcentric(
                                mainVm.Notifier, channel.Channel.ParamSetter,
                                appAbilities.RtuParamReceiver,
                                cmdListenerMukVaporizerReply03,
                                cmdListenerMukVaporizerRequest16
                            )
                        }
                    });
                    mainVm.AddTab(new TabItemViewModel
                    {
                        FullHeader = "МУК тёплого пола",
                        ShortHeader = "МУК 5",
                        Content = new MukWarmFloorDataView
                        {
                            DataContext = new MukWarmFloorDataViewModel(
                                mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerMukWarmFloorReply03, cmdListenerMukWarmFloorRequest16)
                        }
                    });

                    mainVm.AddTab(new TabItemViewModel
                    {
                        FullHeader = "БС-СМ",
                        ShortHeader = "БС-СМ",
                        Content = new BsSmDataView
                        {
                            DataContext = new BsSmDataViewModel(
                                mainVm.Notifier, cmdListenerBsSmRequest32, cmdListenerBsSmReply32)
                        }
                    });

                    mainVm.AddTab(new TabItemViewModel
                    {
                        FullHeader = "БВС",
                        ShortHeader = "БВС",
                        Content = new BvsDataView
                        {
                            DataContext = new BvsDataViewModel(mainVm.Notifier, cmdListenerBvsReply65)
                        }
                    });

                    mainVm.AddTab(new TabItemViewModel
                    {
                        FullHeader = "КСМ",
                        ShortHeader = "КСМ",
                        Content = new KsmDataView
                        {
                            DataContext = new KsmDataViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerKsm50Params)
                        }
                    });
                }
            });

        }
    }
}
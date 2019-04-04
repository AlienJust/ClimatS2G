using System.Windows;
using CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator;
using CustomModbusSlave.Es2gClimatic.CabinTgmApp.SystemDiagnostic;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.AppWindow;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm;
using CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm;
using CmdListenerMukVaporizerReply03 = CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.Reply03.CmdListenerMukVaporizerReply03;
using CmdListenerMukVaporizerRequest16 = CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.Request16.CmdListenerMukVaporizerRequest16;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var appFactory = new AppFactory("psn.TGM-climatic-cabin.xml");
            var appAbilities = appFactory.Abilities;
            
            var cmdListenerMukVaporizerReply03 = new CmdListenerMukVaporizerReply03(3, 3, 41);
            appAbilities.CmdNotifierStd.AddListener(cmdListenerMukVaporizerReply03);
            
            var cmdListenerMukVaporizerRequest16 = new CmdListenerMukVaporizerRequest16(3, 16, 21);
            appAbilities.CmdNotifierStd.AddListener(cmdListenerMukVaporizerRequest16);

            var cmdListenerFcRequestF0 = new CmdListenerBytes(8, 0xF0, 6);
            appAbilities.CmdNotifierStd.AddListener(cmdListenerFcRequestF0);
            
            var cmdListenerFcReplyF0 = new CmdListenerBytes(8, 0xF0, 20);
            appAbilities.CmdNotifierStd.AddListener(cmdListenerFcReplyF0);

            var cmdListenerKsm50Params = new CmdListenerKsmParams(20, 16, 109);
            appAbilities.CmdNotifierStd.AddListener(cmdListenerKsm50Params);
            
            
            appFactory.ShowMainWindowInOwnThread("Технический абонент, кабина ТГМ6", appAbilities, mainVm =>
            {
                var channel = mainVm.AddChannel("Single channel");
                mainVm.AddTab(new TabItemViewModel
                {
                    FullHeader = "Диагностика системы",
                    ShortHeader = "ДС",
                    Content = new SystemDiagnosticView
                    {
                        DataContext = new SystemDiagCabinTgmViewModel(
                            appAbilities.Version == AppVersion.Full,
                            appAbilities.Version == AppVersion.Half || appAbilities.Version == AppVersion.Full,
                            appAbilities.IsHourCountersVisible, mainVm.Notifier,
                            cmdListenerMukVaporizerReply03, cmdListenerMukVaporizerRequest16,
                            cmdListenerKsm50Params)
                    }
                });
                
                var tabsBuilderMuk3 = new TabInterfaceBuilder(mainVm.Notifier, cmdListenerMukVaporizerReply03,
                    cmdListenerMukVaporizerRequest16, appAbilities.ParameterLogger,
                    channel.Channel.ParamSetter);
                var tabVmMuk3 = tabsBuilderMuk3.Build();
                //searchVm.RegisterTopLevelGroup(tabVmMuk8);
                mainVm.AddTab(new TabItemViewModel
                {
                    FullHeader = "МУК вентилятора испарителя", ShortHeader = "МУК 3",
                    Content = new ParametersListView {DataContext = tabVmMuk3}
                });

                mainVm.AddTab(new TabItemViewModel
                {
                    FullHeader = "Преобразователь частоты",
                    ShortHeader = "ПЧ",
                    /*
                    Content = new FriquencyModifierView
                    {
                        DataContext = new FriquencyModifierViewModel(
                            mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerFriquencyModifierRequest08, cmdListenerFriquencyModifierReply08)
                    }*/
                });
                
                if (appAbilities.Version == AppVersion.Full) {
                    var tabsBuilderKsm = new Ksm.KsmTabInterfaceBuilder(mainVm.Notifier, cmdListenerKsm50Params, appAbilities.ParameterLogger, channel.Channel.ParamSetter, appAbilities.Version);
                    var tabVmKsm = tabsBuilderKsm.Build();
                    //searchVm.RegisterTopLevelGroup(tabVmKsm);
                    mainVm.AddTab(new TabItemViewModel {FullHeader = "КСМ и в Африке КСМ", ShortHeader = "КСМ", Content = new ParametersListView {DataContext = tabVmKsm}});
                }
            });
        }
    }
}
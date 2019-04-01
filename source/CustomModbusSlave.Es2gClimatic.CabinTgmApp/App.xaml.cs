using System.Windows;
using CustomModbusSlave.Es2gClimatic.CabinApp.Ksm;
using CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator;
using CustomModbusSlave.Es2gClimatic.CabinTgmApp.SystemDiagnostic;
using CustomModbusSlave.Es2gClimatic.Shared.AppWindow;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm;
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
            var appFactory = new AppFactory("psn.TGM-climatic-cabin.xml");
            var appAbilities = appFactory.Abilities;

            
            var cmdListenerMukVaporizerReply03 = new CmdListenerMukVaporizerReply03(3, 3, 41);
            appAbilities.CmdNotifierStd.AddListener(cmdListenerMukVaporizerReply03);
            
            var cmdListenerMukVaporizerRequest16 = new CmdListenerMukVaporizerRequest16(3, 16, 21);
            appAbilities.CmdNotifierStd.AddListener(cmdListenerMukVaporizerRequest16);

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

                mainVm.AddTab(new TabItemViewModel
                {
                    FullHeader = "КСМ",
                    ShortHeader = "КСМ",
                    Content = new KsmDataView
                    {
                        DataContext = new KsmDataViewModel(mainVm.Notifier, channel.Channel.ParamSetter, cmdListenerKsm50Params)
                    }
                });
            });
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.UserInterface.Contracts;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation;
using DataAbstractionLevel.Low.PsnConfig.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public interface ISharedMainViewModel : INotifyPropertyChanged, IUserInterfaceRoot
    {
        //IThreadNotifier Notifier { get; }
        IWindowSystem WindowsSystem { get; }
        ILogger Logger { get; }

        FrameworkElement TopContent { get; set; }
        FrameworkElement MainContent { get; set; }

        void AddTab(TabItemViewModel tabVm);

        ComPortControlViewModel AddChannel(string channelName);

        Dictionary<string, IParameterViewModel> Parameters { get; }

        Dictionary<string, ICommandPartViewModel> CommandParts { get; }

        void AddParameter(string key, IParameterDescription description, IPsnProtocolParameterConfigurationVariable configuration, IParameterSetter parameterSetter);

        void AddCommandPart(string key, IPsnProtocolCommandPartConfiguration config);

        bool TabHeadersAreLong { get; set; }

        bool UseCustomContent { get; set; }
    }
}
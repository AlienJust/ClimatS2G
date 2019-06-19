using System.Windows;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.UserInterface.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public interface ISharedMainViewModel
    {
        IThreadNotifier Notifier { get; }
        IWindowSystem WindowsSystem { get; }
        ILogger Logger { get; }

        FrameworkElement TopContent { get; set; }
        void AddTab(TabItemViewModel tabVm);

        ComPortControlViewModel AddChannel(string channelName);
    }
}
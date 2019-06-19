using System;
using System.Collections.ObjectModel;
using System.Windows;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using AlienJust.Support.UserInterface.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.ProgamLog;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    sealed class SharedMainViewModel : ViewModelBase, IUserInterfaceRoot, ISharedMainViewModel
    {
        private readonly ISharedAppAbilities _appAbilities;
        private bool _tabHeadersAreLong;
        private FrameworkElement _topContent;
        public ProgramLogViewModel ProgramLogVm { get; }
        public IThreadNotifier Notifier { get; }
        public IWindowSystem WindowsSystem { get; }
        public ILogger Logger { get; }

        public string WindowTitle { get; }
        public ObservableCollection<TabItemViewModel> Tabs { get; }
        public ObservableCollection<ComPortControlViewModel> ComPortControlVms { get; }

        public SharedMainViewModel(IThreadNotifier notifier, IWindowSystem windowSystem, string windowTitle, ISharedAppAbilities appAbilities)
        {
            Notifier = notifier;
            WindowsSystem = windowSystem;
            _appAbilities = appAbilities;
            WindowTitle = windowTitle;

            ProgramLogVm = new ProgramLogViewModel(this);
            Logger = new RelayLogger(ProgramLogVm, new DateTimeFormatter(" > "));
            Tabs = new ObservableCollection<TabItemViewModel>();
            ComPortControlVms = new ObservableCollection<ComPortControlViewModel>();

            Logger.Log("Программа загружена");
        }

        public ComPortControlViewModel AddChannel(string channelName)
        {
            //Console.WriteLine("SharedMainViewModel.AddChannel() called");
            var result = new ComPortControlViewModel(_appAbilities, Logger, Notifier, WindowsSystem, channelName);
            ComPortControlVms.Add(result);
            return result;
        }

        public void RemoveChannel(string channelName)
        {
            // TODO: when closing app, also call IStdNotifier.RemoveChannel()
            //Console.WriteLine("SharedMainViewModel.RemoveChannel() called");
        }

        public void AddTab(TabItemViewModel tabVm)
        {
            //Console.WriteLine("SharedMainViewModel.AddTab() called");
            Tabs.Add(tabVm);
        }

        public bool TabHeadersAreLong
        {
            get => _tabHeadersAreLong;
            set
            {
                //Console.WriteLine("SharedMainViewModel.TabHeadersAreLong.Set() called");
                if (_tabHeadersAreLong != value)
                {
                    _tabHeadersAreLong = value;
                    foreach (var tabItemViewModel in Tabs)
                    {
                        tabItemViewModel.TabHeadersAreLong = value;
                    }

                    RaisePropertyChanged(() => TabHeadersAreLong);
                }
            }
        }


        public FrameworkElement TopContent
        {
            get => _topContent;
            set //=> SetProp(() => !Equals(_topContent, value), () => _topContent = value, () => TopContent);
            {
                //Console.WriteLine("SharedMainViewModel.TopContent.Set() called");
                if (!Equals(_topContent, value))
                {
                    _topContent = value;
                    RaisePropertyChanged(() => TopContent);
                }
            }
        }
    }
}
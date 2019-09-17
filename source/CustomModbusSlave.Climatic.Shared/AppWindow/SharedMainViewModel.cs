using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using AlienJust.Support.UserInterface.Contracts;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation;
using CustomModbusSlave.Es2gClimatic.Shared.ProgamLog;
using DataAbstractionLevel.Low.PsnConfig.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    sealed class SharedMainViewModel : ViewModelBase, ISharedMainViewModel
    {
        private readonly ISharedAppAbilities _appAbilities;

        public Dictionary<string, IParameterViewModel> Parameters { get; }
        public Dictionary<string, ICommandPartViewModel> CommandParts { get; }



        private bool _tabHeadersAreLong;
        private FrameworkElement _topContent;
        private FrameworkElement _mainContent;

        public ProgramLogViewModel ProgramLogVm { get; }
        public IThreadNotifier Notifier { get; }
        public IWindowSystem WindowsSystem { get; }
        public ILogger Logger { get; }

        public string WindowTitle { get; }
        public ObservableCollection<TabItemViewModel> Tabs { get; }
        public ObservableCollection<ComPortControlViewModel> ComPortControlVms { get; }


        private bool _useCustomContent;
        public bool UseCustomContent
        {
            get => _useCustomContent;
            set
            {
                //Console.WriteLine("SharedMainViewModel.TabHeadersAreLong.Set() called");
                if (_useCustomContent != value)
                {
                    _useCustomContent = value;

                    RaisePropertyChanged(() => UseCustomContent);
                }
            }
        }

        public SharedMainViewModel(IThreadNotifier notifier, IWindowSystem windowSystem, string windowTitle, ISharedAppAbilities appAbilities)
        {
            Notifier = notifier;
            WindowsSystem = windowSystem;
            _appAbilities = appAbilities;
            WindowTitle = windowTitle;

            _tabHeadersAreLong = false;
            _useCustomContent = false;

            ProgramLogVm = new ProgramLogViewModel(this);
            Logger = new RelayLogger(ProgramLogVm, new DateTimeFormatter(" > "));
            Tabs = new ObservableCollection<TabItemViewModel>();
            ComPortControlVms = new ObservableCollection<ComPortControlViewModel>();

            Parameters = new Dictionary<string, IParameterViewModel>();
            CommandParts = new Dictionary<string, ICommandPartViewModel>();

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
            set
            {
                if (!Equals(_topContent, value))
                {
                    _topContent = value;
                    RaisePropertyChanged(() => TopContent);
                }
            }
        }

        public FrameworkElement MainContent
        {
            get => _mainContent;
            set
            {
                if (!Equals(_mainContent, value))
                {
                    _mainContent = value;
                    RaisePropertyChanged(() => MainContent);
                    Console.WriteLine("MainContent was setted");
                }
            }
        }



        public void AddParameter(string key, IParameterDescription description, Tuple<IPsnProtocolCommandPartConfiguration, IPsnProtocolParameterConfigurationVariable> configuration, IParameterSetter parameterSetter)
        {
            Parameters.Add(key, new ParameterViewModelSimple(description.CustomName, configuration.Item2.Name,
                new ParameterGetterViewModelSimple(
                    description.Identifier, _appAbilities.ParamListener, Notifier, description.View,
                    _appAbilities.ParameterLogger, configuration.Item2.IsBitSignal, 
                    configuration.Item1.PartName + ": " + configuration.Item2.Name),
                    description.Injection == null ? null : new ParameterSetterViewModelSimple(parameterSetter, Notifier, description.Injection)
                ));
        }

        public void AddCommandPart(string key, IPsnProtocolCommandPartConfiguration config)
        {
            CommandParts.Add(key, new CommandPartViewModelSimple(_appAbilities.CommandPartListener, Notifier, config));
        }
    }
}
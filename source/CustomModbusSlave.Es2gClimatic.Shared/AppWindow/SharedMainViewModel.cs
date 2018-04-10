﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using AlienJust.Support.UserInterface.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.ProgamLog;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow {
	sealed class SharedMainViewModel : ViewModelBase, IUserInterfaceRoot, ISharedMainViewModel {
		private readonly IThreadNotifier _notifier;
		private readonly IWindowSystem _windowSystem;
		private readonly ISharedAppAbilities _appAbilities;

		private readonly ProgramLogViewModel _programLogVm;
		private readonly ILogger _logger;

		private bool _tabHeadersAreLong;

		public string WindowTitle { get; }
		public ObservableCollection<TabItemViewModel> Tabs { get; }
		public ObservableCollection<ComPortControlViewModel> ComPortControlVms { get; }

		public SharedMainViewModel(IThreadNotifier notifier, IWindowSystem windowSystem, string windowTitle, ISharedAppAbilities appAbilities, List<string> channelNames) {
			_notifier = notifier;
			_windowSystem = windowSystem;
			_appAbilities = appAbilities;
			WindowTitle = windowTitle;

			_programLogVm = new ProgramLogViewModel(this);
			_logger = new RelayLogger(_programLogVm, new DateTimeFormatter(" > "));
			Tabs = new ObservableCollection<TabItemViewModel>();
			ComPortControlVms = new ObservableCollection<ComPortControlViewModel>();
			ComPortControlVms.Add(new ComPortControlViewModel(_appAbilities, _logger, _notifier, _windowSystem));

			_logger.Log("Программа загружена");
		}

		public void AddTab(TabItemViewModel tabVm) {
			//TabControlVm.Tabs.Add(tabVm);
			Tabs.Add(tabVm);
			Console.WriteLine("Tab added");
		}

		public bool TabHeadersAreLong {
			get => _tabHeadersAreLong;
			set {
				if (_tabHeadersAreLong != value) {
					_tabHeadersAreLong = value;
					foreach (var tabItemViewModel in Tabs) {
						tabItemViewModel.TabHeadersAreLong = value;
					}
					RaisePropertyChanged(() => TabHeadersAreLong);
				}
			}
		}

		public ProgramLogViewModel ProgramLogVm => _programLogVm;

		public IThreadNotifier Notifier => _notifier;
		public IWindowSystem WindowsSystem => _windowSystem;
		public ILogger Logger => _logger;
	}
}

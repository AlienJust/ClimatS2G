using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using AlienJust.Support.UserInterface.Contracts;
using CustomModbusSlave.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.CommandHearedTimer;
using CustomModbusSlave.Es2gClimatic.Shared.ProgamLog;
using CustomModbusSlave.Es2gClimatic.Shared.Record;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow {
	sealed class SharedMainViewModel : ViewModelBase, IUserInterfaceRoot, ISharedMainViewModel {
		private List<string> _comPortsAvailable;
		private string _selectedComName;

		private readonly IThreadNotifier _notifier;
		private readonly IWindowSystem _windowSystem;
		private readonly ISharedAppAbilities _appAbilities;

		private readonly RelayCommand _openPortCommand;
		private readonly RelayCommand _closePortCommand;
		public RelayCommand GetPortsAvailableCommand { get; }

		private readonly ProgramLogViewModel _programLogVm;
		private readonly ILogger _logger;

		private readonly List<ICmdListenerStd> _cmdListeners;

		public RecordViewModel RecordVm { get; }

		private bool _isPortOpened;

		private readonly CommandHearedTimerNotThreadSafe _commandHearedTimeoutMonitor;
		private Colors _linkBackColor;

		private bool _tabHeadersAreLong;

		public string WindowTitle { get; }
		public ObservableCollection<TabItemViewModel> Tabs { get; set; }

		public SharedMainViewModel(IThreadNotifier notifier, IWindowSystem windowSystem, string windowTitle, ISharedAppAbilities appAbilities) {
			_notifier = notifier;
			_windowSystem = windowSystem;
			_appAbilities = appAbilities;
			WindowTitle = windowTitle;

			_openPortCommand = new RelayCommand(OpenPort, () => !_isPortOpened);
			_closePortCommand = new RelayCommand(ClosePort, () => _isPortOpened);
			GetPortsAvailableCommand = new RelayCommand(GetPortsAvailable);

			_programLogVm = new ProgramLogViewModel(this);
			_logger = new RelayLogger(_programLogVm, new DateTimeFormatter(" > "));
			Tabs = new ObservableCollection<TabItemViewModel>();

			RecordVm = new RecordViewModel(_notifier, _windowSystem);
			GetPortsAvailable();

			_appAbilities.SerialChannel.CommandHeared += SerialChannelOnCommandHeared;
			_appAbilities.SerialChannel.CommandHearedWithReplyPossibility += SerialChannelOnCommandHearedWithReplyPossibility;
			_appAbilities.CommandHearedTimeoutMonitor.SomeCommandWasHeared += CommandHearedTimeoutMonitorOnSomeCommandWasHeared;
			_appAbilities.CommandHearedTimeoutMonitor.NoAnyCommandWasHearedTooLong += CommandHearedTimeoutMonitorOnNoAnyCommandWasHearedTooLong;

			_logger.Log("Программа загружена");
		}

		private void CommandHearedTimeoutMonitorOnSomeCommandWasHeared() {
			_notifier.Notify(() => { LinkBackColor = Colors.LimeGreen; });
		}

		private void CommandHearedTimeoutMonitorOnNoAnyCommandWasHearedTooLong() {
			_notifier.Notify(() => { LinkBackColor = Colors.OrangeRed; });
		}


		private void SerialChannelOnCommandHearedWithReplyPossibility(ICommandPart commandPart, ISendAbility sendAbility) {
			// TODO: RecordVm or not?
			//RecordVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
		}

		private void SerialChannelOnCommandHeared(ICommandPart commandPart) {
			RecordVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
		}

		private void GetPortsAvailable() {
			var ports = new List<string> { _appAbilities.TestPortName };
			ports.AddRange(SerialPort.GetPortNames());
			ComPortsAvailable = ports;
			if (ComPortsAvailable.Count > 0) SelectedComName = ComPortsAvailable[0];
			_logger.Log("Список COM-портов получен");
		}

		private void ClosePort() {
			_appAbilities.SerialChannel.CloseCurrentPortAsync(ex => _notifier.Notify(() => {
				if (ex == null) {
					IsPortOpened = false;
					_logger.Log("Порт " + _selectedComName + " закрыт");
					_closePortCommand.RaiseCanExecuteChanged();
					_openPortCommand.RaiseCanExecuteChanged();
				}
				else {
					_logger.Log("Ошибка во время закрытия порта: " + ex);
				}
			}));
		}

		private void OpenPort() {
			ISerialPortContainer portContainer;
			if (_selectedComName == _appAbilities.TestPortName) {
				var filename = _windowSystem.ShowOpenFileDialog("Текстовый файл с данными", "Текстовые файлы|*.txt|Все файлы|*.*");
				/*List<byte> valuesFromFile = new List<byte>();
				var parts = File.ReadAllText(filename).Split(new[] {" ", Environment.NewLine, "\t", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < parts.Length; ++i)
				{
					Console.WriteLine(parts[i]);
					valuesFromFile.Add(byte.Parse(parts[i], NumberStyles.HexNumber));
				}*/
				portContainer = !string.IsNullOrEmpty(filename) ? new SerialPortContainerTest(File.ReadAllText(filename).Split(new[] { " ", Environment.NewLine, "\t", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries).Select(t => byte.Parse(t, NumberStyles.HexNumber)).ToArray()) : new SerialPortContainerTest();
			}
			else {
				portContainer = new SerialPortContainerReal(_selectedComName, 57600);
			}

			_appAbilities.SerialChannel.SelectPortAsync(portContainer, ex => _notifier.Notify(() => {
				if (ex == null) {
					IsPortOpened = true;
					_logger.Log("Порт " + _selectedComName + " открыт");
					_closePortCommand.RaiseCanExecuteChanged();
					_openPortCommand.RaiseCanExecuteChanged();
				}
				else {
					_logger.Log("Ошибка во время открытия порта: " + ex);
				}
			}));
		}

		public void AddTab(TabItemViewModel tabVm) {
			//TabControlVm.Tabs.Add(tabVm);
			Tabs.Add(tabVm);
			Console.WriteLine("Tab added");
		}

		public List<string> ComPortsAvailable {
			get => _comPortsAvailable;
			set {
				if (_comPortsAvailable != value) {
					_comPortsAvailable = value;
					RaisePropertyChanged(() => ComPortsAvailable);
				}
			}
		}

		public string SelectedComName {
			get => _selectedComName;
			set {
				if (value != _selectedComName) {
					_selectedComName = value;
					RaisePropertyChanged(() => SelectedComName);
				}
			}
		}

		public bool IsPortOpened {
			get => _isPortOpened;
			set {
				if (_isPortOpened != value) {
					_isPortOpened = value;
					RaisePropertyChanged(() => IsPortOpened);
				}
			}
		}

		public Colors LinkBackColor {
			get => _linkBackColor;
			set {
				if (_linkBackColor != value) {
					_linkBackColor = value;
					RaisePropertyChanged(() => LinkBackColor);
				}
			}
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

		public RelayCommand OpenPortCommand => _openPortCommand;
		public RelayCommand ClosePortCommand => _closePortCommand;
		public ProgramLogViewModel ProgramLogVm => _programLogVm;

		public IThreadNotifier Notifier => _notifier;
		public IWindowSystem WindowsSystem => _windowSystem;
		public ILogger Logger => _logger;
	}
}

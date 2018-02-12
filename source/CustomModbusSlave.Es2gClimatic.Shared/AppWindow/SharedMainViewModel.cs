using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using AlienJust.Support.UserInterface.Contracts;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbus.Slave.FastReply.Queued;
using CustomModbusSlave.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp;
using CustomModbusSlave.Es2gClimatic.Shared.CommandHearedTimer;
using CustomModbusSlave.Es2gClimatic.Shared.ProgamLog;
using CustomModbusSlave.Es2gClimatic.Shared.Record;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow {
	public sealed class SharedMainViewModel : ViewModelBase, IUserInterfaceRoot, IAppAbilities {
		private List<string> _comPortsAvailable;
		private string _selectedComName;

		private readonly IThreadNotifier _notifier;
		private readonly IWindowSystem _windowSystem;
		private readonly SerialChannel _serialChannel;
		private readonly string _testPortName;

		private readonly RelayCommand _openPortCommand;
		private readonly RelayCommand _closePortCommand;
		public RelayCommand GetPortsAvailableCommand { get; }

		private readonly ProgramLogViewModel _programLogVm;
		private readonly ILogger _logger;

		private readonly IFastReplyGenerator _replyGenerator;
		private readonly IFastReplyAcceptor _replyAcceptor;

		private readonly ModbusRtuParamReceiver _rtuParamReceiver;

		private readonly List<ICmdListenerStd> _cmdListeners;

		public RecordViewModel RecordVm { get; }

		private bool _isPortOpened;

		private readonly CommandHearedTimerThreadSafe _commandHearedTimeoutMonitor;
		private Colors _linkBackColor;

		private bool _tabHeadersAreLong;

		public bool IsHalfOrFullVersion { get; }
		public bool IsFullVersion { get; }
		public bool IsHalfVersion => IsHalfOrFullVersion && !IsFullVersion; //easy, does it?

		public string WindowTitle { get; }
		public ObservableCollection<TabItemViewModel> Tabs { get; }

		public SharedMainViewModel(IThreadNotifier notifier, IWindowSystem windowSystem, IMultiLoggerWithStackTrace<int> debugLogger, SerialChannel serialChannel, string portName, string windowTitle, ObservableCollection<TabItemViewModel> tabs) {
			IsFullVersion = File.Exists("FullVersion.txt");

			IsHalfOrFullVersion = IsFullVersion;
			if (!IsHalfOrFullVersion) IsHalfOrFullVersion = File.Exists("HalfVersion.txt");

			_notifier = notifier;
			_windowSystem = windowSystem;
			DebugLogger = debugLogger;
			_serialChannel = serialChannel;
			_testPortName = portName;
			WindowTitle = windowTitle;
			Tabs = tabs;

			_rtuParamReceiver = new ModbusRtuParamReceiver();

			_openPortCommand = new RelayCommand(OpenPort, () => !_isPortOpened);
			_closePortCommand = new RelayCommand(ClosePort, () => _isPortOpened);
			GetPortsAvailableCommand = new RelayCommand(GetPortsAvailable);

			_programLogVm = new ProgramLogViewModel(this);
			_logger = new RelayLogger(_programLogVm, new DateTimeFormatter(" > "));

			var replyGenerator = new ReplyGeneratorWithQueueAttempted();
			ParamSetter = replyGenerator;
			_replyGenerator = replyGenerator;
			_replyAcceptor = replyGenerator;

			RecordVm = new RecordViewModel(_notifier, _windowSystem);

			_serialChannel.CommandHearedWithReplyPossibility += SerialChannelOnCommandHearedWithReplyPossibility;
			_serialChannel.CommandHeared += SerialChannelOnCommandHeared;

			_commandHearedTimeoutMonitor = new CommandHearedTimerThreadSafe(_serialChannel, TimeSpan.FromSeconds(1), _notifier);
			_commandHearedTimeoutMonitor.NoAnyCommandWasHearedTooLong += CommandHearedTimeoutMonitorOnNoAnyCommandWasHearedTooLong;
			_commandHearedTimeoutMonitor.SomeCommandWasHeared += CommandHearedTimeoutMonitorOnSomeCommandWasHeared;
			_commandHearedTimeoutMonitor.Start();

			GetPortsAvailable();
			_logger.Log("Программа загружена");
		}

		private void CommandHearedTimeoutMonitorOnSomeCommandWasHeared() {
			LinkBackColor = Colors.LimeGreen;
		}

		private void CommandHearedTimeoutMonitorOnNoAnyCommandWasHearedTooLong() {
			LinkBackColor = Colors.OrangeRed;
		}


		private void SerialChannelOnCommandHearedWithReplyPossibility(ICommandPart commandPart, ISendAbility sendAbility) {
			if (commandPart.Address == 20) {
				//_notifier.Notify(() => _recordedData.Add(commandPart.ReplyBytes));

				if (commandPart.CommandCode == 33 && commandPart.ReplyBytes.Count == 8) {
					_replyAcceptor.AcceptReply(commandPart.ReplyBytes.ToArray()); // TODO: bad performance (.ToArray())
					var reply = _replyGenerator.GenerateReply();
					sendAbility.Send(reply);
				}
			}
		}

		private void SerialChannelOnCommandHeared(ICommandPart commandPart) {
			RecordVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			// TODO: can be indexed for speeding up
			foreach (var cmdListenerStd in _cmdListeners) {
				cmdListenerStd.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			}
			_rtuParamReceiver.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
		}

		private void GetPortsAvailable() {
			var ports = new List<string> { _testPortName };
			ports.AddRange(SerialPort.GetPortNames());
			ComPortsAvailable = ports;
			if (ComPortsAvailable.Count > 0) SelectedComName = ComPortsAvailable[0];
			_logger.Log("Список COM-портов получен");
		}

		private void ClosePort() {
			_serialChannel.CloseCurrentPortAsync(ex => _notifier.Notify(() => {
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
			if (_selectedComName == _testPortName) {
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

			_serialChannel.SelectPortAsync(portContainer, ex => _notifier.Notify(() => {
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
					RaisePropertyChanged(() => TabHeadersAreLong);
				}
			}
		}

		public RelayCommand OpenPortCommand => _openPortCommand;
		public RelayCommand ClosePortCommand => _closePortCommand;
		public ProgramLogViewModel ProgramLogVm => _programLogVm;

		public IThreadNotifier Notifier => _notifier;
		public IWindowSystem WindowsSystem => _windowSystem;
		public ILogger Logger => _programLogVm;
		public IMultiLoggerWithStackTrace<int> DebugLogger { get; }
		public IParameterSetter ParamSetter { get; }
		public IFastReplyGenerator ReplyGenerator => _replyGenerator;
		public IFastReplyAcceptor ReplyAcceptor => _replyAcceptor;
	}
}

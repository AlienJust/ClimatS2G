using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using AlienJust.Support.UserInterface.Contracts;
using CustomModbusSlave.Contracts;
using DataAbstractionLevel.Low.PsnConfig;

namespace CustomModbusSlave.MicroclimatEs2gApp
{
	class MainViewModel : ViewModelBase, IUserInterfaceRoot
	{
		private List<string> _comPortsAvailable;
		private string _selectedComName;

		private readonly IThreadNotifier _notifier;
		private readonly IWindowSystem _windowSystem;

		private readonly RelayCommand _openPortCommand;
		private readonly RelayCommand _closePortCommand;
		private readonly RelayCommand _getPortsAvailableCommand;
		
		private readonly ProgramLogViewModel _programLogVm;
		private readonly ILogger _logger;

		private readonly SerialChannel _serialChannel;

		private bool _isPortOpened;
		private readonly MukFlapDataViewModel _mukFlapDataVm;
		private readonly MukVaporizerFanDataViewModel _mukVaporizerDataVm;
		private readonly MukFridgeFanDataViewModel _mukFridgeFanDataVm;
		private readonly MukWarmFloorDataViewModel _mukWarmFloorDataVm;

		public MainViewModel(IThreadNotifier notifier, IWindowSystem windowSystem) {
			_notifier = notifier;
			_windowSystem = windowSystem;

			_openPortCommand = new RelayCommand(OpenPort, () => !_isPortOpened);
			_closePortCommand = new RelayCommand(ClosePort, () => _isPortOpened);
			_getPortsAvailableCommand = new RelayCommand(GetPortsAvailable);

			_programLogVm = new ProgramLogViewModel(this);
			_logger = new RelayLogger(_programLogVm, new DateTimeFormatter(" > "));

			var psnConfig = new PsnProtocolConfigurationLoaderFromXml(Path.Combine(Environment.CurrentDirectory, "psn.Микроклимат-ES2G.xml")).LoadConfiguration();
			_serialChannel = new SerialChannel(
				new CommandPartSearcherPsnConfigBased(psnConfig),
				new SerialPortContainerRealWithTest("Test", new SerialPortContainerReal(), new SerialPortContainerTest()));
			_serialChannel.CommandHeared += SerialChannelOnCommandHeared;

			_mukFlapDataVm = new MukFlapDataViewModel(_notifier);
			_mukVaporizerDataVm = new MukVaporizerFanDataViewModel(_notifier);
			_mukFridgeFanDataVm = new MukFridgeFanDataViewModel(_notifier);
			_mukWarmFloorDataVm = new MukWarmFloorDataViewModel(_notifier);

			GetPortsAvailable();
			_logger.Log("Программа загружена");
		}

		private void SerialChannelOnCommandHeared(ICommandPart commandpart) {
			_notifier.Notify(()=>_logger.Log("Подслушана команда addr=0x" + commandpart.Address.ToString("X2") + ", code=0x" + commandpart.CommandCode.ToString("X2") + ", data.Count=" + commandpart.ReplyBytes.Count));
			_mukFlapDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);
			_mukVaporizerDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);
			_mukFridgeFanDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);
			_mukWarmFloorDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);
		}

		private void GetPortsAvailable() {
			var ports = new List<string> { "Test" }; // TODO: extract constant
			ports.AddRange(SerialPort.GetPortNames());
			ComPortsAvailable = ports;
			if (ComPortsAvailable.Count > 0) SelectedComName = ComPortsAvailable[0];
		}

		private void ClosePort() {
			_serialChannel.CloseCurrentPortAsync(ex => _notifier.Notify(() => {
				if (ex == null) {
					_isPortOpened = false;
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
			_serialChannel.SelectPortAsync(_selectedComName, 57600,ex => _notifier.Notify(() => {
				if (ex == null) {
					_isPortOpened = true;
					_logger.Log("Порт " + _selectedComName + " открыт");
					_closePortCommand.RaiseCanExecuteChanged();
					_openPortCommand.RaiseCanExecuteChanged();
				}
				else
				{
					_logger.Log("Ошибка во время открытия порта: " + ex);
				}
			}));
		}

		public IThreadNotifier Notifier {
			get { return _notifier; }
		}

		public List<string> ComPortsAvailable
		{
			get { return _comPortsAvailable; }
			set
			{
				if (_comPortsAvailable != value)
				{
					_comPortsAvailable = value;
					RaisePropertyChanged(() => ComPortsAvailable);
				}
			}
		}

		public string SelectedComName
		{
			get { return _selectedComName; }
			set
			{
				if (value != _selectedComName)
				{
					_selectedComName = value;
					RaisePropertyChanged(() => SelectedComName);
				}
			}
		}

		public RelayCommand OpenPortCommand
		{
			get { return _openPortCommand; }
		}

		public RelayCommand ClosePortCommand
		{
			get { return _closePortCommand; }
		}

		public RelayCommand GetPortsAvailableCommand
		{
			get { return _getPortsAvailableCommand; }
		}

		public ProgramLogViewModel ProgramLogVm
		{
			get { return _programLogVm; }
		}

		public MukFlapDataViewModel MukFlapDataVm {
			get { return _mukFlapDataVm; }
		}

		public MukVaporizerFanDataViewModel MukVaporizerFanDataVm
		{
			get { return _mukVaporizerDataVm; }
		}

		public MukFridgeFanDataViewModel MukFridgeFanDataVm {
			get { return _mukFridgeFanDataVm; }
		}

		public MukWarmFloorDataViewModel MukWarmFloorDataVm {
			get { return _mukWarmFloorDataVm; }
		}
	}
}

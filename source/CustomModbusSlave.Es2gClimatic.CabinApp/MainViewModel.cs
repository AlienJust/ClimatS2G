using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using AlienJust.Support.UserInterface.Contracts;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbus.Slave.FastReply.Queued;
using CustomModbusSlave.Contracts;
using CustomModbusSlave.Es2gClimatic.CabinApp.BsSm;
using CustomModbusSlave.Es2gClimatic.CabinApp.Ksm;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFridge;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukVaporizer;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.Bvs;
using CustomModbusSlave.Es2gClimatic.Shared.CommandHearedTimer;
using CustomModbusSlave.Es2gClimatic.Shared.ProgamLog;

namespace CustomModbusSlave.Es2gClimatic.CabinApp
{
	class MainViewModel : ViewModelBase, IUserInterfaceRoot {
		private List<string> _comPortsAvailable;
		private string _selectedComName;

		private readonly IThreadNotifier _notifier;
		private readonly IWindowSystem _windowSystem;
		private readonly IMultiLoggerWithStackTrace<int> _debugLogger;

		private readonly RelayCommand _openPortCommand;
		private readonly RelayCommand _closePortCommand;

		private readonly ProgramLogViewModel _programLogVm;
		private readonly ILogger _logger;

		private readonly SerialChannel _serialChannel;
		private readonly string _testPortName;

		private bool _isPortOpened;
		private readonly MukFlapDataViewModel _mukFlapDataVm;
		private readonly MukVaporizerFanDataViewModel _mukVaporizerDataVm;
		private readonly MukFridgeFanDataViewModel _mukFridgeFanDataVm;
		private readonly MukWarmFloorDataViewModel _mukWarmFloorDataVm;
		private readonly BsSmDataViewModel _bsSmDataVm;
		private readonly IParameterSetter _paramSetter;
		private readonly IFastReplyGenerator _replyGenerator;
		private readonly IFastReplyAcceptor _replyAcceptor;

		private readonly CommandHearedTimerThreadSafe _commandHearedTimeoutMonitor;
		private Colors _linkBackColor;

		public MainViewModel(IThreadNotifier notifier, IWindowSystem windowSystem, IMultiLoggerWithStackTrace<int> debugLogger, SerialChannel serialChannel, string testPortName) {
			_notifier = notifier;
			_windowSystem = windowSystem;
			_debugLogger = debugLogger;
			_serialChannel = serialChannel;
			_testPortName = testPortName;

			_openPortCommand = new RelayCommand(OpenPort, () => !_isPortOpened);
			_closePortCommand = new RelayCommand(ClosePort, () => _isPortOpened);
			GetPortsAvailableCommand = new RelayCommand(GetPortsAvailable);

			_programLogVm = new ProgramLogViewModel(this);
			_logger = new RelayLogger(_programLogVm, new DateTimeFormatter(" > "));

			_serialChannel.CommandHearedWithReplyPossibility += SerialChannelOnCommandHearedWithReplyPossibility;
			_serialChannel.CommandHeared += SerialChannelOnCommandHeared;

			var replyGenerator = new ReplyGeneratorWithQueueAttempted();
			_paramSetter = replyGenerator;
			_replyGenerator = replyGenerator;
			_replyAcceptor = replyGenerator;

			_mukFlapDataVm = new MukFlapDataViewModel(_notifier, _paramSetter);
			_mukVaporizerDataVm = new MukVaporizerFanDataViewModel(_notifier, _paramSetter);
			_mukFridgeFanDataVm = new MukFridgeFanDataViewModel(_notifier, _paramSetter);
			_mukWarmFloorDataVm = new MukWarmFloorDataViewModel(_notifier, _paramSetter);
			_bsSmDataVm = new BsSmDataViewModel(_notifier);
			BvsDataVm = new BvsDataViewModel(_notifier, 0x1E);

			KsmDataVm = new KsmDataViewModel(_notifier, _paramSetter);

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
				if (commandPart.CommandCode == 33 && commandPart.ReplyBytes.Count == 8) {
					_replyAcceptor.AcceptReply(commandPart.ReplyBytes.ToArray());
					var reply = _replyGenerator.GenerateReply();

					sendAbility.Send(reply);
					//Console.WriteLine("Reply sended--------------------------------------------------------------------------------------------------");
					//_notifier.Notify(() => _logger.Log("Reply sended"));
				}
				else if (commandPart.CommandCode == 16 && commandPart.ReplyBytes.Count == 109) {
					// todo: send back
					//Console.WriteLine("Accepted 50 params command -----------------------------------------------------------------------------------");
					_notifier.Notify(() => {
						KsmDataVm.AcceptCommandAllParameters(commandPart.ReplyBytes.ToList());
					});
				}
			}
		}
	

		private void SerialChannelOnCommandHeared(ICommandPart commandpart) {
			//_notifier.Notify(()=>_logger.Log("Подслушана команда addr=0x" + commandpart.Address.ToString("X2") + ", code=0x" + commandpart.CommandCode.ToString("X2") + ", data.Count=" + commandpart.ReplyBytes.Count));
			_mukFlapDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);
			_mukVaporizerDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);
			_mukFridgeFanDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);
			_mukWarmFloorDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);
			_bsSmDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);

			BvsDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);
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
			_serialChannel.SelectPortAsync(_selectedComName, 57600,ex => _notifier.Notify(() => {
				if (ex == null) {
					IsPortOpened = true;
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

		public IThreadNotifier Notifier => _notifier;

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

		public RelayCommand OpenPortCommand => _openPortCommand;

		public RelayCommand ClosePortCommand => _closePortCommand;

		public RelayCommand GetPortsAvailableCommand { get; }

		public ProgramLogViewModel ProgramLogVm => _programLogVm;

		public MukFlapDataViewModel MukFlapDataVm => _mukFlapDataVm;

		public MukVaporizerFanDataViewModel MukVaporizerFanDataVm => _mukVaporizerDataVm;

		public MukFridgeFanDataViewModel MukFridgeFanDataVm => _mukFridgeFanDataVm;

		public MukWarmFloorDataViewModel MukWarmFloorDataVm => _mukWarmFloorDataVm;

		public BsSmDataViewModel BsSmDataVm => _bsSmDataVm;

		public bool IsPortOpened {
			get { return _isPortOpened; }
			set {
				if (_isPortOpened != value) {
					_isPortOpened = value;
					RaisePropertyChanged(() => IsPortOpened);
				}
			}
		}

		public Colors LinkBackColor {
			get { return _linkBackColor; }
			set {
				if (_linkBackColor != value) {
					_linkBackColor = value;
					RaisePropertyChanged(() => LinkBackColor);
				}
			}
		}

		public BvsDataViewModel BvsDataVm { get; }

		public KsmDataViewModel KsmDataVm { get; }
	}
}

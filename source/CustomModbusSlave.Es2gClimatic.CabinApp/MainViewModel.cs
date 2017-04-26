using System;
using System.Collections.Generic;
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
using CustomModbusSlave.Es2gClimatic.CabinApp.BsSm;
using CustomModbusSlave.Es2gClimatic.CabinApp.Ksm;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor;
using CustomModbusSlave.Es2gClimatic.InteriorApp;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFridge;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.Bvs;
using CustomModbusSlave.Es2gClimatic.Shared.CommandHearedTimer;
using CustomModbusSlave.Es2gClimatic.Shared.MukCondenser.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.ProgamLog;
using CustomModbusSlave.Es2gClimatic.Shared.Record;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm;

namespace CustomModbusSlave.Es2gClimatic.CabinApp {
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

		private readonly ModbusRtuParamReceiver _rtuParamReceiver;

		private bool _isPortOpened;
		private readonly MukFlapDataViewModel _mukFlapDataVm;
		public MukVaporizerFanDataViewModelParamcentric MukFanVaporizerDataVm { get; }
		private readonly MukWarmFloorDataViewModel _mukWarmFloorDataVm;
		private readonly BsSmDataViewModel _bsSmDataVm;
		private readonly IParameterSetter _paramSetter;
		private readonly IFastReplyGenerator _replyGenerator;
		private readonly IFastReplyAcceptor _replyAcceptor;

		private readonly ICmdListener<IMukVaporizerFanReply03Telemetry> _cmdListenerMukVaporizerReply03;
		private readonly ICmdListener<IMukVaporizerRequest16InteriorData> _cmdListenerMukVaporizerRequest16;

		private readonly ICmdListener<IMukCondensorFanReply03Data> _cmdListenerMukCondenserFanReply03;
		private readonly ICmdListener<IMukCondenserRequest16Data> _cmdListenerMukCondenserRequest16;

		private readonly ICmdListener<IList<BytesPair>> _cmdListenerKsm50Params;

		public RecordViewModel RecordVm { get; }

		private readonly CommandHearedTimerThreadSafe _commandHearedTimeoutMonitor;
		private Colors _linkBackColor;


		public MainViewModel(IThreadNotifier notifier, IWindowSystem windowSystem, IMultiLoggerWithStackTrace<int> debugLogger, SerialChannel serialChannel, string testPortName) {
			_notifier = notifier;
			_windowSystem = windowSystem;
			_debugLogger = debugLogger;
			_serialChannel = serialChannel;
			_testPortName = testPortName;

			_rtuParamReceiver = new ModbusRtuParamReceiver();

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

			_cmdListenerMukVaporizerReply03 = new CmdListenerMukVaporizerReply03(3, 3, 41);
			_cmdListenerMukVaporizerRequest16 = new CmdListenerMukVaporizerRequest16(3, 16, 21);

			_cmdListenerMukCondenserFanReply03 = new CmdListenerMukCondenserFanReply03(4, 3, 29);
			_cmdListenerMukCondenserRequest16 = new CmdListenerMukCondenserFanRequest16(4, 16, 15);

			_cmdListenerKsm50Params = new CmdListenerKsmParams(20, 16, 109);

			_mukFlapDataVm = new MukFlapDataViewModel(_notifier, _paramSetter);
			MukFanVaporizerDataVm = new MukVaporizerFanDataViewModelParamcentric(
				notifier,
				_paramSetter,
				_rtuParamReceiver,
				_cmdListenerMukVaporizerReply03,
				_cmdListenerMukVaporizerRequest16
			);

			MukFridgeFanDataVm = new MukFridgeFanDataViewModel(_notifier, _paramSetter, _cmdListenerMukCondenserFanReply03, _cmdListenerMukCondenserRequest16);
			_mukWarmFloorDataVm = new MukWarmFloorDataViewModel(_notifier, _paramSetter);

			_bsSmDataVm = new BsSmDataViewModel(_notifier);
			BvsDataVm = new BvsDataViewModel(_notifier, 0x1E);
			KsmDataVm = new KsmDataViewModel(_notifier, _paramSetter, _cmdListenerKsm50Params);

			RecordVm = new RecordViewModel(_notifier, _windowSystem);

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
				}
			}
			_cmdListenerKsm50Params.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
		}


		private void SerialChannelOnCommandHeared(ICommandPart commandPart) {
			RecordVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			//_notifier.Notify(()=>_logger.Log("Подслушана команда addr=0x" + commandPart.Address.ToString("X2") + ", code=0x" + commandPart.CommandCode.ToString("X2") + ", data.Count=" + commandPart.ReplyBytes.Count));
			_mukFlapDataVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);

			_cmdListenerMukVaporizerReply03.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			_cmdListenerMukVaporizerRequest16.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);

			_cmdListenerMukCondenserFanReply03.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			_cmdListenerMukCondenserRequest16.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);

			_mukWarmFloorDataVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			_bsSmDataVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			BvsDataVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);

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
				portContainer = !string.IsNullOrEmpty(filename) ? new SerialPortContainerTest(File.ReadAllText(filename).Split(new[] { " ", Environment.NewLine, "\t" }, StringSplitOptions.RemoveEmptyEntries).Select(t => byte.Parse(t, NumberStyles.HexNumber)).ToArray()) : new SerialPortContainerTest();
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

		public IThreadNotifier Notifier => _notifier;

		public List<string> ComPortsAvailable {
			get { return _comPortsAvailable; }
			set {
				if (_comPortsAvailable != value) {
					_comPortsAvailable = value;
					RaisePropertyChanged(() => ComPortsAvailable);
				}
			}
		}

		public string SelectedComName {
			get { return _selectedComName; }
			set {
				if (value != _selectedComName) {
					_selectedComName = value;
					RaisePropertyChanged(() => SelectedComName);
				}
			}
		}



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

		public RelayCommand OpenPortCommand => _openPortCommand;
		public RelayCommand ClosePortCommand => _closePortCommand;
		public RelayCommand GetPortsAvailableCommand { get; }
		public ProgramLogViewModel ProgramLogVm => _programLogVm;
		public MukFlapDataViewModel MukFlapDataVm => _mukFlapDataVm;
		public MukFridgeFanDataViewModel MukFridgeFanDataVm { get; }

		public MukWarmFloorDataViewModel MukWarmFloorDataVm => _mukWarmFloorDataVm;
		public BsSmDataViewModel BsSmDataVm => _bsSmDataVm;
		public BvsDataViewModel BvsDataVm { get; }
		public KsmDataViewModel KsmDataVm { get; }
	}
}

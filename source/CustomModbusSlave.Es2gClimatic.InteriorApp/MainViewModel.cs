﻿using System;
using System.Collections.Generic;
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
using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm;
using CustomModbusSlave.Es2gClimatic.InteriorApp.Ksm;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.ViewModel;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapReturnAir;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFridge;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukVaporizerFan;
using CustomModbusSlave.Es2gClimatic.InteriorApp.SystemDiagnostic;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.Bvs;
using CustomModbusSlave.Es2gClimatic.Shared.CommandHearedTimer;
using CustomModbusSlave.Es2gClimatic.Shared.MukVaporizer.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.ProgamLog;
using CustomModbusSlave.Es2gClimatic.UniversalParams;
using ParamCentric.Common.Contracts;
using ParamCentric.Modbus.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp
{
	class MainViewModel : ViewModelBase, IUserInterfaceRoot {
		private const string TestPortName = "ТЕСТ";
		private List<string> _comPortsAvailable;
		private string _selectedComName;

		private readonly IThreadNotifier _notifier;
		private readonly IWindowSystem _windowSystem;
		private readonly IMultiLoggerWithStackTrace<int> _debugLogger;
		private readonly SerialChannel _serialChannel;
		private readonly string _testPortName;

		private readonly RelayCommand _openPortCommand;
		private readonly RelayCommand _closePortCommand;

		private readonly ProgramLogViewModel _programLogVm;
		private readonly ILogger _logger;

		private readonly IParameterSetter _paramSetter;
		private readonly IFastReplyGenerator _replyGenerator;
		private readonly IFastReplyAcceptor _replyAcceptor;

		private readonly ModbusRtuParamReceiver _rtuParamReceiver;

		private readonly CmdListenerMukVaporizerRequest16 _cmdListenerMukVaporizerRequest16;
		private readonly CmdListenerKsm60Params _cmdListenerKsm60Params;
		private readonly SystemDiagnosticViewModel _systemDiagnosticVm;

		private bool _isPortOpened;

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
			
			var replyGenerator = new ReplyGeneratorWithQueueAttempted();
			_paramSetter = replyGenerator;
			_replyGenerator = replyGenerator;
			_replyAcceptor = replyGenerator;

			MukFlapDataVm = new MukFlapDataViewModel(_notifier, _paramSetter);

			_cmdListenerMukVaporizerRequest16 = new CmdListenerMukVaporizerRequest16(3, 16, 21);
			_systemDiagnosticVm = new SystemDiagnosticViewModel(_notifier, _cmdListenerMukVaporizerRequest16);
			MukVaporizerFanDataVm = new MukVaporizerFanDataViewModelParamcentric(_notifier, _paramSetter, null, _rtuParamReceiver, _cmdListenerMukVaporizerRequest16);


			MukFridgeFanDataVm = new MukFridgeFanDataViewModel(_notifier, _paramSetter);
			MukAirExhausterDataVm = new MukAirExhausterDataViewModel(_notifier, _paramSetter);
			MukFlapReturnAirDataVm = new MukFlapReturnAirViewModel(_notifier, _paramSetter);
			MukFlapWinterSummerDataVm = new MukFlapWinterSummerViewModel(_notifier, _paramSetter);

			BsSmDataVm = new BsSmDataViewModel(_notifier);
			BvsDataVm = new BvsDataViewModel(_notifier, 0x1E);
			BvsDataVm2 = new BvsDataViewModel(_notifier, 0x1D);

			KsmDataVm = new KsmDataViewModel(_notifier, _paramSetter);

			


			_serialChannel.CommandHearedWithReplyPossibility += SerialChannelOnCommandHearedWithReplyPossibility;
			_serialChannel.CommandHeared += SerialChannelOnCommandHeared;

			_commandHearedTimeoutMonitor = new CommandHearedTimerThreadSafe(_serialChannel, TimeSpan.FromSeconds(1), _notifier);
			_commandHearedTimeoutMonitor.NoAnyCommandWasHearedTooLong += CommandHearedTimeoutMonitorOnNoAnyCommandWasHearedTooLong;
			_commandHearedTimeoutMonitor.SomeCommandWasHeared += CommandHearedTimeoutMonitorOnSomeCommandWasHeared;
			_commandHearedTimeoutMonitor.Start();

			GetPortsAvailable();
			_logger.Log("Программа загружена");


			var testItems = new List<IGroupItem>();
			TestGroup = new GroupSimple("Тестовая группа", testItems);
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
					//Console.WriteLine("Reply sended--------------------------------------------------------------------------------------------------------------------------------");
					//_notifier.Notify(() => _logger.Log("Reply sended"));
				}
				else if (commandPart.CommandCode == 16 && commandPart.ReplyBytes.Count == 129) {
					// todo: send back
					//Console.WriteLine("Accepted 60 params command =============================================================================================================================================");
					_notifier.Notify(() => {
						KsmDataVm.AcceptCommandAllParameters(commandPart.ReplyBytes.ToList());
					});
				}

				_cmdListenerKsm60Params.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			}
		}

		private void SerialChannelOnCommandHeared(ICommandPart commandPart) {
			//_notifier.Notify(()=>_logger.Log("Подслушана команда addr=0x" + commandPart.Address.ToString("X2") + ", code=0x" + commandPart.CommandCode.ToString("X2") + ", data.Count=" + commandPart.ReplyBytes.Count));
			_cmdListenerMukVaporizerRequest16.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);

			_rtuParamReceiver.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);

			MukFlapDataVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			MukVaporizerFanDataVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			MukFridgeFanDataVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			MukAirExhausterDataVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			MukFlapReturnAirDataVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			MukFlapWinterSummerDataVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			
			BsSmDataVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);

			BvsDataVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			BvsDataVm2.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
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

		public MukFlapDataViewModel MukFlapDataVm { get; }

		public MukVaporizerFanDataViewModelParamcentric MukVaporizerFanDataVm { get; }

		public MukFridgeFanDataViewModel MukFridgeFanDataVm { get; }

		public MukAirExhausterDataViewModel MukAirExhausterDataVm { get; }

		public MukFlapReturnAirViewModel MukFlapReturnAirDataVm { get; }

		public MukFlapWinterSummerViewModel MukFlapWinterSummerDataVm { get; }
		
		public BsSmDataViewModel BsSmDataVm { get; }
		public BvsDataViewModel BvsDataVm { get; }
		public BvsDataViewModel BvsDataVm2 { get; }

		public KsmDataViewModel KsmDataVm { get; }

		public bool IsPortOpened {
			get { return _isPortOpened; }
			set {
				if (_isPortOpened != value) {
					_isPortOpened = value;
					RaisePropertyChanged(() => IsPortOpened);
				}
			}
		}

		public Colors LinkBackColor
		{
			get { return _linkBackColor; }
			set
			{
				if (_linkBackColor != value) {
					_linkBackColor = value;
					RaisePropertyChanged(()=>LinkBackColor);
				}
			}
		}

		public IGroup TestGroup { get; }

		public SystemDiagnosticViewModel SystemDiagnosticVm => _systemDiagnosticVm;
	}

	class ModbusRtuParamReceiver : IReceiverModbusRtu, ICommandListener {
		private readonly List<IReceivableModbusRtuParameter> _params;
		public ModbusRtuParamReceiver() {
			_params = new List<IReceivableModbusRtuParameter>();
		}

		public void RegisterParamToReceive(IReceivableModbusRtuParameter parameter) {
			_params.Add(parameter);
		}

		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (data.Count%2 != 0) { // ответ ModbusRTU всегда нечетный! (запрос чётный)
				foreach (var parameter in _params) {
					if (parameter.Address == addr && parameter.CommandCode == code) {
						try {
							parameter.ReceivedBytesValue = new BytesPair(data[3 + parameter.ZeroBasedParameterNumber*2], data[4 + parameter.ZeroBasedParameterNumber*2]);
						}
						catch {
							// ignored
						}
					}
				}
			}
		}
	}
}

using System;
using System.Collections.Generic;
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
using CustomModbusSlave.Contracts;
using CustomModbusSlave.Es2gClimatic;
using CustomModbusSlave.Es2gClimatic.UniversalParams;
using CustomModbusSlave.MicroclimatEs2gApp.BsSm;
using CustomModbusSlave.MicroclimatEs2gApp.Bvs;
using CustomModbusSlave.MicroclimatEs2gApp.Common;
using CustomModbusSlave.MicroclimatEs2gApp.Common.CommandHearedTimer;
using CustomModbusSlave.MicroclimatEs2gApp.Common.ProgamLog;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Common.UniversalParams;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;
using CustomModbusSlave.MicroclimatEs2gApp.MukAirExhauster.ViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapReturnAir;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapWinterSummer;
using CustomModbusSlave.MicroclimatEs2gApp.MukFridge;
using CustomModbusSlave.MicroclimatEs2gApp.MukVaporizerFan;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams;
using DataAbstractionLevel.Low.PsnConfig;

namespace CustomModbusSlave.MicroclimatEs2gApp
{
	class MainViewModel : ViewModelBase, IUserInterfaceRoot {
		private const string TestPortName = "ТЕСТ";
		private List<string> _comPortsAvailable;
		private string _selectedComName;

		private readonly IThreadNotifier _notifier;
		private readonly IWindowSystem _windowSystem;

		private readonly RelayCommand _openPortCommand;
		private readonly RelayCommand _closePortCommand;

		private readonly ProgramLogViewModel _programLogVm;
		private readonly ILogger _logger;

		private readonly SerialChannel _serialChannel;

		private readonly IParameterSetter _paramSetter;
		private readonly IFastReplyGenerator _replyGenerator;
		private readonly IFastReplyAcceptor _replyAcceptor;

		private readonly ModbusRtuParamReceiver _rtuParamReceiver;

		private bool _isPortOpened;

		private readonly CommandHearedTimerThreadSafe _commandHearedTimeoutMonitor;
		private Colors _linkBackColor;

		public MainViewModel(IThreadNotifier notifier, IWindowSystem windowSystem) {
			_notifier = notifier;
			_windowSystem = windowSystem;

			_rtuParamReceiver = new ModbusRtuParamReceiver();

			_openPortCommand = new RelayCommand(OpenPort, () => !_isPortOpened);
			_closePortCommand = new RelayCommand(ClosePort, () => _isPortOpened);
			GetPortsAvailableCommand = new RelayCommand(GetPortsAvailable);

			_programLogVm = new ProgramLogViewModel(this);
			_logger = new RelayLogger(_programLogVm, new DateTimeFormatter(" > "));

			var psnConfig = new PsnProtocolConfigurationLoaderFromXml(Path.Combine(Environment.CurrentDirectory, "psn.Микроклимат-ЭС2ГП-салон.xml")).LoadConfiguration();

			var portConatiner = new SerialPortContainerRealWithTest(TestPortName, new SerialPortContainerReal(), new SerialPortContainerTest());
			_serialChannel = new SerialChannel(
				new CommandPartSearcherPsnConfigBasedFast(psnConfig),
				portConatiner, portConatiner);

			

			var replyGenerator = new ReplyGeneratorWithQueueAttempted(_notifier);
			_paramSetter = replyGenerator;
			_replyGenerator = replyGenerator;
			_replyAcceptor = replyGenerator;

			MukFlapDataVm = new MukFlapDataViewModel(_notifier, _paramSetter);
			MukVaporizerFanDataVm = new MukVaporizerFanDataViewModelParamcentric(_notifier, _paramSetter, null, _rtuParamReceiver);
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

					Console.WriteLine("Reply sended--------------------------------------------------------------------------------------------------------------------------------");
					_notifier.Notify(() => _logger.Log("Reply sended"));
				}
				else if (commandPart.CommandCode == 16 && commandPart.ReplyBytes.Count == 129) {
					// todo: send back
					Console.WriteLine("Accepted 60 params command =============================================================================================================================================");
					_notifier.Notify(() => {
						KsmDataVm.AcceptCommandAllParameters(commandPart.ReplyBytes.ToList());
					});
				}
			}
		}

		private void SerialChannelOnCommandHeared(ICommandPart commandpart) {
			_notifier.Notify(()=>_logger.Log("Подслушана команда addr=0x" + commandpart.Address.ToString("X2") + ", code=0x" + commandpart.CommandCode.ToString("X2") + ", data.Count=" + commandpart.ReplyBytes.Count));

			_rtuParamReceiver.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);

			MukFlapDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);
			MukVaporizerFanDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);
			MukFridgeFanDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);
			MukAirExhausterDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);
			MukFlapReturnAirDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);
			MukFlapWinterSummerDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);
			
			BsSmDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);

			BvsDataVm.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);
			BvsDataVm2.ReceiveCommand(commandpart.Address, commandpart.CommandCode, commandpart.ReplyBytes);
		}

		private void GetPortsAvailable() {
			var ports = new List<string> { TestPortName };
			ports.AddRange(SerialPort.GetPortNames());
			ComPortsAvailable = ports;
			if (ComPortsAvailable.Count > 0) SelectedComName = ComPortsAvailable[0];
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

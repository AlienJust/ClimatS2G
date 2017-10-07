using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows.Input;
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
using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.Ksm;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Data.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.ViewModel;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirWinterSummer.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFridge;
using CustomModbusSlave.Es2gClimatic.InteriorApp.SystemDiagnostic;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.Bvs;
using CustomModbusSlave.Es2gClimatic.Shared.CommandHearedTimer;
using CustomModbusSlave.Es2gClimatic.Shared.MukCondenser.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.ProgamLog;
using CustomModbusSlave.Es2gClimatic.Shared.Record;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm;
using CustomModbusSlave.Es2gClimatic.UniversalParams;
using ParamCentric.Common.Contracts;
using CmdListenerMukAirExhausterRequest16 = CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Request16.CmdListenerMukAirExhausterRequest16;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp {
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
		public RelayCommand GetPortsAvailableCommand { get; }

		private readonly ProgramLogViewModel _programLogVm;
		private readonly ILogger _logger;

		private readonly IParameterSetter _paramSetter;
		private readonly IFastReplyGenerator _replyGenerator;
		private readonly IFastReplyAcceptor _replyAcceptor;

		private readonly ModbusRtuParamReceiver _rtuParamReceiver;

		private readonly ICmdListener<IMukFlapReply03Telemetry> _cmdListenerMukFlapOuterAirReply03;
		private readonly ICmdListener<IMukFlapOuterAirRequest16Data> _cmdListenerMukFlapOuterAirRequest16;

		private readonly ICmdListener<IMukFanVaporizerDataReply03> _cmdListenerMukVaporizerReply03;
		private readonly ICmdListener<IMukFanVaporizerDataRequest16> _cmdListenerMukVaporizerRequest16;

		private readonly ICmdListener<IMukCondensorFanReply03Data> _cmdListenerMukCondenserFanReply03;
		private readonly ICmdListener<IMukCondenserRequest16Data> _cmdListenerMukCondenserRequest16;

		private readonly ICmdListener<IMukAirExhausterReply03Data> _cmdListenerMukAirExhausterReply03;
		private readonly ICmdListener<IMukFanAirExhausterRequest16Data> _cmdListenerMukAirExhausterRequest16;

		private readonly ICmdListener<IMukFlapReturnAirReply03Telemetry> _cmdListenerMukFlapReturnAirReply03;
		private readonly ICmdListener<IMukFlapAirRecycleRequest16Data> _cmdListenerMukFlapAirRecycleRequest16;

		private readonly ICmdListener<IMukFlapWinterSummerReply03Telemetry> _cmdListenerMukFlapWinterSummerReply03;
		private readonly ICmdListener<IMukFlapAirWinterSummerRequest16Data> _cmdListenerMukAirFlapWinterSummerRequest16;

		private readonly ICmdListener<IBsSmAndKsm1DataCommand32Request> _cmdListenerBsSmRequest32;
		private readonly ICmdListener<IBsSmAndKsm1DataCommand32Reply> _cmdListenerBsSmReply32;

		private readonly ICmdListener<IBvsReply65Telemetry> _cmdListenerBvs1Reply65;
		private readonly ICmdListener<IBvsReply65Telemetry> _cmdListenerBvs2Reply65;

		private readonly ICmdListener<IList<BytesPair>> _cmdListenerKsmParams;

		public SystemDiagnosticViewModel SystemDiagnosticVm { get; }
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
		public IGroup TestGroup { get; }
		public RecordViewModel RecordVm { get; }

		private bool _isPortOpened;

		private readonly CommandHearedTimerThreadSafe _commandHearedTimeoutMonitor;
		private Colors _linkBackColor;

		private bool _tabHeadersAreLong;
		public bool IsFullVersion { get; }

		public MainViewModel(IThreadNotifier notifier, IWindowSystem windowSystem, IMultiLoggerWithStackTrace<int> debugLogger, SerialChannel serialChannel, string testPortName)
		{
			IsFullVersion = File.Exists("FullVersion.txt");

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

			// Заслонка наруж. воздуха
			_cmdListenerMukFlapOuterAirReply03 = new CmdListenerMukFlapOuterAirReply03(2, 3, 47);
			_cmdListenerMukFlapOuterAirRequest16 = new CmdListenerMukFlapOuterAirRequest16(2, 16, 21);

			// Вентилятор испарителя
			_cmdListenerMukVaporizerReply03 = new CmdListenerMukVaporizerReply03(3, 3, 41);
			_cmdListenerMukVaporizerRequest16 = new CmdListenerMukVaporizerRequest16(3, 16, 21);

			// Вентилятор конденсатора
			_cmdListenerMukCondenserFanReply03 = new CmdListenerMukCondenserFanReply03(4, 3, 29);
			_cmdListenerMukCondenserRequest16 = new CmdListenerMukCondenserFanRequest16(4, 16, 15);

			// МУК вытяжного вентилятора
			_cmdListenerMukAirExhausterReply03 = new CmdListenerMukAirExhausterReply03(6, 3, 31);
			_cmdListenerMukAirExhausterRequest16 = new CmdListenerMukAirExhausterRequest16(6, 16, 21);

			// МУК рециркуляционной заслонки
			_cmdListenerMukFlapReturnAirReply03 = new CmdListenerMukFlapReturnAirReply03(7, 3, 43);
			_cmdListenerMukFlapAirRecycleRequest16 = new CmdListenerMukFlapAirRecycleRequest16(7, 16, 21);

			// МУК заслонки зима-лето
			_cmdListenerMukFlapWinterSummerReply03 = new CmdListenerMukFlapWinterSummerReply03(8, 3, 47);
			_cmdListenerMukAirFlapWinterSummerRequest16 = new CmdListenerMukFlapAirWinterSummerRequest16(8, 16, 21);

			_cmdListenerBsSmReply32 = new CmdListenerBsSmReply32(10, 32, 47);
			_cmdListenerBsSmRequest32 = new CmdListenerBsSmRequest32(10, 32, 27);

			_cmdListenerBvs1Reply65 = new CmdListenerBvsReply65(0x1E, 65, 7);
			_cmdListenerBvs2Reply65 = new CmdListenerBvsReply65(0x1D, 65, 7);

			_cmdListenerKsmParams = new CmdListenerKsmParams(20, 16, 129);

			RecordVm = new RecordViewModel(_notifier, _windowSystem);

			SystemDiagnosticVm = new SystemDiagnosticViewModel(
				IsFullVersion,
				_notifier,
				_cmdListenerMukFlapOuterAirReply03,
				_cmdListenerMukVaporizerReply03,
				_cmdListenerMukVaporizerRequest16,
				_cmdListenerMukCondenserFanReply03,
				_cmdListenerMukAirExhausterReply03,
				_cmdListenerMukFlapReturnAirReply03,
				_cmdListenerMukFlapWinterSummerReply03,
				_cmdListenerBsSmReply32,
				_cmdListenerKsmParams,
				_cmdListenerBvs1Reply65,
				_cmdListenerBvs2Reply65);

			MukFlapDataVm = new MukFlapDataViewModel(
				_notifier,
				_paramSetter,
				_cmdListenerMukFlapOuterAirReply03,
				_cmdListenerMukFlapOuterAirRequest16);

			MukVaporizerFanDataVm = new MukVaporizerFanDataViewModelParamcentric(
				notifier,
				_paramSetter,
				_rtuParamReceiver,
				_cmdListenerMukVaporizerReply03,
				_cmdListenerMukVaporizerRequest16
				);

			MukFridgeFanDataVm = new MukFridgeFanDataViewModel(_notifier, _paramSetter, _cmdListenerMukCondenserFanReply03, _cmdListenerMukCondenserRequest16);
			MukAirExhausterDataVm = new MukAirExhausterDataViewModel(_notifier, _paramSetter, _cmdListenerMukAirExhausterReply03, _cmdListenerMukAirExhausterRequest16);
			MukFlapReturnAirDataVm = new MukFlapReturnAirViewModel(_notifier, _paramSetter, _cmdListenerMukFlapReturnAirReply03, _cmdListenerMukFlapAirRecycleRequest16);
			MukFlapWinterSummerDataVm = new MukFlapWinterSummerViewModel(_notifier, _paramSetter, _cmdListenerMukFlapWinterSummerReply03, _cmdListenerMukAirFlapWinterSummerRequest16);

			BsSmDataVm = new BsSmDataViewModel(_notifier, _cmdListenerBsSmRequest32, _cmdListenerBsSmReply32);
			BvsDataVm = new BvsDataViewModel(_notifier, _cmdListenerBvs1Reply65);
			BvsDataVm2 = new BvsDataViewModel(_notifier, _cmdListenerBvs2Reply65);

			KsmDataVm = new KsmDataViewModel(_notifier, _paramSetter, _cmdListenerKsmParams);

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
				//_notifier.Notify(() => _recordedData.Add(commandPart.ReplyBytes));

				if (commandPart.CommandCode == 33 && commandPart.ReplyBytes.Count == 8) {
					_replyAcceptor.AcceptReply(commandPart.ReplyBytes.ToArray()); // TODO: bad perfomance (.ToArray)
					var reply = _replyGenerator.GenerateReply();
					sendAbility.Send(reply);
				}
				_cmdListenerKsmParams.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes); // TODO: or move it to SerialChannelOnCommandHeared
			}
		}

		private void SerialChannelOnCommandHeared(ICommandPart commandPart) {
			RecordVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			// TODO: can be indexed for speeding up
			_cmdListenerMukFlapOuterAirReply03.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			_cmdListenerMukFlapOuterAirRequest16.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);

			_cmdListenerMukVaporizerReply03.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			_cmdListenerMukVaporizerRequest16.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);

			_cmdListenerMukCondenserFanReply03.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			_cmdListenerMukCondenserRequest16.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);

			_cmdListenerMukAirExhausterReply03.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			_cmdListenerMukAirExhausterRequest16.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);

			_cmdListenerMukFlapReturnAirReply03.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			_cmdListenerMukFlapAirRecycleRequest16.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);

			_cmdListenerMukFlapWinterSummerReply03.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			_cmdListenerMukAirFlapWinterSummerRequest16.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);

			_cmdListenerBsSmReply32.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			_cmdListenerBsSmRequest32.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);

			_cmdListenerBvs1Reply65.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			_cmdListenerBvs2Reply65.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);

			_rtuParamReceiver.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);

			//BsSmDataVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			//BvsDataVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			//BvsDataVm2.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
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
				portContainer = !string.IsNullOrEmpty(filename) ? new SerialPortContainerTest(File.ReadAllText(filename).Split(new[] { " ", Environment.NewLine, "\t", "\n", "\r"}, StringSplitOptions.RemoveEmptyEntries).Select(t => byte.Parse(t, NumberStyles.HexNumber)).ToArray()) : new SerialPortContainerTest();
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
					RaisePropertyChanged(()=> TabHeadersAreLong);
				}
			}
		}

		public RelayCommand OpenPortCommand => _openPortCommand;
		public RelayCommand ClosePortCommand => _closePortCommand;
		public ProgramLogViewModel ProgramLogVm => _programLogVm;
		public IThreadNotifier Notifier => _notifier;
	}
}

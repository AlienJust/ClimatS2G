using System;
using System.Collections.Generic;
using System.IO;
using AlienJust.Adaptation.ConsoleLogger;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Text;
using AlienJust.Support.Text.Contracts;
using DataAbstractionLevel.Low.PsnConfig;
using DataAbstractionLevel.Low.PsnConfig.Contracts;
using DrillingRig.ConfigApp.AppControl.ParamLogger;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow {
	public sealed class SharedAppAbilities : ISharedAppAbilities {
		private const string NoStackInfoText = "[NO STACK INFO]";
		private const string LogSeporator = " > ";
		public AppVersion Version { get; }
		public bool IsHourCountersVisible { get; }
		public RelayMultiLoggerWithStackTraceSimple DebugLogger { get; }
		

		private readonly IPsnProtocolConfiguration _psnConfig;
		private readonly Dictionary<string, SerialChannelWithTimeoutMonitorAndSendReplyAbility> _channels;
		public string TestPortName => "ТЕСТ";
		public IStdNotifier CmdNotifierStd { get; }

		public ModbusRtuParamReceiver RtuParamReceiver { get; }

		public IParamLoggerRegistrationPoint ParamLoggerRegistrationPoint { get; }
		public IParameterLogger ParameterLogger { get; }

		public SharedAppAbilities(string psnProtocolFileName) {
			var isFullVersion = File.Exists("FullVersion.txt");
			var isHalfOrFullVersion = isFullVersion;
			if (!isHalfOrFullVersion) isHalfOrFullVersion = File.Exists("HalfVersion.txt");

			Version = isFullVersion ? AppVersion.Full :
				isHalfOrFullVersion ? AppVersion.Half : AppVersion.Base;

			IsHourCountersVisible = File.Exists("HourCounters.txt");

			ILoggerWithStackTrace logConsoleDarkRed = new RelayLoggerWithStackTrace(
				new RelayLogger(new ColoredConsoleLogger(ConsoleColor.DarkRed, ConsoleColor.Black),
					new ChainedFormatter(new List<ITextFormatter> { new ThreadFormatter(LogSeporator, true, false, false), new DateTimeFormatter(LogSeporator) })),
				new StackTraceFormatterWithNullSuport(LogSeporator, NoStackInfoText));
			ILoggerWithStackTrace logConsoleRed = new RelayLoggerWithStackTrace(
				new RelayLogger(new ColoredConsoleLogger(ConsoleColor.Red, ConsoleColor.Black),
					new ChainedFormatter(new List<ITextFormatter> { new ThreadFormatter(LogSeporator, true, false, false), new DateTimeFormatter(LogSeporator) })),
				new StackTraceFormatterWithNullSuport(LogSeporator, NoStackInfoText));
			ILoggerWithStackTrace logConsoleYellow = new RelayLoggerWithStackTrace(
				new RelayLogger(new ColoredConsoleLogger(ConsoleColor.Yellow, ConsoleColor.Black),
					new ChainedFormatter(new List<ITextFormatter> { new ThreadFormatter(LogSeporator, true, false, false), new DateTimeFormatter(LogSeporator) })),
				new StackTraceFormatterWithNullSuport(LogSeporator, NoStackInfoText));
			ILoggerWithStackTrace logConsoleDarkCyan = new RelayLoggerWithStackTrace(
				new RelayLogger(new ColoredConsoleLogger(ConsoleColor.DarkCyan, ConsoleColor.Black),
					new ChainedFormatter(new List<ITextFormatter> { new ThreadFormatter(LogSeporator, true, false, false), new DateTimeFormatter(LogSeporator) })),
				new StackTraceFormatterWithNullSuport(LogSeporator, NoStackInfoText));
			ILoggerWithStackTrace logConsoleCyan = new RelayLoggerWithStackTrace(
				new RelayLogger(new ColoredConsoleLogger(ConsoleColor.Cyan, ConsoleColor.Black),
					new ChainedFormatter(new List<ITextFormatter> { new ThreadFormatter(LogSeporator, true, false, false), new DateTimeFormatter(LogSeporator) })),
				new StackTraceFormatterWithNullSuport(LogSeporator, NoStackInfoText));
			ILoggerWithStackTrace logConsoleGreen = new RelayLoggerWithStackTrace(
				new RelayLogger(new ColoredConsoleLogger(ConsoleColor.Green, ConsoleColor.Black),
					new ChainedFormatter(new List<ITextFormatter> { new ThreadFormatter(LogSeporator, true, false, false), new DateTimeFormatter(LogSeporator) })),
				new StackTraceFormatterWithNullSuport(LogSeporator, NoStackInfoText));
			ILoggerWithStackTrace logConsoleWhite = new RelayLoggerWithStackTrace(
				new RelayLogger(new ColoredConsoleLogger(ConsoleColor.White, ConsoleColor.Black),
					new ChainedFormatter(new List<ITextFormatter> { new ThreadFormatter(LogSeporator, true, false, false), new DateTimeFormatter(LogSeporator) })),
				new StackTraceFormatterWithNullSuport(LogSeporator, NoStackInfoText));

			DebugLogger = new RelayMultiLoggerWithStackTraceSimple(logConsoleDarkRed, logConsoleRed,
				logConsoleYellow, logConsoleDarkCyan, logConsoleDarkCyan, logConsoleCyan, logConsoleGreen,
				logConsoleWhite);

			_psnConfig = new PsnProtocolConfigurationLoaderFromXml(Path.Combine(Environment.CurrentDirectory, psnProtocolFileName)).LoadConfiguration();
			Console.WriteLine("PSN config loaded: " + _psnConfig.Information.Description);
			//var portConatiner = new SerialPortContainerRealWithTest(TestPortName, new SerialPortContainerReal(), new SerialPortContainerTest(File.ReadAllText("CabinIoSample.txt").Split(' ').Select(t => byte.Parse(t, NumberStyles.HexNumber)).ToList()));

			//SerialChannel = new SerialChannel(new CommandPartSearcherPsnConfigBasedFast(_psnConfig), logConsoleYellow);
			//Console.WriteLine("Serial channel created");

			RtuParamReceiver = new ModbusRtuParamReceiver();

			CmdNotifierStd = new StdNotifier();
			CmdNotifierStd.AddListener(RtuParamReceiver);
			_channels = new Dictionary<string, SerialChannelWithTimeoutMonitorAndSendReplyAbility>();

			var paramLoggerAndRegPoint = new ParamLoggerRegistrationPointThreadSafe();
			ParameterLogger = paramLoggerAndRegPoint;
			ParamLoggerRegistrationPoint = paramLoggerAndRegPoint;
		}

		public SerialChannelWithTimeoutMonitorAndSendReplyAbility CreateChannel(string channelName) {
			var serialChannel = new SerialChannelWithTimeoutMonitorAndSendReplyAbility(new SerialChannel(new CommandPartSearcherPsnConfigBasedFast(_psnConfig), DebugLogger.GetLogger(3)));
			_channels.Add(channelName, serialChannel);
			CmdNotifierStd.AddSerialChannel(serialChannel.Channel);
			Console.WriteLine("Serial channel created, with timeout monitor and sending reply abbility, blackjack and hookers");
			return serialChannel;
		}

		public void DestroyChannel(string channelName) {
			if (_channels.ContainsKey(channelName)) {
				var channel = _channels[channelName];
				_channels.Remove(channelName);
				CmdNotifierStd.RemoveSerialChannel(channel.Channel);
				channel.BecameUnused();
				Console.WriteLine("Serial channel destroyed");
			}
		}
	}
}

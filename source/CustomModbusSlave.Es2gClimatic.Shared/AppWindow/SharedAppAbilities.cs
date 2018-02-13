using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AlienJust.Adaptation.ConsoleLogger;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Text;
using AlienJust.Support.Text.Contracts;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbus.Slave.FastReply.Queued;
using CustomModbusSlave.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.CommandHearedTimer;
using DataAbstractionLevel.Low.PsnConfig;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow {
	public sealed class SharedAppAbilities : ISharedAppAbilities {
		private const string NoStackInfoText = "[NO STACK INFO]";
		private const string LogSeporator = " > ";
		private readonly List<ICmdListenerStd> _cmdListeners;


		public string TestPortName => "ТЕСТ";

		public SerialChannel SerialChannel { get; }

		public CommandHearedTimerNotThreadSafe CommandHearedTimeoutMonitor { get; }

		public AppVersion Version { get; }
		public IFastReplyGenerator ReplyGenerator { get; }
		public IFastReplyAcceptor ReplyAcceptor { get; }
		public IParameterSetter ParamSetter { get; }
		public ModbusRtuParamReceiver RtuParamReceiver { get; }
		public RelayMultiLoggerWithStackTraceSimple DebugLogger { get; }

		public SharedAppAbilities(string psnProtocolFileName) {
			var isFullVersion = File.Exists("FullVersion.txt");
			var isHalfOrFullVersion = isFullVersion;
			if (!isHalfOrFullVersion) isHalfOrFullVersion = File.Exists("HalfVersion.txt");

			Version = isFullVersion ? AppVersion.Full :
				isHalfOrFullVersion ? AppVersion.Half : AppVersion.Base;

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

			var psnConfig = new PsnProtocolConfigurationLoaderFromXml(Path.Combine(Environment.CurrentDirectory, psnProtocolFileName)).LoadConfiguration();
			Console.WriteLine("PSN config loaded: " + psnConfig.Information.Description);
			//var portConatiner = new SerialPortContainerRealWithTest(TestPortName, new SerialPortContainerReal(), new SerialPortContainerTest(File.ReadAllText("CabinIoSample.txt").Split(' ').Select(t => byte.Parse(t, NumberStyles.HexNumber)).ToList()));

			SerialChannel = new SerialChannel(new CommandPartSearcherPsnConfigBasedFast(psnConfig), logConsoleYellow);
			Console.WriteLine("Serial channel created");

			var replyGenerator = new ReplyGeneratorWithQueueAttempted();
			ReplyGenerator = replyGenerator;
			ReplyAcceptor = replyGenerator;
			ParamSetter = replyGenerator;

			RtuParamReceiver = new ModbusRtuParamReceiver();
			_cmdListeners = new List<ICmdListenerStd>{RtuParamReceiver};
			// TODO: add custom std cmd listeners


			SerialChannel.CommandHearedWithReplyPossibility += SerialChannelOnCommandHearedWithReplyPossibility;
			SerialChannel.CommandHeared += SerialChannelOnCommandHeared;

			CommandHearedTimeoutMonitor = new CommandHearedTimerNotThreadSafe(SerialChannel, TimeSpan.FromSeconds(1));
			CommandHearedTimeoutMonitor.NoAnyCommandWasHearedTooLong += CommandHearedTimeoutMonitorOnNoAnyCommandWasHearedTooLong;
			CommandHearedTimeoutMonitor.SomeCommandWasHeared += CommandHearedTimeoutMonitorOnSomeCommandWasHeared;
			CommandHearedTimeoutMonitor.Start();
		}

		private void CommandHearedTimeoutMonitorOnSomeCommandWasHeared() {
			//throw new NotImplementedException();
		}

		private void CommandHearedTimeoutMonitorOnNoAnyCommandWasHearedTooLong() {
			//throw new NotImplementedException();
		}

		private void SerialChannelOnCommandHeared(ICommandPart commandPart) {
			foreach (var cmdListenerStd in _cmdListeners) {
				cmdListenerStd.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
			}
		}

		private void SerialChannelOnCommandHearedWithReplyPossibility(ICommandPart commandPart, ISendAbility sendability) {
			if (commandPart.Address == 20) {
				//_notifier.Notify(() => _recordedData.Add(commandPart.ReplyBytes));

				if (commandPart.CommandCode == 33 && commandPart.ReplyBytes.Count == 8) {
					ReplyAcceptor.AcceptReply(commandPart.ReplyBytes.ToArray()); // TODO: bad performance (.ToArray())
					var reply = ReplyGenerator.GenerateReply();
					sendability.Send(reply);
				}
			}
		}
	}
}
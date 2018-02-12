using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using AlienJust.Support.Text.Contracts;
using CustomModbus.Slave.FastReply.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow {
	public sealed class TabItemViewModel : ViewModelBase {
		public string ShortHeader { get; set; }
		public string FullHeader { get; set; }
		public FrameworkElement Content { get; set; }
	}


	public sealed class SharedAppAbilities : ISharedAppAbilities {
		private const string NoStackInfoText = "[NO STACK INFO]";
		private const string LogSeporator = " > ";

		public string TestPortName => "ТЕСТ";
		
		private RelayMultiLoggerWithStackTraceSimple _debugLogger;
		private SerialChannel _serialChannel;

		private ILoggerWithStackTrace _logConsoleDarkRed;
		private ILoggerWithStackTrace _logConsoleRed;
		private ILoggerWithStackTrace _logConsoleYellow;
		private ILoggerWithStackTrace _logConsoleDarkCyan;
		private ILoggerWithStackTrace _logConsoleCyan;
		private ILoggerWithStackTrace _logConsoleGreen;
		private ILoggerWithStackTrace _logConsoleWhite;

		public AppVersion Version { get; }
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

			_logConsoleDarkRed = new RelayLoggerWithStackTrace(
				new RelayLogger(new ColoredConsoleLogger(ConsoleColor.DarkRed, ConsoleColor.Black),
				new ChainedFormatter(new List<ITextFormatter> { new ThreadFormatter(LogSeporator, true, false, false), new DateTimeFormatter(LogSeporator) })),
				new StackTraceFormatterWithNullSuport(LogSeporator, NoStackInfoText));
			_logConsoleRed = new RelayLoggerWithStackTrace(
				new RelayLogger(new ColoredConsoleLogger(ConsoleColor.Red, ConsoleColor.Black),
				new ChainedFormatter(new List<ITextFormatter> { new ThreadFormatter(LogSeporator, true, false, false), new DateTimeFormatter(LogSeporator) })),
				new StackTraceFormatterWithNullSuport(LogSeporator, NoStackInfoText));
			_logConsoleYellow = new RelayLoggerWithStackTrace(
				new RelayLogger(new ColoredConsoleLogger(ConsoleColor.Yellow, ConsoleColor.Black),
				new ChainedFormatter(new List<ITextFormatter> { new ThreadFormatter(LogSeporator, true, false, false), new DateTimeFormatter(LogSeporator) })),
				new StackTraceFormatterWithNullSuport(LogSeporator, NoStackInfoText));
			_logConsoleDarkCyan = new RelayLoggerWithStackTrace(
				new RelayLogger(new ColoredConsoleLogger(ConsoleColor.DarkCyan, ConsoleColor.Black),
				new ChainedFormatter(new List<ITextFormatter> { new ThreadFormatter(LogSeporator, true, false, false), new DateTimeFormatter(LogSeporator) })),
				new StackTraceFormatterWithNullSuport(LogSeporator, NoStackInfoText));
			_logConsoleCyan = new RelayLoggerWithStackTrace(
				new RelayLogger(new ColoredConsoleLogger(ConsoleColor.Cyan, ConsoleColor.Black),
				new ChainedFormatter(new List<ITextFormatter> { new ThreadFormatter(LogSeporator, true, false, false), new DateTimeFormatter(LogSeporator) })),
				new StackTraceFormatterWithNullSuport(LogSeporator, NoStackInfoText));
			_logConsoleGreen = new RelayLoggerWithStackTrace(
				new RelayLogger(new ColoredConsoleLogger(ConsoleColor.Green, ConsoleColor.Black),
				new ChainedFormatter(new List<ITextFormatter> { new ThreadFormatter(LogSeporator, true, false, false), new DateTimeFormatter(LogSeporator) })),
				new StackTraceFormatterWithNullSuport(LogSeporator, NoStackInfoText));
			_logConsoleWhite = new RelayLoggerWithStackTrace(
				new RelayLogger(new ColoredConsoleLogger(ConsoleColor.White, ConsoleColor.Black),
				new ChainedFormatter(new List<ITextFormatter> { new ThreadFormatter(LogSeporator, true, false, false), new DateTimeFormatter(LogSeporator) })),
				new StackTraceFormatterWithNullSuport(LogSeporator, NoStackInfoText));
		}
	}

	public interface ISharedAppAbilities {
		AppVersion Version { get; }
		IFastReplyAcceptor ReplyAcceptor { get; }
		IParameterSetter ParamSetter { get; }
		ModbusRtuParamReceiver RtuParamReceiver { get; }
		string TestPortName { get; }
		RelayMultiLoggerWithStackTraceSimple DebugLogger { get; }
	}

	public enum AppVersion {
		Full,
		Half,
		Base
	}
}
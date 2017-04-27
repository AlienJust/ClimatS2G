using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using AlienJust.Adaptation.ConsoleLogger;
using AlienJust.Adaptation.WindowsPresentation;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Text;
using AlienJust.Support.Text.Contracts;
using DataAbstractionLevel.Low.PsnConfig;

namespace CustomModbusSlave.Es2gClimatic.CabinApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {
		private const string TestPortName = "ТЕСТ";
		private const string NoStackInfoText = "[NO STACK INFO]";
		private const string LogSeporator = " > ";

		private ManualResetEvent _mainWindowCreationCompleteWaiter;
		private RelayMultiLoggerWithStackTraceSimple _debugLogger;
		private SerialChannel _serialChannel;

		private ILoggerWithStackTrace _logConsoleDarkRed;
		private ILoggerWithStackTrace _logConsoleRed;
		private ILoggerWithStackTrace _logConsoleYellow;
		private ILoggerWithStackTrace _logConsoleDarkCyan;
		private ILoggerWithStackTrace _logConsoleCyan;
		private ILoggerWithStackTrace _logConsoleGreen;
		private ILoggerWithStackTrace _logConsoleWhite;

		private void App_OnStartup(object sender, StartupEventArgs e) {
			_logConsoleDarkRed = new RelayLoggerWithStackTrace(
				new RelayLogger(new ColoredConsoleLogger(ConsoleColor.DarkRed, ConsoleColor.Black), 
				new ChainedFormatter(new List<ITextFormatter> {new ThreadFormatter(LogSeporator, true, false, false), new DateTimeFormatter(LogSeporator)})), 
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

			var psnConfig = new PsnProtocolConfigurationLoaderFromXml(Path.Combine(Environment.CurrentDirectory, "psn.Микроклимат-ЭС2ГП-кабина.xml")).LoadConfiguration();
			_serialChannel = new SerialChannel(new CommandPartSearcherPsnConfigBasedFast(psnConfig), _logConsoleYellow);


			List<Action> closeChildWindowsActions = new List<Action>(); // TODO: here to add close child windows
			var appThreadNotifier = new WpfUiNotifierAsync(System.Windows.Threading.Dispatcher.CurrentDispatcher);

			_mainWindowCreationCompleteWaiter = new ManualResetEvent(false);
			var mainWindowThread = new Thread(() => {
				var mainViewModel = new MainViewModel(
					new WpfUiNotifierAsync(System.Windows.Threading.Dispatcher.CurrentDispatcher), 
					new WpfWindowSystem(),
					_debugLogger, _serialChannel, TestPortName);

				var mainWindow = new MainView(appThreadNotifier, () => {
					foreach (var closingAction in closeChildWindowsActions) {
						closingAction.Invoke();
					}
					closeChildWindowsActions.Clear();
				}
				) { DataContext = mainViewModel };
				mainWindow.Show();

				_mainWindowCreationCompleteWaiter.Set();
				System.Windows.Threading.Dispatcher.Run();
			});
			mainWindowThread.SetApartmentState(ApartmentState.STA);
			mainWindowThread.Priority = ThreadPriority.AboveNormal;
			mainWindowThread.IsBackground = true;
			mainWindowThread.Start();

			_mainWindowCreationCompleteWaiter.WaitOne();
		}
	}
}

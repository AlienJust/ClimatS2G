using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using AlienJust.Adaptation.ConsoleLogger;
using AlienJust.Adaptation.WindowsPresentation;
using AlienJust.Support.Loggers;
using AlienJust.Support.Text;
using AlienJust.Support.Text.Contracts;

namespace CustomModbusSlave.Es2gClimatic.CabinApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {
		private ManualResetEvent _mainWindowCreationCompleteWaiter;
		private RelayMultiLoggerWithStackTraceSimple _debugLogger;


		private void App_OnStartup(object sender, StartupEventArgs e) {
			_debugLogger = new RelayMultiLoggerWithStackTraceSimple(
				new RelayLoggerWithStackTrace(
					new RelayActionLogger(s => { }),
					//new RelayLogger(
					//new ColoredConsoleLogger(ConsoleColor.DarkRed, ConsoleColor.Black),
					//new ChainedFormatter(new List<ITextFormatter> { new ThreadFormatter(" > ", true, false, false), new DateTimeFormatter(" > ") })),
					new StackTraceFormatterWithNullSuport(" > ", "[NO STACK INFO]")),
				new RelayLoggerWithStackTrace(
					new RelayLogger(
						new ColoredConsoleLogger(ConsoleColor.Red, ConsoleColor.Black),
						new ChainedFormatter(new List<ITextFormatter>
						{
							new ThreadFormatter(" > ", true, false, false),
							new DateTimeFormatter(" > ")
						})),
					new StackTraceFormatterWithNullSuport(" > ", "[NO STACK INFO]")),
				new RelayLoggerWithStackTrace(
					new RelayLogger(
						new ColoredConsoleLogger(ConsoleColor.Yellow, ConsoleColor.Black),
						new ChainedFormatter(new List<ITextFormatter>
						{
							new ThreadFormatter(" > ", true, false, false),
							new DateTimeFormatter(" > ")
						})),
					new StackTraceFormatterWithNullSuport(" > ", "[NO STACK INFO]")),
				new RelayLoggerWithStackTrace(
					new RelayLogger(
						new ColoredConsoleLogger(ConsoleColor.DarkCyan, ConsoleColor.Black),
						new ChainedFormatter(new List<ITextFormatter>
						{
							new ThreadFormatter(" > ", true, false, false),
							new DateTimeFormatter(" > ")
						})),
					//new StackTraceFormatterWithNullSuport(" > ", "[NO STACK INFO]")),
					new StackTraceFormatterNothing()),
				new RelayLoggerWithStackTrace(
					new RelayLogger(
						new ColoredConsoleLogger(ConsoleColor.Cyan, ConsoleColor.Black),
						new ChainedFormatter(new List<ITextFormatter>
						{
							new ThreadFormatter(" > ", true, false, false),
							new DateTimeFormatter(" > ")
						})),
					//new StackTraceFormatterWithNullSuport(" > ", "[NO STACK INFO]")),
					new StackTraceFormatterNothing()),
				new RelayLoggerWithStackTrace(
					new RelayLogger(
						new ColoredConsoleLogger(ConsoleColor.Green, ConsoleColor.Black),
						new ChainedFormatter(new List<ITextFormatter> { new ThreadFormatter(" > ", false, true, false), new DateTimeFormatter(" > ") })),
					new StackTraceFormatterWithNullSuport(" > ", string.Empty)),
				new RelayLoggerWithStackTrace(
					new RelayLogger(
						new ColoredConsoleLogger(ConsoleColor.White, ConsoleColor.Black),
						new ChainedFormatter(new List<ITextFormatter> { new ThreadFormatter(" > ", true, false, false), new DateTimeFormatter(" > ") })),
					new StackTraceFormatterNothing()));

			List<Action> closeChildWindowsActions = new List<Action>(); // TODO: here to add close child windows
			var appThreadNotifier = new WpfUiNotifierAsync(System.Windows.Threading.Dispatcher.CurrentDispatcher);

			_mainWindowCreationCompleteWaiter = new ManualResetEvent(false);
			var mainWindowThread = new Thread(() => {
				var mainViewModel = new MainViewModel(
					new WpfUiNotifierAsync(System.Windows.Threading.Dispatcher.CurrentDispatcher), 
					new WpfWindowSystem(),
					_debugLogger);

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

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using AlienJust.Adaptation.WindowsPresentation;
using AlienJust.Support.Concurrent.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow {

	/// <summary>
	/// Create new WPF application, in app.xaml.cs OnStartup() method call this class constructor and create new window
	/// </summary>
	public sealed class AppFactory {
		private readonly Lazy<ISharedAppAbilities> _abilities;
		private readonly ManualResetEvent _mainWindowCreationCompleteWaiter;
		private readonly List<Action> _closeChildWindowsActions;

		public AppFactory(string psnProtocolFileName) {
			_abilities = new Lazy<ISharedAppAbilities>(() => new SharedAppAbilities(psnProtocolFileName));
			_mainWindowCreationCompleteWaiter = new ManualResetEvent(false);
			_closeChildWindowsActions = new List<Action>(); // TODO: here to add close child windows
		}

		public ISharedAppAbilities Abilities => _abilities.Value;
		
		/// <summary>
		/// T - is DataContext type (ViewModel)
		/// </summary>
		/// <param name="childWindow"></param>
		public void ShowChildWindowInOwnThread<T>(Window childWindowView, Func<IThreadNotifier, T> viewModelCreator) {

		}

		/// <summary>
		/// Shows main window and calls back function in window thread with window view model as argument
		/// </summary>
		/// <param name="windowTitle">Window title</param>
		/// <param name="appAbilities">Window need for some app abilities, use AppFactory.Abilities property to take them</param>
		/// <param name="callback">Callback is needed to complete window creation with tabs</param>
		public void ShowMainWindowInOwnThread(string windowTitle, ISharedAppAbilities appAbilities, Action<ISharedMainViewModel> callback) {
			var appThreadNotifier = new WpfUiNotifierAsync(System.Windows.Threading.Dispatcher.CurrentDispatcher);

			var mainWindowThread = new Thread(() => {
				var mainWindowNotifier = new WpfUiNotifierAsync(System.Windows.Threading.Dispatcher.CurrentDispatcher);
				var windowSystem = new WpfWindowSystem();

				var mainViewModel = new SharedMainViewModel(mainWindowNotifier, windowSystem, windowTitle, appAbilities);
				var mainWindow = new SharedMainView(appThreadNotifier, () => {
					foreach (var closingAction in _closeChildWindowsActions) {
						closingAction.Invoke();
					}
					_closeChildWindowsActions.Clear();
				}
				) { DataContext = mainViewModel };
				mainWindow.Show();
				callback(mainViewModel);
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
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using AlienJust.Adaptation.WindowsPresentation;
using AlienJust.Support.Concurrent.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    /// <summary>
    /// Create new WPF application, in app.xaml.cs OnStartup() method call this class constructor and create new window
    /// </summary>
    public sealed class AppFactory
    {
        private readonly Lazy<ISharedAppAbilities> _abilities;
        private readonly ManualResetEvent _mainWindowCreationCompleteWaiter;
        private readonly List<Action> _closeChildWindowsActions;

        public AppFactory(string psnProtocolFileName)
        {
            _abilities = new Lazy<ISharedAppAbilities>(() => new SharedAppAbilities(psnProtocolFileName));
            _mainWindowCreationCompleteWaiter = new ManualResetEvent(false);
            _closeChildWindowsActions = new List<Action>(); // TODO: here to add close child windows
        }

        public ISharedAppAbilities Abilities => _abilities.Value;

        public void ShowChildWindowInOwnThread(Func<IThreadNotifier, WindowAndClosableViewModel> windowCreateFunc)
        {
            var childWindowWaiter = new ManualResetEvent(false);
            var childWindowThread = new Thread(() =>
            {
                //var uiNotifier = new WpfUiNotifierAsync(System.Windows.Threading.Dispatcher.CurrentDispatcher);
                var uiNotifier = new WpfUiNotifierAsync(System.Windows.Threading.Dispatcher.CurrentDispatcher);
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " > uiNotifier created, line before window and WM were created");
                var windowAndVm = windowCreateFunc.Invoke(uiNotifier);
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " > window and WM were created");

                windowAndVm.Window.DataContext = windowAndVm.WindowVm;
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " > window WM assigned as window DataContext");

                _closeChildWindowsActions.Add(() => uiNotifier.Notify(() =>
                {
                    windowAndVm.Window.Close();
                    windowAndVm.WindowVm.NotifyWindowIsClosed();
                }));
                windowAndVm.Window.Show();
                childWindowWaiter.Set();
                System.Windows.Threading.Dispatcher.Run();
            });

            childWindowThread.SetApartmentState(ApartmentState.STA);
            childWindowThread.IsBackground = true;
            childWindowThread.Priority = ThreadPriority.BelowNormal;
            childWindowThread.Start();
            childWindowWaiter.WaitOne();
        }

        /// <summary>
        /// Shows main window and calls back function in window thread with window view model as argument
        /// </summary>
        /// <param name="windowTitle">Window title</param>
        /// <param name="appAbilities">Window need for some app abilities, use AppFactory.Abilities property to take them</param>
        /// <param name="callback">Callback is needed to complete window creation with tabs</param>
        public void ShowMainWindowInOwnThread(string windowTitle, ISharedAppAbilities appAbilities, Action<ISharedMainViewModel> callback)
        {
            var appThreadNotifier = new WpfUiNotifierAsync(System.Windows.Threading.Dispatcher.CurrentDispatcher);

            var mainWindowThread = new Thread(() =>
            {
                try
                {
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " > Main window thread started");
                    var mainWindowNotifier = new WpfUiNotifierAsync(System.Windows.Threading.Dispatcher.CurrentDispatcher);
                    var windowSystem = new WpfWindowSystem();
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " > uiNotifier and WpfWindowSystem created, line before window and WM were created");
                    var mainViewModel = new SharedMainViewModel(mainWindowNotifier, windowSystem, windowTitle, appAbilities);
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " > windowVM was created");
                    var mainWindow = new SharedMainView(appThreadNotifier, () =>
                    {
                        foreach (var closingAction in _closeChildWindowsActions)
                        {
                            closingAction.Invoke();
                        }

                        _closeChildWindowsActions.Clear();
                    }) {DataContext = mainViewModel};
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " > mainWindow was created, let's call it's .Show() method");

                    mainWindow.Show();
                    //mainWindow.ShowDialog();
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " > mainWindow.Show() was called");

                    callback(mainViewModel);
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " > Callback was fired");

                    _mainWindowCreationCompleteWaiter.Set();
                    System.Windows.Threading.Dispatcher.Run();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
            mainWindowThread.SetApartmentState(ApartmentState.STA);
            mainWindowThread.Priority = ThreadPriority.Normal;
            mainWindowThread.IsBackground = false;
            mainWindowThread.Start();
            _mainWindowCreationCompleteWaiter.WaitOne();
        }
    }

    public sealed class WindowAndClosableViewModel
    {
        public WindowAndClosableViewModel(Window window, IClosableVm windowVm)
        {
            Window = window;
            WindowVm = windowVm;
        }

        public Window Window { get; }
        public IClosableVm WindowVm { get; }
    }

    public interface IClosableVm
    {
        void NotifyWindowIsClosed();
    }
}
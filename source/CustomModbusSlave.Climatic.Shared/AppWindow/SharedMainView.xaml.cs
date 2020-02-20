using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using AlienJust.Support.Concurrent.Contracts;
using MahApps.Metro.Controls;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    /// <summary>
    /// Interaction logic for SharedMainView.xaml
    /// </summary>
    public partial class SharedMainView : MetroWindow
    {
        private readonly IThreadNotifier _appMainThreadNotifier;
        private readonly Action _closeOtherWindows;

        public SharedMainView(IThreadNotifier appMainThreadNotifier, Action closeOtherWindows)
        {
            _appMainThreadNotifier = appMainThreadNotifier;
            _closeOtherWindows = closeOtherWindows;
            InitializeComponent();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _closeOtherWindows.Invoke();
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            _appMainThreadNotifier.Notify(() =>
            {
                Application.Current.Shutdown();
                Thread.Sleep(555); // TODO: make it proper
                Process.GetCurrentProcess().Kill();
            });
        }
    }
}

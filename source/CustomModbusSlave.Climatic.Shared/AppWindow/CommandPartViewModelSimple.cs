using System;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using DataAbstractionLevel.Low.PsnConfig.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    internal sealed class CommandPartViewModelSimple : ViewModelBase, ICommandPartViewModel
    {
        
        private readonly ICommandPartListener _listener;
        private readonly IThreadNotifier _uiNotifier;

        public string Name { get; }

        private readonly IPsnProtocolCommandPartConfiguration _config;

        private string _dataText;
        public string DataText
        {
            get => _dataText;
            set
            {
                if (_dataText != value)
                {
                    _dataText = value;
                    RaisePropertyChanged(() => DataText);
                }
            }
        }

        private string _receivedTimeText;
        public string ReceiveTimeText
        {
            get => _receivedTimeText;
            set
            {
                if (_receivedTimeText != value)
                {
                    _receivedTimeText = value;
                    RaisePropertyChanged(() => ReceiveTimeText);
                }
            }
        }

        public CommandPartViewModelSimple(ICommandPartListener listener, IThreadNotifier uiNotifier, IPsnProtocolCommandPartConfiguration config)
        {
            _listener = listener;
            _uiNotifier = uiNotifier;
            _config = config;

            _listener.CommandPartReceived += ListenerValueReceived;
            _dataText = "?";
            ReceiveTimeText = "?";
        }

        private void ListenerValueReceived(object sender, CommandPartReceivedEventArgs e)
        {
            if (e.Data.CmdPartConfig.Id.IdentyString == _config.Id.IdentyString)
            {
                _uiNotifier.Notify(() => {

                    DataText = e.Data.DataBytes.ToText();
                    ReceiveTimeText = DateTime.Now.ToString("HH:mm:ss");
                });
            }
        }
    }
}
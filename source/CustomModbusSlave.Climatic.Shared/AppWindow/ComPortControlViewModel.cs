using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows.Input;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.UserInterface.Contracts;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.Record;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public sealed class ComPortControlViewModel : ViewModelBase
    {
        private readonly ISharedAppAbilities _appAbilities;
        private readonly ILogger _logger;
        private readonly IThreadNotifier _notifier;
        private readonly IWindowSystem _windowSystem;

        public RecordViewModel RecordVm { get; }
        private readonly RelayCommand _openPortCommand;
        private readonly RelayCommand _closePortCommand;
        public ICommand GetPortsAvailableCommand { get; }

        private bool _isPortOpened;

        private List<string> _comPortsAvailable;
        private string _selectedComName;

        private readonly SerialChannelWithTimeoutMonitorAndSendReplyAbility _channel;
        private readonly ISerialChannelWithIoProgress _channelWithIoProgress;

        //private readonly CommandHearedTimerNotThreadSafe _commandHearedTimeoutMonitor;
        private Colors _linkBackColor;
        private double _progress;

        public ComPortControlViewModel(ISharedAppAbilities appAbilities, ILogger logger, IThreadNotifier notifier, IWindowSystem windowSystem, string channelName)
        {
            _appAbilities = appAbilities;
            _logger = logger;
            _notifier = notifier;
            _windowSystem = windowSystem;
            _openPortCommand = new RelayCommand(OpenPort, () => !_isPortOpened);
            _closePortCommand = new RelayCommand(ClosePort, () => _isPortOpened);
            GetPortsAvailableCommand = new RelayCommand(GetPortsAvailable);
            RecordVm = new RecordViewModel(_notifier, _windowSystem);


            //TODO: unsubscribe
            _channel = _appAbilities.CreateChannel(channelName);
            _channel.Channel.CommandHeared += SerialChannelOnCommandHeared;
            _channel.Channel.CommandHearedWithReplyPossibility += SerialChannelOnCommandHearedWithReplyPossibility;
            _channel.TimeoutMonitor.SomeCommandWasHeared += CommandHearedTimeoutMonitorOnSomeCommandWasHeared;
            _channel.TimeoutMonitor.NoAnyCommandWasHearedTooLong += CommandHearedTimeoutMonitorOnNoAnyCommandWasHearedTooLong;

            _channelWithIoProgress = _channel.Channel as ISerialChannelWithIoProgress;
            if (_channelWithIoProgress != null) _channelWithIoProgress.ProgressChanged += ChannelWithIoProgressOnProgressChanged;


            GetPortsAvailable();

            _logger.Log("Канал обмена добавлен");
        }

        private void ChannelWithIoProgressOnProgressChanged(double progressPercentage)
        {
            _notifier.Notify(() => Progress = progressPercentage);
        }

        private void OpenPort()
        {
            Progress = 0.0;
            ISerialPortContainer portContainer;
            if (_selectedComName == _appAbilities.TestPortName)
            {
                var filename = _windowSystem.ShowOpenFileDialog("Текстовый файл с данными", "Текстовые файлы|*.txt|Все файлы|*.*");
                portContainer = !string.IsNullOrEmpty(filename)
                    ? new SerialPortContainerTest(File.ReadAllText(filename)
                        .Split(new[] {" ", Environment.NewLine, "\t", "\n", "\r"},
                            StringSplitOptions.RemoveEmptyEntries).Select(t => byte.Parse(t, NumberStyles.HexNumber))
                        .ToArray())
                    : new SerialPortContainerTest();
            }
            else
            {
                portContainer = new SerialPortContainerReal(_selectedComName, 57600);
            }

            _channel.Channel.SelectPortAsync(portContainer, ex => _notifier.Notify(() =>
            {
                if (ex == null)
                {
                    IsPortOpened = true;
                    _logger.Log("Порт " + _selectedComName + " открыт");
                    _closePortCommand.RaiseCanExecuteChanged();
                    _openPortCommand.RaiseCanExecuteChanged();
                }
                else
                {
                    _logger.Log("Ошибка во время открытия порта: " + ex);
                }
            }));
        }

        private void GetPortsAvailable()
        {
            var ports = new List<string> {_appAbilities.TestPortName};
            ports.AddRange(SerialPort.GetPortNames());
            ComPortsAvailable = ports;
            if (ComPortsAvailable.Count > 0) SelectedComName = ComPortsAvailable[0];
            _logger.Log("Список COM-портов получен");
        }

        private void ClosePort()
        {
            _channel.Channel.CloseCurrentPortAsync(ex => _notifier.Notify(() =>
            {
                if (ex == null)
                {
                    IsPortOpened = false;
                    _logger.Log("Порт " + _selectedComName + " закрыт");
                    _closePortCommand.RaiseCanExecuteChanged();
                    _openPortCommand.RaiseCanExecuteChanged();
                }
                else
                {
                    _logger.Log("Ошибка во время закрытия порта: " + ex);
                }
            }));
        }


        private void CommandHearedTimeoutMonitorOnSomeCommandWasHeared()
        {
            _notifier.Notify(() => { LinkBackColor = Colors.LimeGreen; });
        }

        private void CommandHearedTimeoutMonitorOnNoAnyCommandWasHearedTooLong()
        {
            _notifier.Notify(() => { LinkBackColor = Colors.OrangeRed; });
        }


        private void SerialChannelOnCommandHearedWithReplyPossibility(ICommandPart commandPart, ISendAbility sendAbility)
        {
            // TODO: RecordVm or not?
            //RecordVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
        }

        private void SerialChannelOnCommandHeared(ICommandPart commandPart)
        {
            RecordVm.ReceiveCommand(commandPart.Address, commandPart.CommandCode, commandPart.ReplyBytes);
        }

        public List<string> ComPortsAvailable
        {
            get => _comPortsAvailable;
            set
            {
                if (_comPortsAvailable != value)
                {
                    _comPortsAvailable = value;
                    RaisePropertyChanged(() => ComPortsAvailable);
                }
            }
        }

        public string SelectedComName
        {
            get => _selectedComName;
            set
            {
                if (value != _selectedComName)
                {
                    _selectedComName = value;
                    RaisePropertyChanged(() => SelectedComName);
                }
            }
        }

        public bool IsPortOpened
        {
            get => _isPortOpened;
            set
            {
                if (_isPortOpened != value)
                {
                    _isPortOpened = value;
                    RaisePropertyChanged(() => IsPortOpened);
                }
            }
        }

        public Colors LinkBackColor
        {
            get => _linkBackColor;
            set
            {
                if (_linkBackColor != value)
                {
                    _linkBackColor = value;
                    RaisePropertyChanged(() => LinkBackColor);
                }
            }
        }

        public double Progress
        {
            get => _progress;
            set => SetProp(() => Math.Abs(_progress - value) > 1.0, () => _progress = value, () => Progress);
        }

        public ICommand OpenPortCommand => _openPortCommand;
        public ICommand ClosePortCommand => _closePortCommand;

        public SerialChannelWithTimeoutMonitorAndSendReplyAbility Channel => _channel;

        public IParameterSetter ParameterSetter => _channel.ParamSetter;
    }
}
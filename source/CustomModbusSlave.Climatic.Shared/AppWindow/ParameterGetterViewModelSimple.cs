using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation;
using DrillingRig.ConfigApp.AppControl.ParamLogger;
using System;
using System.Timers;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    internal sealed class ParameterGetterViewModelSimple : ViewModelBase, IParameterGetterViewModel, IDisposable
    {
        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    RaisePropertyChanged(() => Value);
                }
            }
        }

        private bool _isLogged;
        public bool IsLogged
        {
            get => _isLogged;
            set
            {
                if (_isLogged != value)
                {
                    _isLogged = value;
                    RaisePropertyChanged(() => IsLogged);
                }
            }
        }

        private bool _valueIsOld;
        public bool ValueIsOld
        {
            get => _valueIsOld;
            set
            {
                if (_valueIsOld != value)
                {
                    _valueIsOld = value;
                    RaisePropertyChanged(() => ValueIsOld);
                }
            }
        }


        private readonly IParamListener _listener;
        private readonly IThreadNotifier _uiNotifier;
        private readonly IParameterView _view;
        private readonly IParameterLogger _parameterLogger;
        private readonly string _logName;
        private readonly string _paramId;
        private readonly Action<string, double?> _log;

        TimeSpan _linkLostTimeout;
        DateTime _lastRecvTime;
        Timer _timer;

        public ParameterGetterViewModelSimple(
            string paramId, IParamListener listener, IThreadNotifier uiNotifier, 
            IParameterView view, IParameterLogger parameterLogger, bool isBitParam, string logName)
        {
            _timer = new Timer(1000);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
            _listener = listener;
            _uiNotifier = uiNotifier;
            _view = view;
            _parameterLogger = parameterLogger;
            _logName = logName;
            _paramId = paramId;

            _listener.ValueReceived += ListenerValueReceived;
            _log = isBitParam ? (Action<string, double?>)LogBool : _parameterLogger.LogAnalogueParameter;


            _value = "?";
            _isLogged = false;

            _valueIsOld = false;

            _lastRecvTime = DateTime.Now;
            _linkLostTimeout = TimeSpan.FromSeconds(5); // TODO: could be injected via .ctor()
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_lastRecvTime + _linkLostTimeout < DateTime.Now)
            {
                _uiNotifier.Notify(() =>
                {
                    ValueIsOld = true;
                });
            }
        }

        /// <summary>
        /// Optimization (choise is made when object is created)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void LogBool(string name, double? value)
        {
            _parameterLogger.LogDiscreteParameter(name, value.Value > 0);
        }

        private void ListenerValueReceived(object sender, ParameterValueReceivedEventArgs e)
        {
            // TODO: eliminate checking for better performance
            if (e.ParameterId == _paramId)
            {
                _uiNotifier.Notify(() =>
                {
                    Value = _view.GetText(e.Value); // TODO: What if error?
                    ValueIsOld = false;
                    if (IsLogged) _log(_logName, e.Value);
                });
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _listener.ValueReceived -= ListenerValueReceived;

                    _timer.Stop();
                    _timer.Elapsed -= TimerElapsed;
                    _timer.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ParameterGetterViewModelSimple()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    internal sealed class ParameterGetterViewModelSimple : ViewModelBase, IParameterGetterViewModel
    {
        private string _value;
        private readonly IParamListener _listener;
        private readonly IThreadNotifier _uiNotifier;
        private readonly IParameterView _view;
        private readonly string _paramId;

        public ParameterGetterViewModelSimple(string paramId, IParamListener listener, IThreadNotifier uiNotifier, IParameterView view)
        {
            _listener = listener;
            _uiNotifier = uiNotifier;
            _view = view;
            _paramId = paramId;

            _listener.ValueReceived += ListenerValueReceived;
            
            Value = "?";
        }

        private void ListenerValueReceived(object sender, ParameterValueReceivedEventArgs e)
        {
            if (e.ParameterId == _paramId)
            {
                _uiNotifier.Notify(() =>
                {
                    Value = _view.GetText(e.Value);
                });
            }
        }

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
    }
}
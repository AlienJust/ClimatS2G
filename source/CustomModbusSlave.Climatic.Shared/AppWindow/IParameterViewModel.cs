using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation;
using DataAbstractionLevel.Low.PsnConfig.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public interface IParameterViewModel
    {
        string Name { get; }

        string Value { get; }
    }

    internal sealed class ParameterViewModelSimple : ViewModelBase, IParameterViewModel
    {
        private string _value;
        private readonly IParamListener _listener;
        private readonly IThreadNotifier _uiNotifier;

        public string Name { get; }

        public string ParameterId { get; set; }

        private readonly IParameterDescription _parameterDescription;

        public ParameterViewModelSimple(IParamListener listener, IThreadNotifier uiNotifier, IParameterDescription description, string parameterNameFromConfiguration)
        {
            _listener = listener;
            _uiNotifier = uiNotifier;
            _listener.ValueReceived += ListenerValueReceived;
            Name = description.CustomName ?? parameterNameFromConfiguration;
            ParameterId = description.Identifier;
        }

        private void ListenerValueReceived(object sender, ParameterValueReceivedEventArgs e)
        {
            if (e.ParameterId == ParameterId)
            {
                _uiNotifier.Notify(()=> {
                    _parameterDescription.View.GetText(e.Value);
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
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation;
using System;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    internal sealed class ParameterViewModelSimple : ViewModelBase, IParameterViewModel
    {
        private string _value;
        private readonly IParamListener _listener;
        private readonly IThreadNotifier _uiNotifier;

        public string Name { get; }

        private readonly IParameterDescription _parameterDescription;

        public ParameterViewModelSimple(IParamListener listener, IThreadNotifier uiNotifier, IParameterDescription description, string parameterNameFromConfiguration)
        {
            _listener = listener;
            _uiNotifier = uiNotifier;
            _parameterDescription = description;

            _listener.ValueReceived += ListenerValueReceived;
            Name = _parameterDescription.CustomName ?? parameterNameFromConfiguration;
            //ParameterId = description.Identifier;
            Value = "?";
        }

        private void ListenerValueReceived(object sender, ParameterValueReceivedEventArgs e)
        {
            if (e.ParameterId == _parameterDescription.Identifier)
            {
                //Console.WriteLine(e.ParameterId + " > " + Name + " > Received value: " + e.Value.ToString("f2"));
                _uiNotifier.Notify(()=> {
                    Value = _parameterDescription.View.GetText(e.Value);
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
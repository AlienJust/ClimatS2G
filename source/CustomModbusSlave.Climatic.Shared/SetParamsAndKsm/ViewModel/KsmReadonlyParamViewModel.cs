using AlienJust.Support.Collections;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters;
using ParamCentric.Modbus.Contracts;
using ParamCentric.UserInterface.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.ViewModel
{
    public class KsmReadonlyParamViewModel : ViewModelBase, IReceivableParameter, IDisplayableParameter<string>
    {
        private readonly int _zbParameterNumber;
        private readonly ITextFormatter<BytesPair?> _currentValueFormatter;
        private BytesPair? _receivedValue;

        public KsmReadonlyParamViewModel(int zbParameterNumber, string name, ITextFormatter<BytesPair?> currentValueFormatter)
        {
            _zbParameterNumber = zbParameterNumber;
            Name = name;
            _currentValueFormatter = currentValueFormatter;
        }

        public string ReceivedValueFormatted => _currentValueFormatter.Format(_receivedValue);

        public string Name { get; }

        public BytesPair? ReceivedBytesValue
        {
            get => _receivedValue;
            set
            {
                if (_receivedValue != value)
                {
                    _receivedValue = value;
                    RaisePropertyChanged(() => ReceivedBytesValue);
                    RaisePropertyChanged(() => ReceivedValueFormatted);
                }
            }
        }
    }
}
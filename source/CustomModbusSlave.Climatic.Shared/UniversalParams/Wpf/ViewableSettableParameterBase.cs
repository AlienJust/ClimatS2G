using AlienJust.Support.Conversion;
using AlienJust.Support.Conversion.Contracts;
using AlienJust.Support.ModelViewViewModel;
using ParamCentric.UserInterface.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Wpf
{
    public class ViewableSettableParameterBase<TRaw> :
        ViewModelBase, 
        IDisplayableParameter<IRawAndConvertedValues<TRaw?, string>>, 
        ISettableByUserParameter<IRawAndConvertedValues<TRaw, string>> where TRaw : struct
    {
        public string NamePrefix { get; }
        public string Name { get; }

        private IRawAndConvertedValues<TRaw, string> _formattedValue;
        private TRaw? _rawValue;

        private readonly IBuilderOneToOne<TRaw?, string> _receivedValueConvertor;
        public IRawAndConvertedValues<TRaw?, string> ReceivedValueFormatted { get; private set; }

        public string ToolTipText { get; }

        public ViewableSettableParameterBase(string namePrefix, string name, TRaw? defaultValue, IBuilderOneToOne<TRaw, string> stringBuilder, string toolTipText)
        {
            NamePrefix = namePrefix;
            Name = name;
            ToolTipText = toolTipText;

            _receivedValueConvertor = new BuilderOneToOneNullableString<TRaw>(stringBuilder, "?");
            ReceivedValueFormatted = new RawAndConvertedValues<TRaw?, string>(defaultValue, _receivedValueConvertor);
        }

        public void SetReceivedValueRaw(TRaw? value)
        {
            if (!Equals(value, _rawValue))
            {
                _rawValue = value;
                ReceivedValueFormatted = new RawAndConvertedValues<TRaw?, string>(_rawValue, _receivedValueConvertor);
                RaisePropertyChanged(() => ReceivedValueFormatted);
            }
        }

        public IRawAndConvertedValues<TRaw, string> FormattedValue
        {
            get => _formattedValue;
            set
            {
                if (_formattedValue != value)
                {
                    _formattedValue = value;
                    RaisePropertyChanged(() => FormattedValue);
                }
            }
        }

        public bool ShowToolTip => !string.IsNullOrWhiteSpace(ToolTipText);
    }
}
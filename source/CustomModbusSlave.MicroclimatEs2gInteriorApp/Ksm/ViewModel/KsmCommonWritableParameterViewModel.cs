using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm.TextFormatters;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm.ViewModel {
	class KsmCommonWritableParameterViewModel : SettableParameterViewModel {
		//private readonly int _zbParameterNumber;
		//private readonly IUshortToDoubleConverter _toDoubleValueConverter;
		//private readonly ITextFormatter<ushort?> _currentValueFormatter;
		//private ushort? _receivedValue;

		public KsmCommonWritableParameterViewModel(int paramIndex, string name, double maxValue, double minValue, double? doubleValue, string stringFormat, IDoubleBytesPairConverter doubleBytesPairConverter, IParameterSetter parameterSetter, IThreadNotifier uiNotifier) : base(paramIndex, name, maxValue, minValue, doubleValue, stringFormat, doubleBytesPairConverter, parameterSetter, uiNotifier) { }

		/*
	public KsmCommonWritableParameterViewModel(int zbParameterNumber, string name, ITextFormatter<ushort?> currentValueFormatter) {
		_zbParameterNumber = zbParameterNumber;
		Name = name;
		//_toDoubleValueConverter = toDoubleValueConverter;
		_currentValueFormatter = currentValueFormatter;
	}*/



		/*public ushort? ReceivedValue {
			get {
				return _receivedValue;

			}

			private set {
				if (_receivedValue != value) {
					_receivedValue = value;
					RaisePropertyChanged(() => ReceivedValue);
					RaisePropertyChanged(() => ReceivedValueFormatted);
					//RaisePropertyChanged(() => ReceivedValueDouble);
				}
			}
		}*/

		//public double? ReceivedValueDouble => _toDoubleValueConverter.Convert(_receivedValue);

		public string ReceivedValueFormatted => ReceivedDoubleValue == null ? null : ReceivedDoubleValue.Value.ToString(_receivedValue);

		//public string Name { get; }

		//public void SetCurrentValue(ushort? currentValue) {
			//ReceivedValue = currentValue;
		//}
		
	}
}

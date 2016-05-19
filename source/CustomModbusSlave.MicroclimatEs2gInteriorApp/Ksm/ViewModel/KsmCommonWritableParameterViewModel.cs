using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm.TextFormatters;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm.ViewModel {
	class KsmCommonWritableParameterViewModel : ViewModelBase, IKsmParameterViewModel {
		private readonly int _zbParameterNumber;
		//private readonly IUshortToDoubleConverter _toDoubleValueConverter;
		private readonly ITextFormatter<ushort?> _currentValueFormatter;
		private ushort? _receivedValue;

		public KsmCommonWritableParameterViewModel(int zbParameterNumber, string name/*, IUshortToDoubleConverter toDoubleValueConverter*/, ITextFormatter<ushort?> currentValueFormatter) {
			_zbParameterNumber = zbParameterNumber;
			Name = name;
			//_toDoubleValueConverter = toDoubleValueConverter;
			_currentValueFormatter = currentValueFormatter;
		}



		public ushort? ReceivedValue {
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
		}

		//public double? ReceivedValueDouble => _toDoubleValueConverter.Convert(_receivedValue);

		public string ReceivedValueFormatted => _currentValueFormatter.Format(_receivedValue);

		public string Name { get; }

		public void SetCurrentValue(ushort? currentValue) {
			ReceivedValue = currentValue;
		}
	}
}

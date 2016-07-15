using AlienJust.Support.Collections;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.TextFormatters;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.ViewModel {
	public class KsmCommonWritableParameterViewModel : ViewModelBase, IReceivableParameter, IDisplayableParameter<string> {
		private readonly int _zbParameterNumber;
		private readonly ITextFormatter<BytesPair?> _currentValueFormatter;
		private BytesPair? _receivedValue;

		public KsmCommonWritableParameterViewModel(int zbParameterNumber, string name/*, IUshortToDoubleConverter toDoubleValueConverter*/, ITextFormatter<BytesPair?> currentValueFormatter) {
			_zbParameterNumber = zbParameterNumber;
			Name = name;
			_currentValueFormatter = currentValueFormatter;
		}



		public string ReceivedValueFormatted => _currentValueFormatter.Format(_receivedValue);

		public string Name { get; }


		public BytesPair? ReceivedBytesValue
		{
			get { return _receivedValue; }
			set {
				if (_receivedValue != value) {
					_receivedValue = value;
					RaisePropertyChanged(() => ReceivedBytesValue);
					RaisePropertyChanged(() => ReceivedValueFormatted);
				}
			}
		}
	}
}

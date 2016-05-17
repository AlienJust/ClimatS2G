using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlienJust.Support.ModelViewViewModel;
using NCalc;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class KsmCommonWritableParameterViewModel : ViewModelBase {
		private readonly int _zbParameterNumber;
		private readonly IUshortToDoubleConverter _toDoubleValueConverter;
		private readonly IDoubleTextFormatter _currentValueFormatter;
		private ushort? _receivedValue;

		public KsmCommonWritableParameterViewModel(int zbParameterNumber, string name, IUshortToDoubleConverter toDoubleValueConverter, IDoubleTextFormatter currentValueFormatter) {
			_zbParameterNumber = zbParameterNumber;
			Name = name;
			_toDoubleValueConverter = toDoubleValueConverter;
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
					RaisePropertyChanged(() => ReceivedValueDouble);
				}
			}
		}

		public double? ReceivedValueDouble => _toDoubleValueConverter.Convert(_receivedValue);

		public string ReceivedValueFormatted => _currentValueFormatter.Format(_receivedValue);

		public string Name { get; }

		public void SetCurrentValue(ushort? currentValue) {
			ReceivedValue = currentValue;
		}
	}
}

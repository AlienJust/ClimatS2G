using AlienJust.Support.Collections;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm.ViewModel {
	class KsmBitParameterViewModel : ViewModelBase, IKsmBitParameterViewModel {
		private readonly int _zbBitNumber;
		private bool? _receivedBitValue;

		public KsmBitParameterViewModel(int zbBitNumber, string name) {
			_zbBitNumber = zbBitNumber;
			Name = name;
			_receivedBitValue = null;
		}

		public string Name { get; }

		public bool? ReceivedValueFormatted {
			get { return _receivedBitValue; }
			private set
			{
				if (_receivedBitValue != value) {
					_receivedBitValue = value;
					RaisePropertyChanged(()=> ReceivedValueFormatted);
				}
			}
		}

		public void SetCurrentValue(BytesPair? value) {
			if (value == null) ReceivedValueFormatted = null;
			else ReceivedValueFormatted = ((value.Value.HighFirstUnsignedValue >> _zbBitNumber) & 0x01) == 0x01;
		}

		public BytesPair? ReceivedBytesValue
		{
			set
			{
				SetCurrentValue(value);
			}
		}
	}
}
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

		public bool? ReceivedBitValue
		{
			get { return _receivedBitValue; }
			private set
			{
				if (_receivedBitValue != value) {
					_receivedBitValue = value;
					RaisePropertyChanged(()=>ReceivedBitValue);
				}
			}
		}

		public void SetCurrentValue(ushort? value) {
			if (value == null) ReceivedBitValue = null;
			ReceivedBitValue = ((value >> _zbBitNumber) & 0x01) == 0x01;
		}
	}
}
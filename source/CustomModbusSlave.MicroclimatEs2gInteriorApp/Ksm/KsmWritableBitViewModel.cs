using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class KsmWritableBitViewModel : ViewModelBase {
		public string Name { get; set; }
		private readonly IParameterRoot _root;
		private readonly int _zbBitNumber;
		private bool _receivedBitValue;
		private bool _bitValueToSet;

		public KsmWritableBitViewModel(IParameterRoot root, int zbBitNumber, string name) {
			Name = name;
			_root = root;
			_zbBitNumber = zbBitNumber;
		}

		public bool ReceivedBitValue
		{
			get { return _receivedBitValue; }
			set // TODO: change setter to method to avoid view access ability
			{
				if (_receivedBitValue != value) {
					_receivedBitValue = value;
					RaisePropertyChanged(()=> ReceivedBitValue);
				}
			}
		}
		public bool BitValueToSet {
			get { return _bitValueToSet; }
			set {
				if (_bitValueToSet != value) {
					_bitValueToSet = value;
					RaisePropertyChanged(() => BitValueToSet);
					_root.NotifyBitValueChanged(_zbBitNumber, _bitValueToSet);
				}
			}
		}
	}
}

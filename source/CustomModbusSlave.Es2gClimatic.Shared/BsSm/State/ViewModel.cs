using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.BsSm.State {
	public class ViewModel : ViewModelBase, IBsSmState, IUpdateable<IBsSmState> {
		private IBsSmState _contract;

		public bool NoLinkMk1AndMspuD1 {
			get {
				if (_contract == null) throw new TelemetryIsNullException();
				return _contract.NoLinkMk1AndMspuD1;
			}
		}

		public bool NoLinkMk2AndMspuD2 {
			get {
				if (_contract == null) throw new TelemetryIsNullException();
				return _contract.NoLinkMk2AndMspuD2;
			}
		}

		public bool ReliabilityBitFromMspuD1 {
			get {
				if (_contract == null) throw new TelemetryIsNullException();
				return _contract.ReliabilityBitFromMspuD1;
			}
		}

		public bool ReliabilityBitFromMspuD2 {
			get {
				if (_contract == null) throw new TelemetryIsNullException();
				return _contract.ReliabilityBitFromMspuD2;
			}
		}

		public bool MspuD1ReceiptNumberFail {
			get {
				if (_contract == null) throw new TelemetryIsNullException();
				return _contract.MspuD1ReceiptNumberFail;
			}
		}

		public bool MspuD2ReceiptNumberFail {
			get {
				if (_contract == null) throw new TelemetryIsNullException();
				return _contract.MspuD2ReceiptNumberFail;
			}
		}

		public bool NoLinkMk1BySpi {
			get {
				if (_contract == null) throw new TelemetryIsNullException();
				return _contract.NoLinkMk1BySpi;
			}
		}

		public bool Reserved {
			get {
				if (_contract == null) throw new TelemetryIsNullException();
				return _contract.Reserved;
			}
		}

		public void Update(IBsSmState updatedValue) {
			_contract = updatedValue;
			RaisePropertyChanged(() => NoLinkMk1AndMspuD1);
			RaisePropertyChanged(() => NoLinkMk2AndMspuD2);
			RaisePropertyChanged(() => ReliabilityBitFromMspuD1);
			RaisePropertyChanged(() => ReliabilityBitFromMspuD2);
			RaisePropertyChanged(() => MspuD1ReceiptNumberFail);
			RaisePropertyChanged(() => MspuD2ReceiptNumberFail);
			RaisePropertyChanged(() => NoLinkMk1BySpi);
			RaisePropertyChanged(() => Reserved);
		}
	}
}

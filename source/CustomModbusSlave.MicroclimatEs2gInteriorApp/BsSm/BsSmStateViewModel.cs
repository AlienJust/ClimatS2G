using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	internal class BsSmStateViewModel : ViewModelBase, IBsSmState, IUpdateable<IBsSmState> {
		private IBsSmState _bsSmState;

		public bool NoLinkMk1AndMspuD1 {
			get {
				if (_bsSmState == null) throw new TelemetryIsNullException();
				return _bsSmState.NoLinkMk1AndMspuD1;
			}
		}

		public bool NoLinkMk2AndMspuD2 {
			get {
				if (_bsSmState == null) throw new TelemetryIsNullException();
				return _bsSmState.NoLinkMk2AndMspuD2;
			}
		}

		public bool ReliabilityBitFromMspuD1 {
			get {
				if (_bsSmState == null) throw new TelemetryIsNullException();
				return _bsSmState.ReliabilityBitFromMspuD1;
			}
		}

		public bool ReliabilityBitFromMspuD2 {
			get {
				if (_bsSmState == null) throw new TelemetryIsNullException();
				return _bsSmState.ReliabilityBitFromMspuD2;
			}
		}

		public bool MspuD1ReceiptNumberFail {
			get {
				if (_bsSmState == null) throw new TelemetryIsNullException();
				return _bsSmState.MspuD1ReceiptNumberFail;
			}
		}

		public bool MspuD2ReceiptNumberFail {
			get {
				if (_bsSmState == null) throw new TelemetryIsNullException();
				return _bsSmState.MspuD2ReceiptNumberFail;
			}
		}

		public bool NoLinkMk1BySpi {
			get {
				if (_bsSmState == null) throw new TelemetryIsNullException();
				return _bsSmState.NoLinkMk1BySpi;
			}
		}

		public bool Reserved {
			get {
				if (_bsSmState == null) throw new TelemetryIsNullException();
				return _bsSmState.Reserved;
			}
		}

		public void Update(IBsSmState updatedValue) {
			_bsSmState = updatedValue;
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
namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	class BsSmStateSimple : IBsSmState {
		private readonly bool _noLinkMk1AndMspuD1;
		private readonly bool _noLinkMk2AndMspuD2;
		private readonly bool _reliabilityBitFromMspuD1;
		private readonly bool _reliabilityBitFromMspuD2;
		private readonly bool _mspuD1ReceiptNumberFail;
		private readonly bool _mspuD2ReceiptNumberFail;
		private readonly bool _noLinkMk1BySpi;
		private readonly bool _reserved;
		

		public BsSmStateSimple(bool noLinkMk1AndMspuD1, bool noLinkMk2AndMspuD2, bool reliabilityBitFromMspuD1, bool reliabilityBitFromMspuD2, bool mspuD1ReceiptNumberFail, bool mspuD2ReceiptNumberFail, bool noLinkMk1BySpi, bool reserved) {
			_noLinkMk1AndMspuD1 = noLinkMk1AndMspuD1;
			_noLinkMk2AndMspuD2 = noLinkMk2AndMspuD2;
			_reliabilityBitFromMspuD1 = reliabilityBitFromMspuD1;
			_reliabilityBitFromMspuD2 = reliabilityBitFromMspuD2;
			_mspuD1ReceiptNumberFail = mspuD1ReceiptNumberFail;
			_mspuD2ReceiptNumberFail = mspuD2ReceiptNumberFail;
			_noLinkMk1BySpi = noLinkMk1BySpi;
			_reserved = reserved;
		}

		public bool NoLinkMk1AndMspuD1 {
			get { return _noLinkMk1AndMspuD1; }
		}

		public bool NoLinkMk2AndMspuD2 {
			get { return _noLinkMk2AndMspuD2; }
		}

		public bool ReliabilityBitFromMspuD1 {
			get { return _reliabilityBitFromMspuD1; }
		}

		public bool ReliabilityBitFromMspuD2 {
			get { return _reliabilityBitFromMspuD2; }
		}

		public bool MspuD1ReceiptNumberFail {
			get { return _mspuD1ReceiptNumberFail; }
		}

		public bool MspuD2ReceiptNumberFail {
			get { return _mspuD2ReceiptNumberFail; }
		}

		public bool Reserved {
			get { return _reserved; }
		}

		public bool NoLinkMk1BySpi {
			get { return _noLinkMk1BySpi; }
		}
	}
}
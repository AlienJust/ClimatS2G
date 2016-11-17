namespace CustomModbusSlave.Es2gClimatic.Shared.BsSm.State {
	public class Simple : IContract {
		public Simple(bool noLinkMk1AndMspuD1, bool noLinkMk2AndMspuD2, bool reliabilityBitFromMspuD1, bool reliabilityBitFromMspuD2, bool mspuD1ReceiptNumberFail, bool mspuD2ReceiptNumberFail, bool noLinkMk1BySpi, bool reserved) {
			NoLinkMk1AndMspuD1 = noLinkMk1AndMspuD1;
			NoLinkMk2AndMspuD2 = noLinkMk2AndMspuD2;
			ReliabilityBitFromMspuD1 = reliabilityBitFromMspuD1;
			ReliabilityBitFromMspuD2 = reliabilityBitFromMspuD2;
			MspuD1ReceiptNumberFail = mspuD1ReceiptNumberFail;
			MspuD2ReceiptNumberFail = mspuD2ReceiptNumberFail;
			NoLinkMk1BySpi = noLinkMk1BySpi;
			Reserved = reserved;
		}

		public bool NoLinkMk1AndMspuD1 { get; }

		public bool NoLinkMk2AndMspuD2 { get; }

		public bool ReliabilityBitFromMspuD1 { get; }

		public bool ReliabilityBitFromMspuD2 { get; }

		public bool MspuD1ReceiptNumberFail { get; }

		public bool MspuD2ReceiptNumberFail { get; }

		public bool NoLinkMk1BySpi { get; }

		public bool Reserved { get; }
	}
}
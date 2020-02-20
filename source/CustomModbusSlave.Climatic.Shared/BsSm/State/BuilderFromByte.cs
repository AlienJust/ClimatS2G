namespace CustomModbusSlave.Es2gClimatic.Shared.BsSm.State {
	public class BuilderFromByte : IBuilder<IBsSmState> {
		private readonly byte _b;
		public BuilderFromByte(byte b) {
			_b = b;
		}

		public IBsSmState Build() {
			bool noLinkMk1AndMspuD1 = (_b & 0x01) == 0x01;
			bool noLinkMk2AndMspuD2 = (_b & 0x02) == 0x02;
			bool reliabilityBitFromMspuD1 = (_b & 0x04) == 0x04;
			bool reliabilityBitFromMspuD2 = (_b & 0x08) == 0x08;
			bool mspuD1ReceiptNumberFail = (_b & 0x10) == 0x10;
			bool mspuD2ReceiptNumberFail = (_b & 0x20) == 0x20;
			bool noLinkNk1BySpi = (_b & 0x40) == 0x40;
			bool reserved = (_b & 0x80) == 0x80;

			return new Simple(
				noLinkMk1AndMspuD1,
				noLinkMk2AndMspuD2,
				reliabilityBitFromMspuD1,
				reliabilityBitFromMspuD2,
				mspuD1ReceiptNumberFail,
				mspuD2ReceiptNumberFail,
				noLinkNk1BySpi,
				reserved
				);
		}
	}
}

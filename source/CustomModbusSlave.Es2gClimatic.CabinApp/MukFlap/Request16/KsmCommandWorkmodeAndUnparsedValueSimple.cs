namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Request16 {
	internal class KsmCommandWorkmodeAndUnparsedValueSimple : IKsmCommandWorkmodeAndUnparsedValue {
		public KsmCommandWorkmodeAndUnparsedValueSimple(int rawValue) {
			RawValue = rawValue;
		}

		public KsmCommandWorkmode ParsedValue => new KsmCommandWorkmodeBuilder(RawValue).Build();

		public int RawValue { get; }
	}
}
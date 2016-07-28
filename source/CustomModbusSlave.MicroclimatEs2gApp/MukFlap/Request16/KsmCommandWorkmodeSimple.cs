namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Request16 {
	internal class KsmCommandWorkmodeSimple : IKsmCommandWorkmode {
		public KsmCommandWorkmodeSimple(int rawValue) {
			RawValue = rawValue;
		}

		public KsmCommandWorkmode ParsedValue => new KsmCommandWorkmodeBuilder(RawValue).Build();

		public int RawValue { get; }
	}
}
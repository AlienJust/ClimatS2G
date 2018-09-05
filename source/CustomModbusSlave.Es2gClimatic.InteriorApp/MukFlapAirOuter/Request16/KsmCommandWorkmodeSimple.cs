namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Request16 {
	internal class KsmCommandWorkmodeSimple : IKsmCommandWorkmode {
		public KsmCommandWorkmodeSimple(int rawValue) {
			RawValue = rawValue;
		}

		public KsmCommandWorkmode ParsedValue => new KsmCommandWorkmodeBuilder(RawValue).Build();

		public int RawValue { get; }
	}
}

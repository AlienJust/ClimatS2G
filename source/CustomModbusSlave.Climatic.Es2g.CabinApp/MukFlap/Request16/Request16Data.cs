namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Request16 {
	class Request16Data : IMukFlapAirRequest16Data {
		public Request16Data(IKsmCommandWorkmodeAndUnparsedValue currentKsmCommandWorkmodeAndUnparsedValue, int outerTemperature, double innerTemperature, int fanSpeedLevel, int bitRegistry, bool interiorOrCabin, int reserve22) {
			CurrentKsmCommandWorkmode = currentKsmCommandWorkmodeAndUnparsedValue;
			OuterTemperature = outerTemperature;
			InnerTemperature = innerTemperature;
			FanSpeedLevel = fanSpeedLevel;
			BitRegistry = bitRegistry;
			InteriorOrCabin = interiorOrCabin;
			Reserve22 = reserve22;
		}

		public IKsmCommandWorkmodeAndUnparsedValue CurrentKsmCommandWorkmode { get; }
		public int OuterTemperature { get; }
		public double InnerTemperature { get; }
		public int FanSpeedLevel { get; }
		public int BitRegistry { get; }
		public bool InteriorOrCabin { get; }
		public int Reserve22 { get; }
	}
}

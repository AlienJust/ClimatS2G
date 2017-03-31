namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Request16 {
	interface IRequest16Data {
		IKsmCommandWorkmodeAndUnparsedValue CurrentKsmCommandWorkmode { get; }
		int OuterTemperature { get; }
		double InnerTemperature { get; }
		int FanSpeedLevel { get; }
		int BitRegistry { get; }
		bool InteriorOrCabin { get; }
		int Reserve22 { get; }
	}
}

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Request16 {
	class Request16Data : IMukFlapOuterAirRequest16Data {
		public Request16Data(IKsmCommandWorkmode currentKsmCommandWorkmode, int outerTemperature, int targetTemperature, int fanSpeed, bool isInteriorIfTrueCabinIfFalse, int reserve26) {
			CurrentKsmCommandWorkmode = currentKsmCommandWorkmode;
			OuterTemperature = outerTemperature;
			TargetTemperature = targetTemperature;
			FanSpeed = fanSpeed;
			IsInteriorIfTrueCabinIfFalse = isInteriorIfTrueCabinIfFalse;
			Reserve26 = reserve26;
		}

		public IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; }
		public int OuterTemperature { get; }
		public int TargetTemperature { get; }
		public int FanSpeed { get; }
		public bool IsInteriorIfTrueCabinIfFalse { get; }
		public int Reserve26 { get; }
	}
}
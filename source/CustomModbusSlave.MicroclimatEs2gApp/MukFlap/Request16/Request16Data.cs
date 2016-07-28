namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Request16 {
	class Request16Data : IRequest16Data {
		public Request16Data(IKsmCommandWorkmode currentKsmCommandWorkmode, int outerTemperature, int targetTemperature, int fanSpeed, int reserve21, int reserve22) {
			CurrentKsmCommandWorkmode = currentKsmCommandWorkmode;
			OuterTemperature = outerTemperature;
			TargetTemperature = targetTemperature;
			FanSpeed = fanSpeed;
			Reserve21 = reserve21;
			Reserve22 = reserve22;
		}

		public IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; }
		public int OuterTemperature { get; }
		public int TargetTemperature { get; }
		public int FanSpeed { get; }
		public int Reserve21 { get; }
		public int Reserve22 { get; }
	}
}
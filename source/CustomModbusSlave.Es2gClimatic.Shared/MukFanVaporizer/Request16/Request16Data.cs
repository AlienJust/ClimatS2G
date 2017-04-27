namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Request16 {
	class Request16Data : IMukFanVaporizerRequest16Data {
		public IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; set; }
		public int OuterTemperature { get; set; }
		public double InnerTemperature { get; set; }
		public int FanSpeed { get; set; }
		public int DeltaT { get; set; }
		public double DeltaTSetting { get; set; }

		public bool IsSlave { get; set; }
	}
}
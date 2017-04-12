namespace CustomModbusSlave.Es2gClimatic.Shared.MukVaporizer.Request16 {
	class Request16Data : IMukVaporizerRequest16InteriorData {
		public IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; set; }
		public int OuterTemperature { get; set; }
		public double InnerTemperature { get; set; }
		public int FanSpeed { get; set; }
		public int DeltaT { get; set; }
		public int Reserve23 { get; set; }
		public bool IsSlave { get; set; }
	}
}
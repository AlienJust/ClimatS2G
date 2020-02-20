namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Request16 {
	class MukFlapAirRecycleRequest16DataSimple : IMukFlapAirRecycleRequest16Data {
		public int MukFlapAirRecycleWorkmodeCommandFromKsm { get; set; }
		public int TemperatureOuterAir { get; set; }
		public double TemperatureInnerAir { get; set; }
		public int FanLevelSetting { get; set; }
		public int ControlOfHiVoltageContactorRaw { get; set; }
		public IControlOfHiVoltageContactor ControlOfHiVoltageContactorDescription { get;  set; }
		public int Reserve624 { get; set; }
	}
}

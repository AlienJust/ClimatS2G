namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirWinterSummer.Request16 {
	class MukFlapAirWinterSummerRequest16DataSimple : IMukFlapAirWinterSummerRequest16Data {
		public int MukFlapAirWinterSummerWorkmodeCommandFromKsm { get; set; }
		public int TemperatureOuterAir { get; set; }
		public double TemperatureInnerAir { get; set; }
		public int FanLevelSetting { get; set; }
		public int Reserve725 { get; set; }
		public int Reserve726 { get; set; }
	}
}
namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Request16 {
	class MukFanAirExhausterRequest16DataSimple : IMukFanAirExhausterRequest16Data {
		public IMukFanAirExhausterWorkmodeCommandFromKsm MukFanAirExhausterWorkmodeCommandFromKsm { get; set; }
		public int TemperatureOuterAir { get; set; }
		public IAttributesRegistryByte AttributesRegistryByte { get; set; }
		public int FanLevelSetting { get; set; }
		public int Reserve517 { get; set; }
		public int Reserve518 { get; set; }
	}
}

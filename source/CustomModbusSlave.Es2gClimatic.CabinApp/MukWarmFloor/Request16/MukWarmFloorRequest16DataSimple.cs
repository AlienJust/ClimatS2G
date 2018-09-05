namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Request16 {
	class MukWarmFloorRequest16DataSimple : IMukWarmFloorRequest16Data {
		public int WorkModeReceivedFromKsm { get; set; }
		public int TemperatureOutside { get; set; }
		public int TemperatureInside { get; set; }
	}
}

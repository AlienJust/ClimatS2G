namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Request16 {
	interface IMukWarmFloorRequest16Data {
		int WorkModeReceivedFromKsm { get; }
		int TemperatureOutside { get; }
		int TemperatureInside { get; }
	}
}

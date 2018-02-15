namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor
{
	interface IMukWarmFloorRequest16
	{
		int WorkModeReceivedFromKsm { get; }
		int TemperatureOutside { get; }
		int TemperatureInside { get; }
	}
}
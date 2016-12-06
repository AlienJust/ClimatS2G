namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor
{
	interface IRequest16
	{
		int WorkModeReceivedFromKsm { get; }
		int TemperatureOutside { get; }
		int TemperatureInside { get; }
	}
}
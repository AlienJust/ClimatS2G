namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor
{
	class MukWarmFloorRequest16Simple : IMukWarmFloorRequest16
	{
		public int WorkModeReceivedFromKsm { get; set; }
		public int TemperatureOutside { get; set; }
		public int TemperatureInside { get; set; }
	}
}
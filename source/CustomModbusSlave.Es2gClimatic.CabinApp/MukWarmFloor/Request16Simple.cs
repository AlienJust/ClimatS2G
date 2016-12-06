namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor
{
	class Request16Simple : IRequest16
	{
		public int WorkModeReceivedFromKsm { get; set; }
		public int TemperatureOutside { get; set; }
		public int TemperatureInside { get; set; }
	}
}
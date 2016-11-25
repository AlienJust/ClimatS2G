using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.SimpleRelease {
	class BsSmAndKsm1DataCommand32ReplySimple : IBsSmAndKsm1DataCommand32Reply {
		public BsSmAndKsm1DataCommand32ReplySimple(uint astronomicTime, uint delayedStartTime, int temperatureOutdoor, int carType, int reserve13D4D7, int targetTemperatureInterior, int climaticSystemWorkmode14D4D7, Shared.BsSm.IWorkMode workModeAndCompressorSwitch, int allowedPowerConsuptionBy380Vline, int reserve17, int reserve18, IBsSmAndKsm1DataCommand32Request ksm2Request, Shared.BsSm.State.IContract contract, int bsSmVersionNumber, int reserve43, int reserve44, int reserve45) {
			AstronomicTime = astronomicTime;
			DelayedStartTime = delayedStartTime;
			TemperatureOutdoor = temperatureOutdoor;
			CarType = carType;
			Reserve13D4D7 = reserve13D4D7;
			TargetTemperatureInterior = targetTemperatureInterior;
			ClimaticSystemWorkmode14D4D7 = climaticSystemWorkmode14D4D7;
			WorkModeAndCompressorSwitch = workModeAndCompressorSwitch;
			AllowedPowerConsuptionBy380Vline = allowedPowerConsuptionBy380Vline;
			Reserve17 = reserve17;
			Reserve18 = reserve18;
			//Segment2CurentCalculatedPowerConsumptionBy380Vline = segment2CurentCalculatedPowerConsumptionBy380Vline;
			//Segment2InteriorTemperature = segment2InteriorTemperature;
			Ksm2Request = ksm2Request;
			Contract = contract;
			BsSmVersionNumber = bsSmVersionNumber;
			Reserve43 = reserve43;
			Reserve44 = reserve44;
			Reserve45 = reserve45;
		}

		public uint AstronomicTime { get; }
		public uint DelayedStartTime { get; }
		public int TemperatureOutdoor { get; }
		public int CarType { get; }
		public int Reserve13D4D7 { get; }
		public int TargetTemperatureInterior { get; }
		public int ClimaticSystemWorkmode14D4D7 { get; }
		public Shared.BsSm.IWorkMode WorkModeAndCompressorSwitch { get; }
		public int AllowedPowerConsuptionBy380Vline { get; }
		public int Reserve17 { get; }
		public int Reserve18 { get; }
		//public int Segment2CurentCalculatedPowerConsumptionBy380Vline { get; }
		//public int Segment2InteriorTemperature { get; }
		public IBsSmAndKsm1DataCommand32Request Ksm2Request { get; }
		public Shared.BsSm.State.IContract Contract { get; }
		public int BsSmVersionNumber { get; }
		public int Reserve43 { get; }
		public int Reserve44 { get; }
		public int Reserve45 { get; }
	}
}
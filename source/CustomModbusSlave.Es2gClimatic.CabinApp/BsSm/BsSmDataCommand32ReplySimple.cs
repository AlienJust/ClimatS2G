using CustomModbusSlave.Es2gClimatic.Shared.BsSm;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm {
	class BsSmDataCommand32ReplySimple : IBsSmDataCommand32Reply {
		public BsSmDataCommand32ReplySimple(int targetTemperatureInsideTheCabin, int fanSpeedLevel, bool isWarmFloorOn, uint astronomicTime, uint delayedStartTime, int temperatureOutdoor, int temperatureIndoor, ClimaticSystemWorkMode climaticWorkmode, IWorkMode workMode, Shared.BsSm.State.IContract contract, int bsSmVersionNumber) {
			TargetTemperatureInsideTheCabin = targetTemperatureInsideTheCabin;
			FanSpeedLevel = fanSpeedLevel;
			IsWarmFloorOn = isWarmFloorOn;
			AstronomicTime = astronomicTime;
			DelayedStartTime = delayedStartTime;
			TemperatureOutdoor = temperatureOutdoor;
			TemperatureIndoor = temperatureIndoor;
			ClimaticWorkmode = climaticWorkmode;
			WorkMode = workMode;
			BsSmState = contract;
			BsSmVersionNumber = bsSmVersionNumber;
		}

		public int TargetTemperatureInsideTheCabin { get; }

		public int FanSpeedLevel { get; }

		public bool IsWarmFloorOn { get; }

		public uint AstronomicTime { get; }

		public uint DelayedStartTime { get; }

		public int TemperatureOutdoor { get; }

		public Shared.BsSm.State.IContract BsSmState { get; }

		public int BsSmVersionNumber { get; }
		public int TemperatureIndoor { get; }
		public ClimaticSystemWorkMode ClimaticWorkmode { get; }
		public IWorkMode WorkMode { get; }
	}
}
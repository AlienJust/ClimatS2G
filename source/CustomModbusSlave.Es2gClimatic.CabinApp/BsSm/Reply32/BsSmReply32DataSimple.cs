namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Reply32 {
	class BsSmReply32DataSimple : IBsSmReply32Data {
		public BsSmReply32DataSimple(int targetTemperatureInsideTheCabin, int fanSpeedLevel, bool isWarmFloorOn, uint astronomicTime, uint delayedStartTime, int temperatureOutdoor, int temperatureIndoor, Shared.BsSm.ClimaticSystemWorkMode climaticWorkmode, Shared.BsSm.IWorkMode workMode, Shared.BsSm.State.IContract contract, int bsSmVersionNumber) {
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
		public Shared.BsSm.ClimaticSystemWorkMode ClimaticWorkmode { get; }
		public Shared.BsSm.IWorkMode WorkMode { get; }
	}
}
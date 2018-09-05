using System;
using CustomModbusSlave.Es2gClimatic.Shared.BsSm;
using CustomModbusSlave.Es2gClimatic.Shared.BsSm.State;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Reply32 {
	class BsSmReply32DataSimple : IBsSmReply32Data {
		public BsSmReply32DataSimple(int targetTemperatureInsideTheCabin, int fanSpeedLevel, bool isWarmFloorOn, DateTime astronomicTime, uint delayedStartTime, int temperatureOutdoor, int temperatureIndoor, Shared.BsSm.ClimaticSystemWorkMode climaticWorkmode, Shared.BsSm.IBsSmWorkMode workMode, Shared.BsSm.State.IBsSmState contract, int bsSmVersionNumber) {
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

		public DateTime AstronomicTime { get; }

		public uint DelayedStartTime { get; }

		public int TemperatureOutdoor { get; }

		public IBsSmState BsSmState { get; }

		public int BsSmVersionNumber { get; }
		public int TemperatureIndoor { get; }
		public ClimaticSystemWorkMode ClimaticWorkmode { get; }
		public IBsSmWorkMode WorkMode { get; }
	}
}

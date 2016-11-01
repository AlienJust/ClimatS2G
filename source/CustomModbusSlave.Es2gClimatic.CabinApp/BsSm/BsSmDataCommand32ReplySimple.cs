using CustomModbusSlave.Es2gClimatic.BsSm.State;

namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	class BsSmDataCommand32ReplySimple : IBsSmDataCommand32Reply {
		public BsSmDataCommand32ReplySimple(int targetTemperatureInsideTheCabin, int fanSpeedLevel, bool isWarmFloorOn, uint astronomicTime, uint delayedStartTime, int temperatureOutdoor, IBsSmState bsSmState, int bsSmVersionNumber) {
			TargetTemperatureInsideTheCabin = targetTemperatureInsideTheCabin;
			FanSpeedLevel = fanSpeedLevel;
			IsWarmFloorOn = isWarmFloorOn;
			AstronomicTime = astronomicTime;
			DelayedStartTime = delayedStartTime;
			TemperatureOutdoor = temperatureOutdoor;
			BsSmState = bsSmState;
			BsSmVersionNumber = bsSmVersionNumber;
		}

		public int TargetTemperatureInsideTheCabin { get; }

		public int FanSpeedLevel { get; }

		public bool IsWarmFloorOn { get; }

		public uint AstronomicTime { get; }

		public uint DelayedStartTime { get; }

		public int TemperatureOutdoor { get; }

		public IBsSmState BsSmState { get; }

		public int BsSmVersionNumber { get; }
	}
}
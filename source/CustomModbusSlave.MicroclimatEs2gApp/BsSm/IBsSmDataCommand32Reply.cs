using CustomModbusSlave.MicroclimatEs2gApp.Common.BsSm.BsSmState;

namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	internal interface IBsSmDataCommand32Reply {
		int TargetTemperatureInsideTheCabin { get; }
		int FanSpeedLevel { get; }
		bool IsWarmFloorOn { get; }
		uint AstronomicTime { get; }
		uint DelayedStartTime { get; }

		int TemperatureOutdoor { get; }
		// TODO: byte 14 and 15
		IBsSmState BsSmState { get; }
		int BsSmVersionNumber { get; }
	}
}
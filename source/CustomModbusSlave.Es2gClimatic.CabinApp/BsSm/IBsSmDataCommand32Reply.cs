using CustomModbusSlave.Es2gClimatic.Shared.BsSm.State;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm {
	internal interface IBsSmDataCommand32Reply {
		int TargetTemperatureInsideTheCabin { get; }
		int FanSpeedLevel { get; }
		bool IsWarmFloorOn { get; }
		uint AstronomicTime { get; }
		uint DelayedStartTime { get; }

		int TemperatureOutdoor { get; }
		// TODO: byte 14 and 15
		IContract Contract { get; }
		int BsSmVersionNumber { get; }
	}
}
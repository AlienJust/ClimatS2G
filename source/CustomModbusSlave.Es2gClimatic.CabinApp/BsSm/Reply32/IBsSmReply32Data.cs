using System;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Reply32 {
	internal interface IBsSmReply32Data {
		// Air conditioner work mode (byte 4):
		int TargetTemperatureInsideTheCabin { get; }
		int FanSpeedLevel { get; }
		bool IsWarmFloorOn { get; }

		DateTime AstronomicTime { get; }
		uint DelayedStartTime { get; }

		int TemperatureOutdoor { get; }

		int TemperatureIndoor { get; }

		//14 byte (as 16 in saloon)
		Shared.BsSm.ClimaticSystemWorkMode ClimaticWorkmode { get; }
		//15 byte
		Shared.BsSm.IBsSmWorkMode WorkMode { get; }

		Shared.BsSm.State.IBsSmState BsSmState { get; }
		int BsSmVersionNumber { get; }
	}
}

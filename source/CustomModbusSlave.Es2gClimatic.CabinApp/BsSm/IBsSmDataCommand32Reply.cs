namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm {
	internal interface IBsSmDataCommand32Reply {
		int TargetTemperatureInsideTheCabin { get; }
		int FanSpeedLevel { get; }
		bool IsWarmFloorOn { get; }
		uint AstronomicTime { get; }
		uint DelayedStartTime { get; }

		int TemperatureOutdoor { get; }
		// TODO: byte 14 and 15
		Shared.BsSm.State.IContract BsSmState { get; }
		int BsSmVersionNumber { get; }
	}
}
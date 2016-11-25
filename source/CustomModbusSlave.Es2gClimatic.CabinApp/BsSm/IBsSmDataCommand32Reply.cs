namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm {
	internal interface IBsSmDataCommand32Reply {
		int TargetTemperatureInsideTheCabin { get; }
		int FanSpeedLevel { get; }
		bool IsWarmFloorOn { get; }
		uint AstronomicTime { get; }
		uint DelayedStartTime { get; }

		int TemperatureOutdoor { get; }

		int TemperatureIndoor { get; }
		Shared.BsSm.ClimaticSystemWorkMode ClimaticWorkmode { get; }
		Shared.BsSm.IWorkMode WorkMode { get; }

		Shared.BsSm.State.IContract BsSmState { get; }
		int BsSmVersionNumber { get; }
	}
}
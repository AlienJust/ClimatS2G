using CustomModbusSlave.Es2gClimatic.Shared.BsSm;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm {
	interface IBsSmDataCommand32Request {
		int TemperatureInsideTheCabin { get; }
		int TemperatureOutdoor { get; }
		int FanSpeed { get; }
		bool IsTunelModeOn { get; }
		bool IsWarmFloorOn { get; }
		ClimaticSystemWorkMode CurrentClimaticWorkMode { get; }
		IWorkMode WorkMode { get; }
		int Fault1 { get; }
		int Fault2 { get; }
		int Fault3 { get; }
		int Fault4 { get; }
		int Fault5 { get; }
	}
}

using CustomModbusSlave.Es2gClimatic.BsSm;
using CustomModbusSlave.MicroclimatEs2gApp.Common.BsSm;

namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	interface IBsSmDataCommand32Request {
		int TemperatureInsideTheCabin { get; }
		int TemperatureOutdoor { get; }
		int FanSpeed { get; }
		bool IsTunelModeOn { get; }
		bool IsWarmFloorOn { get; }
		ClimaticSystemWorkMode CurrentClimaticWorkMode { get; }
		int Fault1 { get; }
		int Fault2 { get; }
		int Fault3 { get; }
		int Fault4 { get; }
		int Fault5 { get; }
	}
}

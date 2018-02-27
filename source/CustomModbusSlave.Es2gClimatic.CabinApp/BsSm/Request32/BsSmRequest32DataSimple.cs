using CustomModbusSlave.Es2gClimatic.Shared.BsSm;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Request32 {
	class BsSmRequest32DataSimple : IBsSmRequest32Data {
		public BsSmRequest32DataSimple(int temperatureInsideTheCabin, int temperatureOutdoor, int fanSpeed, bool isTunelModeOn, bool isWarmFloorOn, ClimaticSystemWorkMode currentClimaticWorkMode, int fault1, int fault2, int fault3, int fault4, int fault5) {
			TemperatureInsideTheCabin = temperatureInsideTheCabin;
			TemperatureOutdoor = temperatureOutdoor;
			FanSpeed = fanSpeed;
			IsTunelModeOn = isTunelModeOn;
			IsWarmFloorOn = isWarmFloorOn;
			CurrentClimaticWorkMode = currentClimaticWorkMode;

			Fault1 = fault1;
			Fault2 = fault2;
			Fault3 = fault3;
			Fault4 = fault4;
			Fault5 = fault5;
			

			// TODO:  see http://stackoverflow.com/questions/1681406/wpf-databinding-watch-for-thrown-exceptions
		}

		public int TemperatureInsideTheCabin { get; }

		public int TemperatureOutdoor { get; }

		public int FanSpeed { get; }

		public bool IsTunelModeOn { get; }

		public bool IsWarmFloorOn { get; }

		public ClimaticSystemWorkMode CurrentClimaticWorkMode { get; }

		public int Fault1 { get; }

		public int Fault2 { get; }

		public int Fault3 { get; }

		public int Fault4 { get; }

		public int Fault5 { get; }
		
	}
}
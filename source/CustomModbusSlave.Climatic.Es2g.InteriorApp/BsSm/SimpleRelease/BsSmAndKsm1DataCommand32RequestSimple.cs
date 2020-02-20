using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.SimpleRelease {
	class BsSmAndKsm1DataCommand32RequestSimple : IBsSmAndKsm1DataCommand32Request {
		public BsSmAndKsm1DataCommand32RequestSimple(int maximumPower, int temperatureInterior, int temperatureOutdoor, int temperatureDelta, IWorkModeInRequest currentClimaticWorkMode, IWorkModeInRequest2 currentClimaticWorkMode2, int powerValueStage1, int powerValueStage2, int powerValueStage3, int fault1, int fault2, int fault3, int fault4, int fault5, int reserveOrTemperatureAverageValue, double co2LevelInCurrentSegment, int reserve25, bool channelTwoEmersonCompressorPressureIsLow) {
			MaximumPower = maximumPower;
			TemperatureInterior = temperatureInterior;
			TemperatureOutdoor = temperatureOutdoor;
			TemperatureDelta = temperatureDelta;
			CurrentClimaticWorkMode = currentClimaticWorkMode;
			CurrentClimaticWorkMode2 = currentClimaticWorkMode2;
			PowerValueStage1 = powerValueStage1;
			PowerValueStage2 = powerValueStage2;
			PowerValueStage3 = powerValueStage3;
			Fault1 = fault1;
			Fault2 = fault2;
			Fault3 = fault3;
			Fault4 = fault4;
			Fault5 = fault5;
			ReserveOrTemperatureAverageValue = reserveOrTemperatureAverageValue;
			Co2LevelInCurrentSegment = co2LevelInCurrentSegment;
			Reserve25 = reserve25;
			ChannelTwoEmersonCompressorPressureIsLow = channelTwoEmersonCompressorPressureIsLow;
		}

		public int MaximumPower { get; }
		public int TemperatureInterior { get; }
		public int TemperatureOutdoor { get; }
		public int TemperatureDelta { get; }
		public IWorkModeInRequest CurrentClimaticWorkMode { get; }
		public IWorkModeInRequest2 CurrentClimaticWorkMode2 { get; }
		public int PowerValueStage1 { get; }
		public int PowerValueStage2 { get; }
		public int PowerValueStage3 { get; }
		public int Fault1 { get; }
		public int Fault2 { get; }
		public int Fault3 { get; }
		public int Fault4 { get; }
		public int Fault5 { get; }
		public int ReserveOrTemperatureAverageValue { get; }
		public double Co2LevelInCurrentSegment { get; }
		public int Reserve25 { get; }
		public bool ChannelTwoEmersonCompressorPressureIsLow { get; }
	}
}

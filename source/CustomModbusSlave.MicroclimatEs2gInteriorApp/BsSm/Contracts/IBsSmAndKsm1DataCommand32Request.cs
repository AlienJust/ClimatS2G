namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm.Contracts {
	interface IBsSmAndKsm1DataCommand32Request {
		int MaximumPower { get; }
		int TemperatureInterior { get; }
		int TemperatureOutdoor { get; }
		int TemperatureDelta { get; }

		IWorkMode CurrentClimaticWorkMode { get; }
		IWorkMode2 CurrentClimaticWorkMode2 { get; }

		int PowerValueStage1 { get; }
		int PowerValueStage2 { get; }
		int PowerValueStage3 { get; }
		int Fault1 { get; }
		int Fault2 { get; }
		int Fault3 { get; }
		int Fault4 { get; }
		int Fault5 { get; }

		int ReserveOrTemperatureAverageValue { get; }
		int Co2LevelInCurrentSegment { get; }
		int Reserve25 { get; }
	}
}

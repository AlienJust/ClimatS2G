namespace CustomModbusSlave.MicroclimatEs2gApp.MukVaporizer.Request16 {
	interface IRequest16Data {
		IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; }
		int OuterTemperature { get; }
		double InnerTemperature { get; }
		int FanSpeed { get; }
		int DeltaT { get; }

		int DeltaTSetting { get; }
	}
}

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Request16 {
	interface IRequest16Data {
		IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; }
		int OuterTemperature { get; }
		int TargetTemperature { get; }
		int FanSpeed { get; }
		int Reserve21 { get; }
		int Reserve22 { get; }
	}
}

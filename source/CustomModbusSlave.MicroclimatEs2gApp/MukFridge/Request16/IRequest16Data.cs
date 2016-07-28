namespace CustomModbusSlave.MicroclimatEs2gApp.MukFridge.Request16 {
	interface IRequest16Data {
		IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; }
		int PressureSetting { get; }
		int Reserve14 { get; }
	}
}

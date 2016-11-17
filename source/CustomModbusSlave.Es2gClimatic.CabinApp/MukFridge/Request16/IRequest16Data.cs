namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFridge.Request16 {
	interface IRequest16Data {
		IKsmCommandWorkmode CurrentKsmCommandWorkmode { get; }
		int PressureSetting { get; }
		int Reserve14 { get; }
	}
}

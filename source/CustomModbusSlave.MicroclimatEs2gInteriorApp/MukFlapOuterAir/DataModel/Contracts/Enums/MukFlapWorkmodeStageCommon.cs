namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts.Enums {
	public enum MukFlapWorkmodeStage {
		ControllerInitialization, // 0
		FlapTesting, // 1
		WorkMode, // 2
		WorkModeWithFailedFlap, // 3
		WorkModePwmCorrectionBack, // 4
		NoKsm // 5
		,
		Unknown // any other value
	}
}
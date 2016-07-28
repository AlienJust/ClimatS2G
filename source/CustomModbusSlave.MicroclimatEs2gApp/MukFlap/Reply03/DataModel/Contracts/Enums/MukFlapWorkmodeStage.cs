namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Reply03.DataModel.Contracts.Enums {
	enum MukFlapWorkmodeStage {
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
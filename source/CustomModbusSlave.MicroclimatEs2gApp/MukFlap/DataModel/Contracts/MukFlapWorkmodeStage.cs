namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap {
	enum MukFlapWorkmodeStage {
		ControllerInitialization, // 0
		FlapTesting, // 1
		WorkMode, // 2
		WorkModeWithFailedFlap, // 3
		WorkModePwmCorrectionBack, // 4
		NoKsm // 5
	}
}
namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts.Enums {
	enum MukFlapOuterAirWorkmodeStage {
		ControllerInitialization, // 0
		FlapTesting, // 1
		WorkMode, // 2
		WorkModeWithFailedFlap, // 3
		WorkModePwmCorrectionBack, // 4
		NoKsm, // 5
		SetFromRemoteController, // 6
		FlapCloseInWashingMode, // 7
		Unknown // any other value
	}
}
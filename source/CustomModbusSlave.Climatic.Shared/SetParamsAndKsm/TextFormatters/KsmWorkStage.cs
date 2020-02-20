namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters {
	public enum KsmWorkStage {
		TurnOff0FormingTurnOffCommandMuk, // 0
		TurnOff1Time5SecondsOut, // 1
		TurnOff2,
		TurnOff3,
		TurnOff4,

		TurnOn5FormingTurnOnCommandMuk, // 5
		TurnOn6Time100SecondsOutAndMukTestsControl, // 6
		TurnOn7RegulatorOnAtEvaporatorGoingToCoolOrHeatMode, // 7
		TurnOn8CoolModeEmersionWorkControlAndSelfRegulator, // 8
		TurnOn9,

		Reserve10,
		Reserve11,
		Reserve12,
		Reserve13,
		Reserve14,

		Settling15,
		Settling16,
		Settling17,
		Settling18,
		Settling19,

		Service20FormingTurnOnCommandMuk, // 20
		Service21Time100SecondsAndMukTestsControl, // 21
		Service22RegulatorOnAtEvaporatorGoingToCoolOrHeatMode, // 22
		Service23CoolModeEmersionWorkControlAndSelfRegulator, // 23
		Service24,

		Washing25FormingTurnOffCommandMuk, //25
		Washing26Time100SecondsOutAndContolFlapCloseAlsoBvsSignalsControl, // 26
		Washing27,
		Washing28,
		Washing29,

		ManualMode30FormingTurnOnCommandMuk, // 31
		ManualMode31Time100SecondsOutAndMukTestsControl, // 32
		ManualMode32RegulatorOnAtEvaporatorGoingToCoolOrHeatMode, // 32
		ManualMode33CoolModeEmersionWorkControlAndSelfRegulator, // 33
		ManualMode34,

		Unknown
	}
}
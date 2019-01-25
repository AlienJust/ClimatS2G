namespace CustomModbusSlave.Es2gClimatic.Shared.Ksm.WamOrCoolForcedModes {
	public enum WarmOrCoolForcedMode : int {
		AutomaticMode = 0,
		Cool100Percent = 1,
		Cool50Percent = 2,
		Ventilation = 3,
		Warm100Percent = 4,
		Warm50Percent = 5,
		TurnOnUov = 6, // TODO: what is "UOV" means?
		Test3000VoltageHeating = 7,
		Test380VoltageHeating = 8,
		Unknown
	}
}

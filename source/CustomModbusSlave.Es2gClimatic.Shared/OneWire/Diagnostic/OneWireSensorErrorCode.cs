namespace CustomModbusSlave.Es2gClimatic.Shared.MukFlap.DiagnosticOneWire {
	public enum OneWireSensorErrorCode {
		None,
		FoundDeviceWithUnknownFamilyCode,
		SensorNotFound,
		NoReactionOnReset,
		SensorShowsIncorrectWorking,
		NoError
	}
}

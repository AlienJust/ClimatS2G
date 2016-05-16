namespace CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DiagnosticOneWire {
	public enum OneWireSensorErrorCode {
		None,
		FoundDeviceWithUnknownFamilyCode,
		SensorNotFound,
		NoReactionOnReset,
		SensorShowsIncorrectWorking,
		NoError
	}
}
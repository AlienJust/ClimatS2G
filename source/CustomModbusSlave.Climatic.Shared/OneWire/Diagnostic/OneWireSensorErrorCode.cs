namespace CustomModbusSlave.Es2gClimatic.Shared.OneWire.Diagnostic {
	public enum OneWireSensorErrorCode {
		None,
		FoundDeviceWithUnknownFamilyCode,
		SensorNotFound,
		NoReactionOnReset,
		SensorShowsIncorrectWorking,
		NoError
	}
}

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap {
	internal enum OneWireSensorErrorCode {
		None,
		FoundDeviceWithUnknownFamilyCode,
		SensorNotFound,
		NoReactionOnReset,
		SensorShowsIncorrectWorking
	}
}
namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts.Enums {
	internal enum OneWireSensorErrorCode {
		None,
		FoundDeviceWithUnknownFamilyCode,
		SensorNotFound,
		NoReactionOnReset,
		SensorShowsIncorrectWorking,
		NoError
	}
}
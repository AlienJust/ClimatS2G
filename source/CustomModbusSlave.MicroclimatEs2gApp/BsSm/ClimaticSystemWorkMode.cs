namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	internal enum ClimaticSystemWorkMode {
		Off,
		On,
		Reserved,
		DowntimeWhileOn,
		Maintenance,
		Washing,
		EmergencyVenting,
		EmergencyHeating,
		Test
	}
}
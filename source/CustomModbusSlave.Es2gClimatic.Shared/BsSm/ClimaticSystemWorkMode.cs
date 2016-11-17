namespace CustomModbusSlave.Es2gClimatic.Shared.BsSm {
	public enum ClimaticSystemWorkMode {
		Off,
		On,
		Reserved,
		DowntimeWhileOn,
		Maintenance,
		Washing,
		EmergencyVenting,
		EmergencyHeating,
		Test,
		Unknown
	}
}
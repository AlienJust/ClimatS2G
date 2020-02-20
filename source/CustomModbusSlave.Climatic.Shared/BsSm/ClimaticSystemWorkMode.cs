namespace CustomModbusSlave.Es2gClimatic.Shared.BsSm {
	/// <summary>
	/// Режим работы климатической системы
	/// </summary>
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

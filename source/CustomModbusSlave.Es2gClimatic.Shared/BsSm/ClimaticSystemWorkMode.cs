namespace CustomModbusSlave.Es2gClimatic.Shared.BsSm {
	/// <summary>
	/// ����� ������ ������������� �������
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
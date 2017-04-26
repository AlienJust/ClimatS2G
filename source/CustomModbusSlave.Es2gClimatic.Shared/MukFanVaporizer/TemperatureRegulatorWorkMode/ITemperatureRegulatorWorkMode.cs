namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.TemperatureRegulatorWorkMode
{
	/// <summary>
	/// ����� ������ ���������� �����������
	/// </summary>
	public interface ITemperatureRegulatorWorkMode {
		/// <summary>
		/// ������ �������� (���� ����)
		/// </summary>
		int FullValue { get; }
		/// <summary>
		/// ����� �����������, ����� ����� �������
		/// </summary>
		bool Cool { get; }
		/// <summary>
		/// ����� ����������� ����������/������� �� ������� �����������
		/// </summary>
		bool Restrict { get; }
	}
}
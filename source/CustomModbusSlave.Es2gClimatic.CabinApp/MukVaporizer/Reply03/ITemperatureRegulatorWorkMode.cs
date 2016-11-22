namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukVaporizer.Reply03
{
	/// <summary>
	/// ����� ������ ���������� �����������
	/// </summary>
	interface ITemperatureRegulatorWorkMode {
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
namespace CustomModbusSlave.Es2gClimatic.Shared {
	/// <summary>
	/// ��������� ����-�� �� �� ����
	/// </summary>
	/// <typeparam name="T">��� �������, ������� ��������� ������</typeparam>
	public interface IBuilder<out T> {
		T Build();
	}
}
namespace CustomModbusSlave.Es2gClimatic {
	/// <summary>
	/// ��������� ����-�� �� �� ����
	/// </summary>
	/// <typeparam name="T">��� �������, ������� ��������� ������</typeparam>
	public interface IBuilder<out T> {
		T Build();
	}
}
namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03 {
	internal interface IMukFlapReturnAirConcentratorStatus {
		/// <summary>
		/// ������� �� ��� (1 - ����������)
		/// </summary>
		bool CommandToPal { get; }

		/// <summary>
		/// ������� � ��� (1- ��������� �����)
		/// </summary>
		bool CommandFromPal { get; }

		/// <summary>
		/// ������ / �������������
		/// </summary>
		bool WorkOrError { get; }

		/// <summary>
		/// ������ �� �������� ��������
		/// </summary>
		bool ErrorNoAnswerFromDriver { get; }

		/// <summary>
		/// ������ �� ���� CC
		/// </summary>
		bool ErrorByCurrentCc { get; }

		/// <summary>
		/// �������� �����������
		/// </summary>
		bool ComparatorValue { get; }

		/// <summary>
		/// ������
		/// </summary>
		bool Reserve { get; }

		/// <summary>
		/// �����
		/// </summary>
		bool Address { get; }
	}
}
using System;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukAirExhauster.Data.Contracts {
	static class AutomaticStageModeExtensions {
		public static string ToText(this AutomaticWorkmodeStage value) {
			switch (value) {
				case AutomaticWorkmodeStage.ControllerInitialization:
					return "������������� �����������";
				case AutomaticWorkmodeStage.WaitingForPowerOnCommand:
					return "�������� ������� �� ���������";
				case AutomaticWorkmodeStage.WorkingWithFanOnByTable:
					return "������ � ���������� ����������� �� �������";
				case AutomaticWorkmodeStage.Unknown:
					return "����������";
				default:
					throw new ArgumentOutOfRangeException(nameof(value), "����������� ��������");
			}
		}
	}
}
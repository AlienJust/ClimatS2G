using System;

namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	public static class ClimaticSystemWorkModeExtensions {
		public static string ToText(this ClimaticSystemWorkMode value) {
			switch (value) {
				case ClimaticSystemWorkMode.Off:
					return "���������";
				case ClimaticSystemWorkMode.On:
					return "��������";
				case ClimaticSystemWorkMode.Reserved:
					return "������";
				case ClimaticSystemWorkMode.DowntimeWhileOn:
					return "������ �� ���������� ���������";
				case ClimaticSystemWorkMode.Maintenance:
					return "���������������";
				case ClimaticSystemWorkMode.Washing:
					return "�����";
				case ClimaticSystemWorkMode.EmergencyVenting:
					return "��������� ����������";
				case ClimaticSystemWorkMode.EmergencyHeating:
					return "��������� ���������";
				case ClimaticSystemWorkMode.Test:
					return "������������ (������ �������)";
				default:
					throw new Exception("Cannot cast such " + typeof(ClimaticSystemWorkMode).FullName + " to string");
			}
		}
	}
}
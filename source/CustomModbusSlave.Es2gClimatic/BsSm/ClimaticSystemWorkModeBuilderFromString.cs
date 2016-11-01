using System;
using CustomModbusSlave.Es2gClimatic;
using CustomModbusSlave.Es2gClimatic.BsSm;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.BsSm {
	public class ClimaticSystemWorkModeBuilderFromString : IBuilder<ClimaticSystemWorkMode> {
		private readonly string _textValue;

		public ClimaticSystemWorkModeBuilderFromString(string textValue) {
			_textValue = textValue;
		}

		public ClimaticSystemWorkMode Build() {
			switch (_textValue) {
				case "���������":
					return ClimaticSystemWorkMode.Off;
				case "��������":
					return ClimaticSystemWorkMode.On;
				case "������":
					return ClimaticSystemWorkMode.Reserved;
				case "������ �� ���������� ���������":
					return ClimaticSystemWorkMode.DowntimeWhileOn;
				case "���������������":
					return ClimaticSystemWorkMode.Maintenance;
				case "�����":
					return ClimaticSystemWorkMode.Washing;
				case "��������� ����������":
					return ClimaticSystemWorkMode.EmergencyVenting;
				case "��������� ���������":
					return ClimaticSystemWorkMode.EmergencyHeating;
				case "������������ (������ �������)":
					return ClimaticSystemWorkMode.Test;
				case "�����������":
					return ClimaticSystemWorkMode.Unknown;
				default:
					throw new Exception("Cannot convert text " + _textValue + " to " + typeof(ClimaticSystemWorkMode).FullName);
			}
		}
	}
}
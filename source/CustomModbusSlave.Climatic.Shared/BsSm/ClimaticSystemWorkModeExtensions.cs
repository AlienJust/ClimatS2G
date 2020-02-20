using System;

namespace CustomModbusSlave.Es2gClimatic.Shared.BsSm {
	public static class ClimaticSystemWorkModeExtensions {
		public static string ToText(this ClimaticSystemWorkMode value) {
			switch (value) {
				case ClimaticSystemWorkMode.Off:
					return "Выключено";
				case ClimaticSystemWorkMode.On:
					return "Включено";
				case ClimaticSystemWorkMode.Reserved:
					return "Резерв";
				case ClimaticSystemWorkMode.DowntimeWhileOn:
					return "Отстой во включённом состоянии";
				case ClimaticSystemWorkMode.Maintenance:
					return "Техобслуживание";
				case ClimaticSystemWorkMode.Washing:
					return "Мойка";
				case ClimaticSystemWorkMode.EmergencyVenting:
					return "Аварийная вентиляция";
				case ClimaticSystemWorkMode.EmergencyHeating:
					return "Аварийное отопление";
				case ClimaticSystemWorkMode.Test:
					return "Тестирование (запуск системы)";
				case ClimaticSystemWorkMode.Unknown:
					return "Неизвестно";
				default:
					throw new Exception("Cannot cast such " + typeof(ClimaticSystemWorkMode).FullName + " to string");
			}
		}
	}
}

using System;

namespace CustomModbusSlave.Es2gClimatic.Shared.BsSm {
	public class ClimaticSystemWorkModeBuilderFromString : IBuilder<ClimaticSystemWorkMode> {
		private readonly string _textValue;

		public ClimaticSystemWorkModeBuilderFromString(string textValue) {
			_textValue = textValue;
		}

		public ClimaticSystemWorkMode Build() {
			switch (_textValue) {
				case "Выключено":
					return ClimaticSystemWorkMode.Off;
				case "Включено":
					return ClimaticSystemWorkMode.On;
				case "Резерв":
					return ClimaticSystemWorkMode.Reserved;
				case "Отстой во включённом состоянии":
					return ClimaticSystemWorkMode.DowntimeWhileOn;
				case "Техобслуживание":
					return ClimaticSystemWorkMode.Maintenance;
				case "Мойка":
					return ClimaticSystemWorkMode.Washing;
				case "Аварийная вентиляция":
					return ClimaticSystemWorkMode.EmergencyVenting;
				case "Аварийное отопление":
					return ClimaticSystemWorkMode.EmergencyHeating;
				case "Тестирование (запуск системы)":
					return ClimaticSystemWorkMode.Test;
				case "Неизваестно":
					return ClimaticSystemWorkMode.Unknown;
				default:
					throw new Exception("Cannot convert text " + _textValue + " to " + typeof(ClimaticSystemWorkMode).FullName);
			}
		}
	}
}

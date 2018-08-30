using AlienJust.Support.Conversion.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.Ksm.WamOrCoolForcedModes {
	public sealed class WarmOrCoolForcedModeToStringConverter : IBuilderOneToOne<WarmOrCoolForcedMode, string> {
		public string Build(WarmOrCoolForcedMode source) {
			switch (source) {
				case WarmOrCoolForcedMode.AutomaticMode:
					return "Авторежим";
				case WarmOrCoolForcedMode.Cool100Percent:
					return "Охлаждение 100%";
				case WarmOrCoolForcedMode.Cool50Percent:
					return "Охлаждение 50%";
				case WarmOrCoolForcedMode.Ventilation:
					return "Вентиляция";
				case WarmOrCoolForcedMode.Warm100Percent:
					return "Нагрев 100%";
				case WarmOrCoolForcedMode.Warm50Percent:
					return "Нагрев 50%";
				case WarmOrCoolForcedMode.TurnOnUov:
					return "Включение УОВ";
				case WarmOrCoolForcedMode.Test3000VoltageHeating:
					return "Тест 3кВ обогрева";
				case WarmOrCoolForcedMode.Unknown:
					return "ХЗ, что за значение";
				default:
					return "Что-то не так, нужно дописать программу";
			}
		}
	}
}
using System;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters {
	public static class ManualForcedModeExtensions {
		public static string ToText(this ManualForcedMode value) {
			switch (value) {
				case ManualForcedMode.Cool100Percent:
					return "Охлаждение 100%";
				case ManualForcedMode.Cool50Percent:
					return "Охлаждение 50%";
				case ManualForcedMode.Fan:
					return "Вентиляция";
				case ManualForcedMode.Heat100Percent:
					return "Нагрев 100%";
				case ManualForcedMode.Heat50Percent:
					return "Нагрев 50%";
				case ManualForcedMode.Unknown:
					return "Неизвестно";
				default:
					throw new ArgumentOutOfRangeException(nameof(value), value, null);
			}
		}
	}
}
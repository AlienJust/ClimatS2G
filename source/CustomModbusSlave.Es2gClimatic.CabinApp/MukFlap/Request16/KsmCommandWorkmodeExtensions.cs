using System;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Request16 {
	internal static class KsmCommandWorkmodeExtensions {
		public static string ToText(this KsmCommandWorkmode mode) {
			switch (mode) {
				case KsmCommandWorkmode.Off:
					return "Выключено";
				case KsmCommandWorkmode.Auto:
					return "Работа в автоматическом режиме";
				case KsmCommandWorkmode.Manual:
					return "Работа по ручной уставке";
				case KsmCommandWorkmode.Unknown:
					return "Неизвестное значение";
				default:
					throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
			}
		}
	}
}
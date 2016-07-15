using System;
using AlienJust.Support.Collections;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.TextFormatters {
	public interface ITextFormatter<in T> {
		string Format(T value);
	}

	public class TextFormatterForcedManualMode: ITextFormatter<BytesPair?> {
		public string Format(BytesPair? value) {
			if (!value.HasValue) return "? - нет данных";
			return value.Value.HighFirstUnsignedValue + " - " + new ManualForcedModeBuilder(value.Value.HighFirstUnsignedValue).Build().ToText();
		}
	}

	public enum ManualForcedMode {
		Cool100Percent, // 1
		Cool50Percent, // 2
		Fan, // 3
		Heat100Percent, // 4
		Heat50Percent, // 5
		Unknown // 6
	}

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

	public class ManualForcedModeBuilder : IBuilder<ManualForcedMode> {
		private readonly ushort _value;
		public ManualForcedModeBuilder(ushort value) {
			_value = value;
		}

		public ManualForcedMode Build() {
			switch (_value) {
				case 1:
					return ManualForcedMode.Cool100Percent;
				case 2:
					return ManualForcedMode.Cool50Percent;
				case 3:
					return ManualForcedMode.Fan;
				case 4:
					return ManualForcedMode.Heat100Percent;
				case 5:
					return ManualForcedMode.Heat50Percent;
				default:
					return ManualForcedMode.Unknown;
			}
		}
	}
}
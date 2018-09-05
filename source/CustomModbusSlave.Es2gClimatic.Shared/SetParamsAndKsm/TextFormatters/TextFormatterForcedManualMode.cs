using AlienJust.Support.Collections;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters {
	public class TextFormatterForcedManualMode: ITextFormatter<BytesPair?> {
		public string Format(BytesPair? value) {
			if (!value.HasValue) return "? - нет данных";
			return value.Value.HighFirstUnsignedValue + " - " + new ManualForcedModeBuilder(value.Value.HighFirstUnsignedValue).Build().ToText();
		}
	}
}
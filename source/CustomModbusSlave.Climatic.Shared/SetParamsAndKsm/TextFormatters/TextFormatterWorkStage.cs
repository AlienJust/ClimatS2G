using AlienJust.Support.Collections;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters {
	public class TextFormatterWorkStage : ITextFormatter<BytesPair?> {
		public string Format(BytesPair? value) {
			if (!value.HasValue)
				return "--";
			return value.Value.HighFirstUnsignedValue + " - " + new KsmWorkstageBuilder(value.Value.HighFirstUnsignedValue).Build().ToText();
		}
	}
}
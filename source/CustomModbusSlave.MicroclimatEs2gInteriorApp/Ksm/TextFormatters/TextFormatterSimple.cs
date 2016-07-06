using AlienJust.Support.Collections;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm.TextFormatters {
	class TextFormatterSimple : ITextFormatter<BytesPair?> {
		private readonly string _format;
		private readonly string _nullFormat;

		public TextFormatterSimple(string format, string nullFormat) {
			_format = format;
			_nullFormat = nullFormat;
		}

		public string Format(BytesPair? value) {
			if (!value.HasValue) return _nullFormat;
			return value.Value.HighFirstUnsignedValue.ToString(_format);
		}
	}
}
using AlienJust.Support.Collections;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm.TextFormatters {
	class TextFormatterBits : ITextFormatter<BytesPair?> {
		private readonly string _nullFormat;

		public TextFormatterBits(string nullFormat) {
			_nullFormat = nullFormat;
		}

		public string Format(BytesPair? value) {
			if (!value.HasValue) return _nullFormat;
			return Convert(value.Value.HighFirstUnsignedValue);
		}

		string Convert(ushort x) {
			string result = string.Empty;

			for (int j = 0; j < 4; ++j) { // 4 times
				for (int i = 0; i < 4; ++i) { // 4 bits
					if (((x >> i + j*4) & 0x01) == 0x01) result += "1";
					else result += "0";
				}
				result += " ";
			}

			return result;
		}
	}
}
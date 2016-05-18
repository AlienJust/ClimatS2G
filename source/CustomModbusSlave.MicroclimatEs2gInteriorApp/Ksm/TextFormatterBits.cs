using System;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class TextFormatterBits : ITextFormatter<ushort?> {
		private readonly string _nullFormat;

		public TextFormatterBits(string nullFormat) {
			_nullFormat = nullFormat;
		}

		public string Format(ushort? value) {
			if (!value.HasValue) return _nullFormat;
			return Convert(value.Value);
		}

		string Convert(ushort x) {
			string result = string.Empty;

			for(int i = 0; i < 16; ++i) {
				if (((x >> i) & 0x01) == 0x01) result += "1";
				else result += "0";
			}

			return result;
		}
	}
}
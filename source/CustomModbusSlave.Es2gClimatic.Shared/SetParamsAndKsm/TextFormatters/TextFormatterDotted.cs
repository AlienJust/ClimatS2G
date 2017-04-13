using AlienJust.Support.Collections;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters {
	public class TextFormatterDotted : ITextFormatter<BytesPair?> {
		private readonly string _nullFormat;

		public TextFormatterDotted(string nullFormat) {
			_nullFormat = nullFormat;
		}

		public string Format(BytesPair? value) {
			if (!value.HasValue) return _nullFormat;
			var frmt = value.Value.HighFirstUnsignedValue.ToString("D");

			var result = string.Empty;
			if (frmt == string.Empty) return result;

			result += frmt[0];
			for (int i = 1; i < frmt.Length; ++i) {
				result += "." + frmt[i];
			}
			return result;
		}
	}
}
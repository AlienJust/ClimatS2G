using AlienJust.Support.Collections;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.UshortToDoubleConverter;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters {
	public class TextFormatterNcalcDouble : ITextFormatter<BytesPair?> {
		private readonly string _doubleFormat;
		private readonly string _nullFormat;
		private readonly UshortToDoubleConverterNcalc _toDoubleConverter;

		public TextFormatterNcalcDouble(string ncalcExpression, string doubleFormat, string nullFormat) {
			_toDoubleConverter = new UshortToDoubleConverterNcalc(ncalcExpression);
			_doubleFormat = doubleFormat;
			_nullFormat = nullFormat;
		}

		public string Format(BytesPair? value) {
			if (!value.HasValue) return _nullFormat;

			var cv = _toDoubleConverter.Convert(value.Value.HighFirstUnsignedValue);
			if (!cv.HasValue) return _nullFormat;

			return cv.Value.ToString(_doubleFormat);
		}
	}
}
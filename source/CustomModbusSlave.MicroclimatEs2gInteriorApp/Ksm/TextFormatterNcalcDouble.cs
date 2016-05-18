namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class TextFormatterNcalcDouble : ITextFormatter<ushort?> {
		private readonly string _doubleFormat;
		private readonly string _nullFormat;
		private readonly UshortToDoubleConverterNcalc _toDoubleConverter;

		public TextFormatterNcalcDouble(string ncalcExpression, string doubleFormat, string nullFormat) {
			_toDoubleConverter = new UshortToDoubleConverterNcalc(ncalcExpression);
			_doubleFormat = doubleFormat;
			_nullFormat = nullFormat;
		}

		public string Format(ushort? value) {
			if (!value.HasValue) return _nullFormat;

			var cv = _toDoubleConverter.Convert(value);
			if (!cv.HasValue) return _nullFormat;

			return cv.Value.ToString(_doubleFormat);
		}
	}
}
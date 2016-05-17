namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class DoubleTextFormatterSimple : IDoubleTextFormatter {
		private readonly string _format;
		private readonly string _nullFormat;

		public DoubleTextFormatterSimple(string format, string nullFormat) {
			_format = format;
			_nullFormat = nullFormat;
		}

		public string Format(double? value) {
			if (!value.HasValue) return _nullFormat;
			return value.Value.ToString(_format);
		}
	}
}
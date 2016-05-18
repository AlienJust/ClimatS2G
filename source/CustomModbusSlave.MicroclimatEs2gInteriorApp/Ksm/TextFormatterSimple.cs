namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class TextFormatterSimple : ITextFormatter<ushort?> {
		private readonly string _format;
		private readonly string _nullFormat;

		public TextFormatterSimple(string format, string nullFormat) {
			_format = format;
			_nullFormat = nullFormat;
		}

		public string Format(ushort? value) {
			if (!value.HasValue) return _nullFormat;
			return value.Value.ToString(_format);
		}
	}
}
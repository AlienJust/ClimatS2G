using AlienJust.Support.Collections;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.BytesPairConverters.Nullable {
	public class BytesPairNullableToStringThroughDoubleConverter : IBytesPairNullableSomethingConverter<string> {
		private readonly double _multiplier;
		private readonly double _addition;
		private readonly bool _signed;
		private readonly bool _hiFirst;
		private readonly string _format;

		public BytesPairNullableToStringThroughDoubleConverter(double multiplier, double addition, bool signed, bool hiFirst, string format) {
			_multiplier = multiplier;
			_addition = addition;
			_signed = signed;
			_hiFirst = hiFirst;
			_format = format;
		}

		public string ConvertToSomething(BytesPair? value) {
			if (!value.HasValue) return "?";

			double rawValue;

			if (_signed && _hiFirst) rawValue = value.Value.HighFirstSignedValue;
			else if (!_signed && _hiFirst) rawValue = value.Value.HighFirstUnsignedValue;
			else if (_signed && !_hiFirst) rawValue = value.Value.LowFirstSignedValue;
			else /*if (!_signed && !_hiFirst)*/ rawValue = value.Value.LowFirstUnsignedValue;

			return (rawValue*_multiplier + _addition).ToString(_format);
		}
	}
}

using AlienJust.Support.Collections;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams.BytesPairNullableConverters;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.UniversalParams.BytesPairConverters {
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

	public class BytesPairNullableToStringThroughOneWireConverter : IBytesPairNullableSomethingConverter<string> {
		private readonly double _multiplier;
		private readonly double _addition;
		private readonly string _format;
		private readonly BytesPair _noLinkCode;

		public BytesPairNullableToStringThroughOneWireConverter(double multiplier, double addition, string format, BytesPair noLinkCode) {
			_multiplier = multiplier;
			_addition = addition;
			_format = format;
			_noLinkCode = noLinkCode;
		}

		public string ConvertToSomething(BytesPair? value) {
			if (!value.HasValue) return "?";

			//double rawValue;

			//if (_signed ) rawValue = value.Value.HighFirstSignedValue;
			//else if (!_signed ) rawValue = value.Value.HighFirstUnsignedValue;

			var si = new SensorIndicationDoubleBasedOnBytesPair(value.Value, _multiplier, _addition, _noLinkCode);
			if (si.NoLinkWithSensor) return "Обрыв датчика";

			return si.Indication.ToString(_format);
		}
	}
}
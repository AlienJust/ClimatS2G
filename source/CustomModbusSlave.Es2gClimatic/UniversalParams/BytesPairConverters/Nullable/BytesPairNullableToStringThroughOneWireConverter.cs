using AlienJust.Support.Collections;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams.BytesPairNullableConverters;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.UniversalParams.BytesPairConverters {
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
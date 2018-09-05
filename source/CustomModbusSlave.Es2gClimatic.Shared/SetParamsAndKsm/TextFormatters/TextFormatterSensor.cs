using AlienJust.Support.Collections;
using CustomModbusSlave.Es2gClimatic.Shared.OneWire;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters {
	public class TextFormatterSensor : ITextFormatter<BytesPair?> {
		private readonly double _modifier;
		private readonly double _addition;
		private readonly BytesPair _noLinkCode;
		private readonly string _format;
		private readonly string _nullFormat;
		private readonly string _noLinkFormat;

		public TextFormatterSensor(double modifier, double addition, BytesPair noLinkCode, string format, string nullFormat, string noLinkFormat) {
			_modifier = modifier;
			_addition = addition;
			_noLinkCode = noLinkCode;
			_format = format;
			_nullFormat = nullFormat;
			_noLinkFormat = noLinkFormat;
		}

		public string Format(BytesPair? value) {
			if (!value.HasValue) return _nullFormat;
			var sensorIndication = new SensorIndicationDoubleBasedOnBytesPair(value.Value, _modifier, _addition, _noLinkCode);
			if (sensorIndication.NoLinkWithSensor)
				return _noLinkFormat;
			return sensorIndication.Indication.ToString(_format);
		}
	}
}

using System.Globalization;

namespace CustomModbusSlave.Es2gClimatic.Shared.OneWire {
	public static class SensorIndicationDoubleExt {
		public static string ToText(this ISensorIndication<double> val, string doubleFormat) {
			if (val == null) return "Нет данных (null)";
			if (val.NoLinkWithSensor) return "Обрыв датчика";
			return val.Indication.ToString(doubleFormat, CultureInfo.InvariantCulture);
		}
	}
}
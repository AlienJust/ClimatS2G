using System;
using System.Globalization;
using System.Windows.Data;

namespace CustomModbusSlave.Es2gClimatic.Shared.OneWire {
	[ValueConversion(typeof(double), typeof(int))]
	public class SensorIndicationDoubleToStringConverter : IValueConverter {
		public string Format { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			try {
				if (value == null) return "Нет данных (null)";
				if (!(value is ISensorIndication<double> ns)) return "Нет данных";
				if (ns.NoLinkWithSensor) return "Обрыв датчика";
				return ns.Indication.ToString(Format, CultureInfo.InvariantCulture);
			}
			catch (Exception ex) {
				return ex.Message;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new Exception("TODO");
		}
	}
}

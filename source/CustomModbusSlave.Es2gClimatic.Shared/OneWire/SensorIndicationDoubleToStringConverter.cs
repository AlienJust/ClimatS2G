using System;
using System.Globalization;
using System.Windows.Data;

namespace CustomModbusSlave.Es2gClimatic.Shared.SensorIndications {
	[ValueConversion(typeof(double), typeof(int))]
	public class SensorIndicationDoubleToStringConverter : IValueConverter {
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			try {
				//if (targetType != typeof(ISensorIndication<double>))
					//throw new Exception($"Wrong type, expected {typeof(ISensorIndication<double>).FullName}");

				var ns = value as ISensorIndication<double>;
				if (ns == null) return "Нет данных";


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

		public string Format { get; set; }
		#endregion
	}
}
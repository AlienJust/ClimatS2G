using System;
using System.Globalization;
using System.Windows.Data;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.View {
	[ValueConversion(typeof(double), typeof(int))]
	class SensorIndicationDoubleToStringConverter : IValueConverter {
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			try {
				//if (targetType != typeof(ISensorIndication<double>))
					//throw new Exception($"Wrong type, expected {typeof(ISensorIndication<double>).FullName}");

				var ns = value as ISensorIndication<double>;
				if (ns == null) return "Нет данных";


				if (ns.NoLinkWithSensor) return "Обрыв датчика";
				return ns.Indication.ToString(Format);
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
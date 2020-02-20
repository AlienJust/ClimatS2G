using System;
using System.Windows.Data;

namespace CustomModbusSlave.Es2gClimatic.Shared.BsSm {
	[ValueConversion(typeof(double), typeof(int))]
	public sealed class ClimaticSystemWorkModeToStringNotThrowableConverterTwoWay : IValueConverter {
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			try {
				if (value != null) {
					var ns = (ClimaticSystemWorkMode)value; // TODO: might throw exception?
					return ns.ToText();
				}
				return "null";
			}
			catch (Exception ex) {
				return ex.Message;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			var nd = (string)value;
			try {
				return new ClimaticSystemWorkModeBuilderFromString(nd).Build();
			}
			catch {
				return ClimaticSystemWorkMode.Unknown;
			}

		}

		#endregion
	}
}

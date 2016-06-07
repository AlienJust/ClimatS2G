using System;
using System.Windows.Data;
using CustomModbusSlave.MicroclimatEs2gApp.Common.BsSm;

namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	[ValueConversion(typeof(double), typeof(int))]
	class ThrowableClimaticSystemWorkModeToStringConverter : IValueConverter {
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			try {
				var ns = (ClimaticSystemWorkMode)value; // TODO: might throw exception?

				return ns.ToText();
			}
			catch (Exception ex) {
				return ex.Message;
			}
			
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			var nd = (string) value;

			return new ClimaticSystemWorkModeBuilderFromString(nd).Build();
		}

		#endregion
	}
}
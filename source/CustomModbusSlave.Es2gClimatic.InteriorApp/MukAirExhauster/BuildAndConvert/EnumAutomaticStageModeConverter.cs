using System;
using System.Windows.Data;
using CustomModbusSlave.MicroclimatEs2gApp.MukAirExhauster.Data.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Converters {
	[ValueConversion(typeof(double), typeof(int))]
	class EnumAutomaticStageModeConverter : IValueConverter {
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			var ns = (AutomaticWorkmodeStage)value; // TODO: might throw exception?
			return ns.ToText();

		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			// TODO: if needed
			throw new NotImplementedException("implement if needed");
		}

		#endregion
	}
}
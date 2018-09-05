using System;
using System.Windows.Data;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Data;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Data.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.BuildAndConvert {
	[ValueConversion(typeof(double), typeof(int))]
	class EnumAutomaticStageModeConverter : IValueConverter {
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if (value != null) {
				var ns = (AutomaticWorkmodeStage) value; // TODO: might throw exception?
				return ns.ToText();
			}

			return "X3 (null)";
		}

		public object ConvertBack(object value, Type targetType, object parameter
			, System.Globalization.CultureInfo culture) {
			// TODO: if needed
			throw new NotImplementedException("implement if needed");
		}

		#endregion
	}
}

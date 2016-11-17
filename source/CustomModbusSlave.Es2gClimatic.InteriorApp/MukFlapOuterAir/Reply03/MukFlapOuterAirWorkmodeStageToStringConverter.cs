using System;
using System.Globalization;
using System.Windows.Data;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts.Enums;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03 {
	[ValueConversion(typeof(double), typeof(int))]
	class MukFlapOuterAirWorkmodeStageToStringConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			try {

				var ns = (MukFlapOuterAirWorkmodeStage)value;

				return ns.ToText();
			}
			catch (Exception ex) {
				return ex.Message;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new Exception("TODO");
		}

		public string Format { get; set; }
	}
}
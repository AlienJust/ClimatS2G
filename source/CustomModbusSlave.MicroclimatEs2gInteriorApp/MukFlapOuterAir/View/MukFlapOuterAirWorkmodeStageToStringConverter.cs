using System;
using System.Globalization;
using System.Windows.Data;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts.Enums;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.View {
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
using System;
using System.Globalization;
using System.Windows.Data;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03 {
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
			throw new NotImplementedException("TODO: implement when needed");
		}

		public string Format { get; set; }
	}
}

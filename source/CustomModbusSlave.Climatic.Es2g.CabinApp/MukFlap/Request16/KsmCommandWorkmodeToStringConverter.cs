using System;
using System.Windows.Data;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Request16 {
	[ValueConversion(typeof(double), typeof(int))]
	class KsmCommandWorkmodeToStringConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			try {
				var ns = (KsmCommandWorkmode)value; // TODO: might throw exception?

				return ns.ToText();
			}
			catch (Exception ex) {
				return ex.Message;
			}
			
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			//var nd = (string) value;
			throw new NotImplementedException("Не написано, пока что не было надобности");
		}
	}
}

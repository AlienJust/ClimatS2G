using System;
using System.Security.Policy;
using System.Windows.Data;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.Converters {
	[ValueConversion(typeof(MukFlapOuterAirWorkmodeStage), typeof(string))]
	class EnumMukFlapOuterAirWorkmodeStageToStringConverter : IValueConverter {
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			switch (value) {
				case null:
					return "null";
				case MukFlapOuterAirWorkmodeStage ns:
					return ns.ToText();
				default:
					return "Not " + typeof(MukFlapOuterAirWorkmodeStage).Name;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			// TODO: if needed
			throw new NotImplementedException("implement if needed");
		}

		#endregion
	}
}

using System;
using System.Globalization;
using System.Windows.Data;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.BytesPairConverters.WpfValueConverters
{
	[ValueConversion(typeof(object), typeof(string))]
	public class ObjectToStringConverter : IValueConverter {
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			try {
				if (value == null) return NullFormat;
				return value.ToString();
			}
			catch (Exception ex) {
				return ex.Message;
			}
		}

		public string NullFormat { get; set; }

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
		#endregion
	}
}
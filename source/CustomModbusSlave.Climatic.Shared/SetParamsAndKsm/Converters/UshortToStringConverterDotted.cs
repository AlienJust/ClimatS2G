using System;
using System.Globalization;
using System.Windows.Data;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.Converters {
	[ValueConversion(typeof(ushort), typeof(string))]
	public class UshortToStringConverterDotted : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			try {
				if (targetType != typeof(string))
					throw new Exception($"Wrong type {targetType.FullName}, expected {typeof(string).FullName}");

				var ns = (ushort)value; // TODO: might throw exception?

				var result = new TextFormatterIntegerDotted().Format(ns);
				return result;
			}
			catch (Exception ex) {
				return ex.Message;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException("TODO");
		}
	}
}

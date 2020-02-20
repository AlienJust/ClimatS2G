using System;
using System.Windows.Data;

namespace CustomModbusSlave.Es2gClimatic.Shared.BsSm {
	[ValueConversion(typeof(double), typeof(int))]
	public sealed class ClimaticSystemWorkModeToStringThrowableConverterTwoWay : IValueConverter {
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if (value != null)
			{
				var ns = (ClimaticSystemWorkMode)value; // TODO: might throw exception?

				return ns.ToText();
			}

			throw new NullReferenceException("Expected value type of " + typeof(double).FullName + ", but received null :o");
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			var nd = (string) value;

			return new ClimaticSystemWorkModeBuilderFromString(nd).Build();
		}

		#endregion
	}
}

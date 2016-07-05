using System;
using System.Globalization;
using System.Windows.Data;
using AlienJust.Support.Collections;
using NCalc;

namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	[ValueConversion(typeof(double), typeof(int))]
	class BytesPairToTemperatureConverter : IValueConverter {
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			try {
				if (targetType != typeof(BytesPair))
					throw new Exception($"Wrong type, expected {typeof(BytesPair).FullName}");

				var ns = (BytesPair)value; // TODO: might throw exception?


				var expr = new Expression(MathExpression);
				expr.Parameters.Add("b1", ns.First);
				expr.Parameters.Add("b2", ns.Second);
				expr.Parameters.Add("u1hi", ns.HighFirstUnsignedValue);
				expr.Parameters.Add("u1lo", ns.LowFirstUnsignedValue);
				expr.Parameters.Add("s1hi", ns.HighFirstSignedValue);
				expr.Parameters.Add("s1lo", ns.LowFirstSignedValue);

				double result = (double)expr.Evaluate();
				return result.ToString(Format);
			}
			catch (Exception ex) {
				return ex.Message;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			var nd = value.ToString();

			var doubleResult = double.Parse(nd, CultureInfo.InvariantCulture);

			throw new Exception("TODO");
		}

		public string MathExpression { get; set; }

		public string Format { get; set; }

		#endregion
	}
}
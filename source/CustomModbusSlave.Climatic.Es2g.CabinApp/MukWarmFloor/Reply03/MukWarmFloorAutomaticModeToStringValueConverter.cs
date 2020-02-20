using System;
using System.Globalization;
using System.Windows.Data;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03
{
	[ValueConversion(typeof(MukWarmFloorAutomaticModeStage), typeof(string))]
	internal sealed class MukWarmFloorAutomaticModeToStringValueConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try {
				if (targetType != typeof(string))
					throw new Exception($"Wrong type {targetType.FullName}, expected {typeof(string).FullName}");

				if (value != null)
				{
					var ns = (MukWarmFloorAutomaticModeStage)value; // TODO: might throw exception?

					var result = new MukWarmFloorAutomaticModeStageToTextBuilder().Build(ns);
					return result;
				}
				throw new NullReferenceException("Incoming for conversion value is null");
			}
			catch (Exception ex) {
				return ex.Message;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

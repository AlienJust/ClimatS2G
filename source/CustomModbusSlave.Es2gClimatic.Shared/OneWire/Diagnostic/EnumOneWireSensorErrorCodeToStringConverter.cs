using System;
using System.Windows.Data;

namespace CustomModbusSlave.Es2gClimatic.Shared.OneWire.Diagnostic {
	[ValueConversion(typeof(OneWireSensorErrorCode), typeof(string))]
	public class EnumOneWireSensorErrorCodeToStringConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			var ns = (OneWireSensorErrorCode)value; // TODO: might throw exception?

			switch (ns) {
				case OneWireSensorErrorCode.FoundDeviceWithUnknownFamilyCode:
					return "Найдено устройство с неизвестным кодом семейства";
				case OneWireSensorErrorCode.SensorNotFound:
					return "Не найден датчик";
				case OneWireSensorErrorCode.NoReactionOnReset:
					return "Нет реакции на сигнал reset";
				case OneWireSensorErrorCode.SensorShowsIncorrectWorking:
					return "Датчик индицирует некорректную работу";
				case OneWireSensorErrorCode.NoError:
					return "Нет ошибок, или неизвестный код";
				default:
					return ns.ToString();
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			// TODO: if needed
			throw new NotImplementedException("implement if needed");
		}
	}
}

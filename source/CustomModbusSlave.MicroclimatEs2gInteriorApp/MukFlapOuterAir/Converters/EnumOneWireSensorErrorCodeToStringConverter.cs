using System;
using System.Windows.Data;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts.Enums;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Converters {
	[ValueConversion(typeof(double), typeof(int))]
	class EnumOneWireSensorErrorCodeToStringConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			var ns = (OneWireSensorErrorCode)value; // TODO: might throw exception?

			switch (ns) {
				case OneWireSensorErrorCode.FoundDeviceWithUnknownFamilyCode:
					return "������� ���������� � ����������� ����� ���������";
				case OneWireSensorErrorCode.SensorNotFound:
					return "�� ������ ������";
				case OneWireSensorErrorCode.NoReactionOnReset:
					return "��� ������� �� ������ reset";
				case OneWireSensorErrorCode.SensorShowsIncorrectWorking:
					return "������ ���������� ������������ ������";
				case OneWireSensorErrorCode.NoError:
					return "��� ������, ��� ����������� ���";
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
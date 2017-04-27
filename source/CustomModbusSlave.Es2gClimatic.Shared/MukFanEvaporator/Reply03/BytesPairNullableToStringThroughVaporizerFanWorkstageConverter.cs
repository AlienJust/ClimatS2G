using System;
using System.Windows.Data;
using AlienJust.Support.Collections;
using CustomModbusSlave.Es2gClimatic.Shared.MukFlap.DiagnosticOneWire;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams.BytesPairNullableConverters;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03 {
	public class BytesPairNullableToStringThroughVaporizerFanWorkstageConverter : IBytesPairNullableSomethingConverter<string> {
		public string ConvertToSomething(BytesPair? value) {
			if (!value.HasValue) return "?";

			var val = value.Value.HighFirstUnsignedValue;
			var result = $"{val} - ";
			switch (val) {
				case 0:
					result += "������������� �����������";
					break;
				case 1:
					result += "���� �����������";
					break;
				case 2:
					result += "������� ����� � ��������� ������������";
					break;
				case 3:
					result += "����� ������ � ����������� ������������";
					break;
				case 4:
					result += "���������� ���� ���� � ��� �� ���������� ������ �� �����������";
					break;
				default:
					result += "����������";
					break;
			}

			return result;
		}
	}

	public enum MukFanEvaporatorWorkstage {
		ControllerInit,
		FanTest,
		WorkAndFanIsGood,
		WorkAndFanIsBad,
		AllSwitchesAndPwmAreCauseNoTemperatureData,
		Unknown
	}

	class MukFanEvaporatorWorkstageBuilder : IBuilder<MukFanEvaporatorWorkstage> {
		private readonly ushort _data;

		public MukFanEvaporatorWorkstageBuilder(ushort data) {
			_data = data;
		}

		public MukFanEvaporatorWorkstage Build() {
			switch (_data) {
				case 0: return MukFanEvaporatorWorkstage.ControllerInit;
				case 1: return MukFanEvaporatorWorkstage.FanTest;
				case 2: return MukFanEvaporatorWorkstage.WorkAndFanIsGood;
				case 3: return MukFanEvaporatorWorkstage.WorkAndFanIsBad;
				case 4: return MukFanEvaporatorWorkstage.AllSwitchesAndPwmAreCauseNoTemperatureData;
				default: return MukFanEvaporatorWorkstage.Unknown;
			}
		}
	}

	static class MukFanEvaporatorWorkstageExt {
		public static string ToText(this MukFanEvaporatorWorkstage val) {
			switch (val) {
				case MukFanEvaporatorWorkstage.ControllerInit:
					return "������������� �����������";
				case MukFanEvaporatorWorkstage.FanTest:
					return "���� �����������";
				case MukFanEvaporatorWorkstage.WorkAndFanIsGood:
					return "������� ����� � ��������� ������������";
				case MukFanEvaporatorWorkstage.WorkAndFanIsBad:
					return "����� ������ � ����������� ������������";
				case MukFanEvaporatorWorkstage.AllSwitchesAndPwmAreCauseNoTemperatureData:
					return "���������� ���� ���� � ��� �� ���������� ������ �� �����������";
				case MukFanEvaporatorWorkstage.Unknown:
					return "����������";
				default:
					return "[�� �����������]";
			}
		}
	}

	[ValueConversion(typeof(MukFanEvaporatorWorkstage), typeof(string))]
	public class MukFanEvaporatorWorkstageToStringConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			var enumedValue = (MukFanEvaporatorWorkstage)value; // TODO: might throw exception?
			return enumedValue.ToText();
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			// TODO: if needed
			throw new NotImplementedException("implement if needed");
		}
	}
}
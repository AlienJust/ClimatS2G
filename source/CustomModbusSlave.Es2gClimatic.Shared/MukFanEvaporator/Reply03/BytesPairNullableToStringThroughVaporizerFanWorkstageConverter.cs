using System;
using System.Windows.Data;
using AlienJust.Support.Collections;
using CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.BytesPairConverters.Nullable;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03 {
	public class BytesPairNullableToStringThroughVaporizerFanWorkstageConverter : IBytesPairNullableSomethingConverter<string> {
		public string ConvertToSomething(BytesPair? value) {
			if (!value.HasValue) return "?";

			var val = value.Value.HighFirstUnsignedValue;
			var result = $"{val} - ";
			switch (val) {
				case 0:
					result += "инициализация контроллера";
					break;
				case 1:
					result += "тест вентилятора";
					break;
				case 2:
					result += "рабочий режим с исправным вентилятором";
					break;
				case 3:
					result += "режим работы с неисправным вентилятором";
					break;
				case 4:
					result += "выключение всех реле и ШИМ по отсутствию данных по температуре";
					break;
				default:
					result += "неизвестно";
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
					return "инициализация контроллера";
				case MukFanEvaporatorWorkstage.FanTest:
					return "тест вентилятора";
				case MukFanEvaporatorWorkstage.WorkAndFanIsGood:
					return "рабочий режим с исправным вентилятором";
				case MukFanEvaporatorWorkstage.WorkAndFanIsBad:
					return "режим работы с неисправным вентилятором";
				case MukFanEvaporatorWorkstage.AllSwitchesAndPwmAreCauseNoTemperatureData:
					return "выключение всех реле и ШИМ по отсутствию данных по температуре";
				case MukFanEvaporatorWorkstage.Unknown:
					return "неизвестно";
				default:
					return "[не реализовано]";
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

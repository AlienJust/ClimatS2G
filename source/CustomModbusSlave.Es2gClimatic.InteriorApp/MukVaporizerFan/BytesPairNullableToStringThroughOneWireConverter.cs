using AlienJust.Support.Collections;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams.BytesPairNullableConverters;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukVaporizerFan {
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
}
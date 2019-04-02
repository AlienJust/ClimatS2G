using AlienJust.Support.Collections;
using CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.BytesPairConverters.Nullable;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.Reply03
{
    public class BytesPairNullableToStringThroughVaporizerFanWorkstageConverter : IBytesPairNullableSomethingConverter<string>
    {
        public string ConvertToSomething(BytesPair? value)
        {
            if (!value.HasValue) return "?";

            var val = value.Value.HighFirstUnsignedValue;
            var result = $"{val} - ";
            switch (val)
            {
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
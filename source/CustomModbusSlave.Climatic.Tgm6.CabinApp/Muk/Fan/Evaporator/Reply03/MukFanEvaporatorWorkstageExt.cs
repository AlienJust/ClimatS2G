namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.Reply03
{
    static class MukFanEvaporatorWorkstageExt
    {
        public static string ToText(this MukFanEvaporatorWorkstage val)
        {
            switch (val)
            {
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
}
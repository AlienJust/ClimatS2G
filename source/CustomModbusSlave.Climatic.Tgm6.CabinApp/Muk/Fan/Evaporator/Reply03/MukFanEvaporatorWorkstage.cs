namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.Reply03
{
    public enum MukFanEvaporatorWorkstage
    {
        ControllerInit,
        FanTest,
        WorkAndFanIsGood,
        WorkAndFanIsBad,
        AllSwitchesAndPwmAreCauseNoTemperatureData,
        Unknown
    }
}
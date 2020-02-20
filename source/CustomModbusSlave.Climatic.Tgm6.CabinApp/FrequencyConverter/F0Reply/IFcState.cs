namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.FrequencyConverter.F0Reply
{
    public interface IFcState
    {
        bool OverloadByCurrent250 { get; }
        bool ProtectionByTemperature { get; }
        bool TransistorsPowerVoltageTooHighOrToLow { get; }
        bool ShortCircuitOfTheBottomTransistors { get; }
        bool TooHighInputVoltage { get; }
        bool OverloadByCurrent150 { get; }
    }
}
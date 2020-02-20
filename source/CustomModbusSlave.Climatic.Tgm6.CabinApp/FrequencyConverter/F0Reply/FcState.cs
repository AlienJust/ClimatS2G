namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.FrequencyConverter.F0Reply
{
    public class FcState : IFcState
    {
        public FcState(bool overloadByCurrent250, bool protectionByTemperature, bool transistorsPowerVoltageTooHighOrToLow, bool shortCircuitOfTheBottomTransistors, bool tooHighInputVoltage, bool overloadByCurrent150)
        {
            OverloadByCurrent250 = overloadByCurrent250;
            ProtectionByTemperature = protectionByTemperature;
            TransistorsPowerVoltageTooHighOrToLow = transistorsPowerVoltageTooHighOrToLow;
            ShortCircuitOfTheBottomTransistors = shortCircuitOfTheBottomTransistors;
            TooHighInputVoltage = tooHighInputVoltage;
            OverloadByCurrent150 = overloadByCurrent150;
        }

        public bool OverloadByCurrent250 { get; }
        public bool ProtectionByTemperature { get; }
        public bool TransistorsPowerVoltageTooHighOrToLow { get; }
        public bool ShortCircuitOfTheBottomTransistors { get; }
        public bool TooHighInputVoltage { get; }
        public bool OverloadByCurrent150 { get; }
    }
}
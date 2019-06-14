namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.Reply03
{
    public interface IMukFanVaporizerDataReply03Diagnostic2
    {
        bool PhaseErrorOrPowerLost { get; }
        bool ReserveBit1 { get; }
        bool PowerSuplyOverheat { get; }
        bool ReserveBit3 { get; }
        bool FaultAttribute { get; }
        bool MotorOverheat { get; }
        bool HallSensorError { get; }
        bool MotorLock { get; }

        bool DcCurrentOverload { get; }
        bool ControllerOverheat { get; }
        bool DriverError { get; }
        bool DcOvervoltage { get; }
        bool DcLowVoltage { get; }
        bool LowVoltage { get; }
        bool ReserveBit14 { get; }
        bool Lock { get; }
    }
}
using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.Reply03
{
    internal class MukFanVaporizerDataReply03Diagnostic2Builder : IBuilder<IMukFanVaporizerDataReply03Diagnostic2>
    {
        private readonly byte _byteLow;
        private readonly byte _byteHigh;

        public MukFanVaporizerDataReply03Diagnostic2Builder(byte byteLow, byte byteHigh)
        {
            _byteLow = byteLow;
            _byteHigh = byteHigh;
        }

        public IMukFanVaporizerDataReply03Diagnostic2 Build()
        {
            return new MukFanVaporizerDataReply03Diagnostic2Simple
            {
                PhaseErrorOrPowerLost = _byteLow.GetBit(0),
                ReserveBit1 = _byteLow.GetBit(1),
                PowerSuplyOverheat = _byteLow.GetBit(2),
                ReserveBit3 = _byteLow.GetBit(3),
                FaultAttribute = _byteLow.GetBit(4),
                MotorOverheat = _byteLow.GetBit(5),
                HallSensorError = _byteLow.GetBit(6),
                MotorLock = _byteLow.GetBit(7),

                DcCurrentOverload = _byteHigh.GetBit(0),
                ControllerOverheat = _byteHigh.GetBit(1),
                DriverError = _byteHigh.GetBit(2),
                DcOvervoltage = _byteHigh.GetBit(3),
                DcLowVoltage = _byteHigh.GetBit(4),
                LowVoltage = _byteHigh.GetBit(5),
                ReserveBit14 = _byteHigh.GetBit(6),
                Lock = _byteHigh.GetBit(7)
            };
        }
    }
}
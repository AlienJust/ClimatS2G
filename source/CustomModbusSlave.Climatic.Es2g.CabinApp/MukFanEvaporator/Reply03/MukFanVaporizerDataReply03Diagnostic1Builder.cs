using AlienJust.Support.Numeric.Bits;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03
{
    internal class MukFanVaporizerDataReply03Diagnostic1Builder : IBuilder<IMukFanVaporizerDataReply03Diagnostic1>
    {
        private readonly byte _b;

        public MukFanVaporizerDataReply03Diagnostic1Builder(byte b)
        {
            _b = b;
        }

        public IMukFanVaporizerDataReply03Diagnostic1 Build()
        {
            return new MukFanVaporizerDataReply03Diagnostic1Simple
            {
                FanControllerLinkLost = _b.GetBit(4),
                FanErrorByDiscreeteInput = _b.GetBit(5),
                ErrorOneWireSensor1 = _b.GetBit(6),
                ErrorOneWireSensor2 = _b.GetBit(7)
            };
        }
    }
}
﻿using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.Reply03
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
                FanErrorByDiscreteInput = _b.GetBit(5),
                ErrorOneWireSensor1 = _b.GetBit(6),
                ErrorOneWireSensor2 = _b.GetBit(7)
            };
        }
    }
}
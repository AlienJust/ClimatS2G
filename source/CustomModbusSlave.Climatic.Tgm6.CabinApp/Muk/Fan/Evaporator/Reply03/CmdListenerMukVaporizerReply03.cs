using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.Reply03
{
    public class CmdListenerMukVaporizerReply03 : CmdListenerBase<IMukFanVaporizerDataReply03>
    {
        public CmdListenerMukVaporizerReply03(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) { }

        public override IMukFanVaporizerDataReply03 BuildData(byte[] bytes)
        {
            return new MukFanVaporizerDataReply03Builder(bytes).Build();
        }
    }
}
using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.Request16
{
    public class CmdListenerMukVaporizerRequest16 : CmdListenerBase<IMukFanVaporizerDataRequest16>
    {
        public CmdListenerMukVaporizerRequest16(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) { }

        public override IMukFanVaporizerDataRequest16 BuildData(byte[] bytes)
        {
            return new MukFanVaporizerDataRequest16Builder(bytes).Build();
        }
    }
}
using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.FrequencyConverter.F0Request
{
    public class CmdListenerFcF0Request : CmdListenerBase<IRequestF0>
    {
        public CmdListenerFcF0Request(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) { }

        public override IRequestF0 BuildData(byte[] bytes)
        {
            return new RequestF0Builder(bytes).Build();
        }
    }
}
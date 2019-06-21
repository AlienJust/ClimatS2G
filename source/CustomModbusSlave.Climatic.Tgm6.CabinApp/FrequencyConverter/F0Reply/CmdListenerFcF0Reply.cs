using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.FrequencyConverter.F0Reply
{
    public class CmdListenerFcF0Reply : CmdListenerBase<IReplyF0>
    {
        public CmdListenerFcF0Reply(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) { }

        public override IReplyF0 BuildData(byte[] bytes)
        {
            return new ReplyF0Builder(bytes).Build();
        }
    }
}
using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Reply32
{
    internal sealed class CmdListenerBsSmReply32 : CmdListenerBase<IBsSmReply32Data>
    {
        public CmdListenerBsSmReply32(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) { }

        public override IBsSmReply32Data BuildData(IList<byte> bytes)
        {
            return new BsSmReply32BuilderFromReplyDataBytes(bytes).Build();
        }
    }
}
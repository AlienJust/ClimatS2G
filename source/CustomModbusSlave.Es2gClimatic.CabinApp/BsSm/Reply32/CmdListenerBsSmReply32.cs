using System.Collections.Generic;
using System.Linq;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Reply32 {
	internal sealed class CmdListenerBsSmRequest32 : CmdListenerBase<IBsSmReply32Data> {
		public CmdListenerBsSmRequest32(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IBsSmReply32Data BuildData(IList<byte> bytes) {
			return new BsSmReply32BuilderFromReplyDataBytes(bytes.Skip(2).Take(bytes.Count - 4).ToList()).Build();
		}
	}
}

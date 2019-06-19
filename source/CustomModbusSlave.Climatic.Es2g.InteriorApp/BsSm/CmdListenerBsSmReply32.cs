using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Build;
using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm {
	class CmdListenerBsSmReply32 : CmdListenerBase<IBsSmAndKsm1DataCommand32Reply> {
		public CmdListenerBsSmReply32(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IBsSmAndKsm1DataCommand32Reply BuildData(IList<byte> bytes) {
			return new BsSmAndKsm1DataCommand32ReplyBuilderFromCommandPartDataBytes(bytes).Build();
		}
	}
}

using System.Collections.Generic;
using System.Linq;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Request32 {
	internal sealed class CmdListenerBsSmRequest32 : CmdListenerBase<IBsSmRequest32Data> {
		public CmdListenerBsSmRequest32(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IBsSmRequest32Data BuildData(IList<byte> bytes) {
			return new BsSmRequest32DataBuilderFromCommandPartDataBytes(bytes.Skip(2).Take(bytes.Count - 4).ToList()).Build();
		}
	}
}

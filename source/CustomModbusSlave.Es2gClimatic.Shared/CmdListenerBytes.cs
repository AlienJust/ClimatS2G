using System.Collections.Generic;
using System.Linq;

namespace CustomModbusSlave.Es2gClimatic.Shared {
	public sealed class CmdListenerBytes : CmdListenerBase<IList<byte>> {
		public CmdListenerBytes(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IList<byte> BuildData(IList<byte> bytes) {
			return bytes.Skip(2).Take(bytes.Count - 4).ToList();
		}
	}
}
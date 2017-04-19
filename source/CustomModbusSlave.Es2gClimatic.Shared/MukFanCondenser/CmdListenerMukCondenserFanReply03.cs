using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFridge {
	public class CmdListenerMukCondenserFanReply03 : CmdListenerBase<IMukCondensorFanReply03Data> {
		public CmdListenerMukCondenserFanReply03(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukCondensorFanReply03Data BuildData(IList<byte> bytes) {
			return new MukFridgeFanReply03DataBuilder(bytes).Build();
		}
	}
}

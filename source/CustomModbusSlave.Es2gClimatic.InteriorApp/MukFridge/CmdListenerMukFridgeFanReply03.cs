using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFridge {
	internal class CmdListenerMukFridgeFanReply03 : CmdListenerBase<IMukFridgeFanReply03Data> {
		public CmdListenerMukFridgeFanReply03(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukFridgeFanReply03Data BuildData(IList<byte> bytes) {
			return new MukFridgeFanReply03DataBuilder(bytes).Build();
		}
	}
}

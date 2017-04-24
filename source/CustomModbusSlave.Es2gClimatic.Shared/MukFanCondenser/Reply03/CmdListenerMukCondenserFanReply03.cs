using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser.Reply03;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFridge {
	public class CmdListenerMukCondenserFanReply03 : CmdListenerBase<IMukCondensorFanReply03Data> {
		public CmdListenerMukCondenserFanReply03(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukCondensorFanReply03Data BuildData(IList<byte> bytes) {
			return new MukFanCondenserReply03DataBuilder(bytes).Build();
		}
	}
}

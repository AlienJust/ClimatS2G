using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03 {
	internal sealed class CmdListenerMukWarmFloorReply03 : CmdListenerBase<IMukWarmFloorReply03Data> {
		public CmdListenerMukWarmFloorReply03(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukWarmFloorReply03Data BuildData(byte[] bytes) {
			return new MukWarmFloorReply03DataBuilder(bytes).Build();
		}
	}
}

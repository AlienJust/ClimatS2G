using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Request16 {
	internal sealed class CmdListenerMukWarmFloorRequest16 : CmdListenerBase<IMukWarmFloorRequest16Data> {
		public CmdListenerMukWarmFloorRequest16(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukWarmFloorRequest16Data BuildData(byte[] bytes) {
			return new MukWarmFloorRequest16BuilderFromBytes(bytes).Build();
		}
	}
}

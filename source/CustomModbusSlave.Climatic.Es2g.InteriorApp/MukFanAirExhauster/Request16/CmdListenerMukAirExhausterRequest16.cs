using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Request16 {
	internal class CmdListenerMukAirExhausterRequest16 : CmdListenerBase<IMukFanAirExhausterRequest16Data> {
		public CmdListenerMukAirExhausterRequest16(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukFanAirExhausterRequest16Data BuildData(byte[] bytes) {
			return new MukFanAirExhausterRequest16DataBuilder(bytes).Build();
		}
	}
}

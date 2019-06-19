using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Request16 {
	class CmdListenerMukFlapAirRecycleRequest16 : CmdListenerBase<IMukFlapAirRecycleRequest16Data> {
		public CmdListenerMukFlapAirRecycleRequest16(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukFlapAirRecycleRequest16Data BuildData(IList<byte> bytes) {
			return new MukFlapAirRecycleRequest16DataBuilder(bytes).Build();
		}
	}
}

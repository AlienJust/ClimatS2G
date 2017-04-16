using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Request16 {
	internal class CmdListenerMukFlapOuterAirRequest16 : CmdListenerBase<IMukFlapOuterAirRequest16Data> {
		public CmdListenerMukFlapOuterAirRequest16(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukFlapOuterAirRequest16Data BuildData(IList<byte> bytes) {
			return new MukFlapOuterAirRequest16DataBuilder(bytes).Build();
		}
	}
}

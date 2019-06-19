using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Request16 {
	internal class CmdListenerMukFlapAirRequest16 : CmdListenerBase<IMukFlapAirRequest16Data> {
		public CmdListenerMukFlapAirRequest16(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukFlapAirRequest16Data BuildData(IList<byte> bytes) {
			return new MukFlapAirRequest16DataBuilder(bytes).Build();
		}
	}
}

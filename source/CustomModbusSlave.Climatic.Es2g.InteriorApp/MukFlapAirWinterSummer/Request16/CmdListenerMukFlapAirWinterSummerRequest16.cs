using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirWinterSummer.Request16 {
	class CmdListenerMukFlapAirWinterSummerRequest16 : CmdListenerBase<IMukFlapAirWinterSummerRequest16Data> {
		public CmdListenerMukFlapAirWinterSummerRequest16(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukFlapAirWinterSummerRequest16Data BuildData(byte[] bytes) {
			return new MukFlapAirWinterSummerRequest16DataBuilder(bytes).Build();
		}
	}
}
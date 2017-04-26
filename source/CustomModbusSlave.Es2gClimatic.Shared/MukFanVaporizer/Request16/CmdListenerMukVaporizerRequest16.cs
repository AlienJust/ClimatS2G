using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Request16 {
	public class CmdListenerMukVaporizerRequest16 : CmdListenerBase<IMukVaporizerRequest16InteriorData> {
		public CmdListenerMukVaporizerRequest16(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukVaporizerRequest16InteriorData BuildData(IList<byte> bytes) {
			return new Request16DataBuilder(bytes).Build();
		}
	}
}

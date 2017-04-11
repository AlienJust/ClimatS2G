using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukVaporizer.Request16 {
	public class CmdListenerMukVaporizerRequest16 : CmdListenerBase<IRequest16Data> {
		public CmdListenerMukVaporizerRequest16(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IRequest16Data BuildData(IList<byte> bytes) {
			return new Request16DataBuilder(bytes).Build();
		}
	}

	// IMukVaporizerReply16InteriorData

	public class CmdListenerKsm60Params : CmdListenerBase<IRequest16Data> {
		public CmdListenerKsm60Params(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IRequest16Data BuildData(IList<byte> bytes) {
			return new Request16DataBuilder(bytes).Build();
		}
	}
}

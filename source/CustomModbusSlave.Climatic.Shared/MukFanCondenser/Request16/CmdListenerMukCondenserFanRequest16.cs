using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser.Request16 {
	public class CmdListenerMukCondenserFanRequest16 : CmdListenerBase<IMukCondenserRequest16Data> {
		public CmdListenerMukCondenserFanRequest16(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukCondenserRequest16Data BuildData(IList<byte> bytes) {
			return new MukCondenserRequest16DataBuilder(bytes).Build();
		}
	}
}

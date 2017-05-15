using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03 {
	public class CmdListenerMukVaporizerReply03 : CmdListenerBase<IMukFanVaporizerDataReply03> {
		public CmdListenerMukVaporizerReply03(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukFanVaporizerDataReply03 BuildData(IList<byte> bytes) {
			return new MukFanVaporizerDataReply03Builder(bytes).Build();
		}
	}
}

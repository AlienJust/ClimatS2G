using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Reply03 {
	public class CmdListenerMukVaporizerReply03 : CmdListenerBase<IMukVaporizerFanReply03Telemetry> {
		public CmdListenerMukVaporizerReply03(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukVaporizerFanReply03Telemetry BuildData(IList<byte> bytes) {
			return new MukVaporizerFanReply03TelemetryBuilder(bytes).Build();
		}
	}
}

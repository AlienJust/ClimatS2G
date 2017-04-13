using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukVaporizerFan {
	internal class CmdListenerMukVaporizerReply03 : CmdListenerBase<IMukVaporizerFanReply03Telemetry> {
		public CmdListenerMukVaporizerReply03(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukVaporizerFanReply03Telemetry BuildData(IList<byte> bytes) {
			return new MukVaporizerFanReply03TelemetryBuilder(bytes).Build();
		}
	}
}

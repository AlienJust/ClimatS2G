using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared.Bvs {
	public class CmdListenerBvsReply65 : CmdListenerBase<IBvsReply65Telemetry> {
		public CmdListenerBvsReply65(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IBvsReply65Telemetry BuildData(IList<byte> bytes) {
			return new BvsReply65TelemetryBuilder(bytes).Build();
		}
	}
}

using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Build;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03 {
	internal class CmdListenerMukFlapOuterAirReply03 : CmdListenerBase<IMukFlapReply03Telemetry> {
		public CmdListenerMukFlapOuterAirReply03(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukFlapReply03Telemetry BuildData(IList<byte> bytes) {
			return new MukFlapReply03TelemetryBuilder(bytes).Build();
		}
	}
}

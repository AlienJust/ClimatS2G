using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03 {
	internal class CmdListenerMukFlapOuterAirReply03 : CmdListenerBase<IMukFlapOuterAirReply03Telemetry> {
		public CmdListenerMukFlapOuterAirReply03(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukFlapOuterAirReply03Telemetry BuildData(byte[] bytes) {
			return new MukFlapReply03TelemetryBuilder(bytes).Build();
		}
	}
}

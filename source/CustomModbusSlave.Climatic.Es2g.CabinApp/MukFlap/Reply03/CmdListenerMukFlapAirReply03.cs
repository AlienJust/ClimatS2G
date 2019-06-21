using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Build;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03 {
	internal class CmdListenerMukFlapAirReply03 : CmdListenerBase<IMukFlapAirReply03Telemetry> {
		public CmdListenerMukFlapAirReply03(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukFlapAirReply03Telemetry BuildData(byte[] bytes) {
			return new MukFlapReply03TelemetryBuilder(bytes).Build();
		}
	}
}

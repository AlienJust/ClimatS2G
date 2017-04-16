using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapReturnAir.DataModel.Builders;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapReturnAir.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapReturnAir {
	internal class CmdListenerMukFlapReturnAirReply03 : CmdListenerBase<IMukFlapReturnAirReply03Telemetry> {
		public CmdListenerMukFlapReturnAirReply03(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukFlapReturnAirReply03Telemetry BuildData(IList<byte> bytes) {
			return new MukFlapReturnAirReply03TelemetryBuilder(bytes).Build();
		}
	}
}

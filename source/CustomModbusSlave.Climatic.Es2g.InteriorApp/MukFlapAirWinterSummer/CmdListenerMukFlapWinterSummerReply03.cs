using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirWinterSummer.DataModel.Builders;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirWinterSummer.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirWinterSummer {
	internal class CmdListenerMukFlapWinterSummerReply03 : CmdListenerBase<IMukFlapWinterSummerReply03Telemetry> {
		public CmdListenerMukFlapWinterSummerReply03(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukFlapWinterSummerReply03Telemetry BuildData(byte[] bytes) {
			return new MukFlapWinterSummerReply03TelemetryBuilder(bytes).Build();
		}
	}
}

﻿using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer.DataModel.Builders;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer {
	internal class CmdListenerMukFlapWinterSummerReply03 : CmdListenerBase<IMukFlapWinterSummerReply03Telemetry> {
		public CmdListenerMukFlapWinterSummerReply03(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukFlapWinterSummerReply03Telemetry BuildData(IList<byte> bytes) {
			return new MukFlapWinterSummerReply03TelemetryBuilder(bytes).Build();
		}
	}
}
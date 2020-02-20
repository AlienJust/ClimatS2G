using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Data;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Data.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster {
	internal class CmdListenerMukAirExhausterReply03 : CmdListenerBase<IMukAirExhausterReply03Data> {
		public CmdListenerMukAirExhausterReply03(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukAirExhausterReply03Data BuildData(byte[] bytes) {
			return new MukAirExhausterReply03DataBuilder(bytes).Build();
		}
	}
}

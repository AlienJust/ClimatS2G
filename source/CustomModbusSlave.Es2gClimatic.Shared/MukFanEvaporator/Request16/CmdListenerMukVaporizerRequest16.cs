﻿using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Request16 {
	public class CmdListenerMukVaporizerRequest16 : CmdListenerBase<IMukFanVaporizerDataRequest16> {
		public CmdListenerMukVaporizerRequest16(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukFanVaporizerDataRequest16 BuildData(IList<byte> bytes) {
			return new MukFanVaporizerDataRequest16Builder(bytes).Build();
		}
	}
}
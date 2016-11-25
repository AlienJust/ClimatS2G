﻿using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm {
	class BsSmDataCommand32ReplyBuilderFromReplyDataBytes : Shared.IBuilder<IBsSmDataCommand32Reply> {
		private readonly IList<byte> _replyBytes;
		public BsSmDataCommand32ReplyBuilderFromReplyDataBytes(IList<byte> replyBytes) {
			_replyBytes = replyBytes;
		}

		public IBsSmDataCommand32Reply Build() {
			int targetTemperatureInsideTheCabin = (_replyBytes[3] & 0x0F) - 2;
			int fanSpeedLevel = _replyBytes[3] & 0x18;
			bool isWarmFloorOn = (_replyBytes[3] & 0x20) == 0x20;
			uint astronomicTime = (uint)(_replyBytes[4] + _replyBytes[5] * 256 + _replyBytes[6] * 65536 + _replyBytes[7] * 16777216); // TODO: improve converting
			uint delayedStartTime = (uint)(_replyBytes[8] + _replyBytes[9] * 256 + _replyBytes[10] * 65536 + _replyBytes[11] * 16777216); // TODO: improve converting

			int temperatureOutdoor = _replyBytes[12];
			int temperatureIndoor = _replyBytes[13] & 0x07;
			var climatMode = new Shared.BsSm.ClimaticSystemWorkModeBuilderFromInt((_replyBytes[13] & 0xF8) >> 3).Build();
			var wm = new Shared.BsSm.WorkModeReplyBuilderFromByte(_replyBytes[14]).Build();
			// TODO: byte 13 and 14
			Shared.BsSm.State.IContract bsSmState = new Shared.BsSm.State.BuilderFromByte(_replyBytes[15]).Build();
			int bsSmVersionNumber = _replyBytes[16];

			return new BsSmDataCommand32ReplySimple(
				targetTemperatureInsideTheCabin,
				fanSpeedLevel,
				isWarmFloorOn,
				astronomicTime,
				delayedStartTime,
				temperatureOutdoor,
				temperatureIndoor,
				climatMode,
				wm,
				bsSmState,
				bsSmVersionNumber
				);
		}
	}
}
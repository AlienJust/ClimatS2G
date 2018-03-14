using System;
using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Reply32 {
	class BsSmReply32BuilderFromReplyDataBytes : Shared.IBuilder<IBsSmReply32Data> {
		private readonly IList<byte> _replyBytes;
		public BsSmReply32BuilderFromReplyDataBytes(IList<byte> replyBytes) {
			_replyBytes = replyBytes;
		}

		public IBsSmReply32Data Build() {
			int targetTemperatureInsideTheCabin = (_replyBytes[3] & 0x0F) - 2;
			int fanSpeedLevel = _replyBytes[3] & 0x18;
			bool isWarmFloorOn = (_replyBytes[3] & 0x20) == 0x20;
			uint astronomicTime = (uint)(_replyBytes[4] + _replyBytes[5] * 256 + _replyBytes[6] * 65536 + _replyBytes[7] * 16777216); // TODO: improve converting
			uint delayedStartTime = (uint)(_replyBytes[8] + _replyBytes[9] * 256 + _replyBytes[10] * 65536 + _replyBytes[11] * 16777216); // TODO: improve converting

			int temperatureOutdoor = _replyBytes[12];
			int temperatureIndoor = _replyBytes[13] & 0x0F;
			var climatMode = new Shared.BsSm.ClimaticSystemWorkModeBuilderFromInt((_replyBytes[13] & 0xF0) >> 4).Build();
			var wm = new Shared.BsSm.WorkModeReplyBuilderFromByte(_replyBytes[14]).Build();
			Shared.BsSm.State.IBsSmState bsSmState = new Shared.BsSm.State.BuilderFromByte(_replyBytes[15]).Build();
			int bsSmVersionNumber = _replyBytes[16];

			return new BsSmReply32DataSimple(
				targetTemperatureInsideTheCabin,
				fanSpeedLevel,
				isWarmFloorOn,
				UnixTimeStampToDateTime(astronomicTime),
				delayedStartTime,
				temperatureOutdoor,
				temperatureIndoor,
				climatMode,
				wm,
				bsSmState,
				bsSmVersionNumber
				);
		}
		public static DateTime UnixTimeStampToDateTime(double unixTimeStamp) {
			// Unix timestamp is seconds past epoch
			DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dtDateTime;
		}
	}
}
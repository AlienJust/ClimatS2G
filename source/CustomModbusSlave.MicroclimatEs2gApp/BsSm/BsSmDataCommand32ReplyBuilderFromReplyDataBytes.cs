using System.Collections.Generic;
using CustomModbusSlave.MicroclimatEs2gApp.Common.BsSm.BsSmState;

namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	class BsSmDataCommand32ReplyBuilderFromReplyDataBytes : IBuilder<IBsSmDataCommand32Reply> {
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
			// TODO: byte 13 and 14
			IBsSmState bsSmState = new BsSmStateBuilderFromByte(_replyBytes[15]).Build();
			int bsSmVersionNumber = _replyBytes[16];

			return new BsSmDataCommand32ReplySimple(
				targetTemperatureInsideTheCabin,
				fanSpeedLevel,
				isWarmFloorOn,
				astronomicTime,
				delayedStartTime,
				temperatureOutdoor,
				bsSmState,
				bsSmVersionNumber
				);
		}
	}
}
using System.Collections.Generic;
using System.Linq;
using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.SimpleRelease;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.BsSm.State;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Build {
	class BsSmAndKsm1DataCommand32ReplyBuilderFromCommandPartDataBytes : IBuilder<IBsSmAndKsm1DataCommand32Reply> {
		private readonly IList<byte> _replyBytes;
		public BsSmAndKsm1DataCommand32ReplyBuilderFromCommandPartDataBytes(IList<byte> replyBytes) {
			_replyBytes = replyBytes;
		}

		public IBsSmAndKsm1DataCommand32Reply Build() {
			uint astronomicTime = (uint)(_replyBytes[3] + _replyBytes[4] * 256 + _replyBytes[5] * 65536 + _replyBytes[6] * 16777216); // TODO: improve converting
			uint delayedStartTime = (uint)(_replyBytes[7] + _replyBytes[8] * 256 + _replyBytes[9] * 65536 + _replyBytes[10] * 16777216); // TODO: improve converting

			int temperatureOutdoor = _replyBytes[11];
			int carType = _replyBytes[12] & 0x0F;
			int reserve13D4D7 = (_replyBytes[12] & 0xF0) >> 8;

			int targetTemperatureInsideTheCabin = (_replyBytes[13] & 0x0F) - 2;
			int climaticSystemWorkmode14D4D7 = (_replyBytes[13] & 0xF0) >> 8;

			var workModeAndCompressorSwitch = new WorkModeReplyBuilderFromByte(_replyBytes[14]).Build();

			int allowedPowerConsuptionBy380Vline = _replyBytes[15];

			int reserve17 = _replyBytes[16];
			int reserve18 = _replyBytes[17];

			var ksm2Request = new BsSmAndKsm1DataCommand32RequestBuilderFromCommandPartDataBytes(_replyBytes.Skip(15).ToList()).Build();

			IContract contract = new BuilderFromByte(_replyBytes[40]).Build();
			int bsSmVersionNumber = _replyBytes[41];

			int reserve43 = _replyBytes[42];
			int reserve44 = _replyBytes[43];
			int reserve45 = _replyBytes[44];

			return new BsSmAndKsm1DataCommand32ReplySimple(
				astronomicTime,
				delayedStartTime,
				temperatureOutdoor,
				carType,
				reserve13D4D7,
				targetTemperatureInsideTheCabin,
				climaticSystemWorkmode14D4D7,
				workModeAndCompressorSwitch,
				allowedPowerConsuptionBy380Vline,
				reserve17,
				reserve18,
				ksm2Request,
				contract,
				bsSmVersionNumber,
				reserve43,
				reserve44,
				reserve45
				);
		}
	}
}
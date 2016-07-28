using System.Collections.Generic;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFridge.Request16 {
	class Request16DataBuilder : IBuilder<IRequest16Data> {
		private readonly IList<byte> _bytes;
		public Request16DataBuilder(IList<byte> bytes) {
			_bytes = bytes;
		}

		public IRequest16Data Build() {
			return new Request16Data {
				CurrentKsmCommandWorkmode =
					new KsmCommandWorkmodeSimple {
						RegulatorIsWorking = (_bytes[8] & 0x01) == 0x01, // zb bit 0
						RegulatorIsWorkingParking = (_bytes[8] & 0x02) == 0x02, // zb bit 1
																														 // zb bit 2 skipped
						Washing = (_bytes[8] & 0x08) == 0x08, // zb bit 3

						FanSpeedIsMax = (_bytes[8] & 0x10) == 0x10, // zb bit 4
						FanIsOff = (_bytes[8] & 0x20) == 0x20, // zb bit 5
						ManualMode = (_bytes[8] & 0x40) == 0x40, // zb bit 6
						//ForceHeatModePwm50 = (_bytes[8] & 0x80) == 0x80 // zb bit 7
					},
				PressureSetting = _bytes[9] * 256 + _bytes[10], // word #1
				Reserve14 = _bytes[11] * 256 + _bytes[12] // word #2
																									// 13 & 14 is CRC
			};
		}
	}
}
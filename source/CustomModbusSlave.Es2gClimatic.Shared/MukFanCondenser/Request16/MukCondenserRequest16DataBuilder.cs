using System.Collections.Generic;
using AlienJust.Support.Numeric.Bits;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser.Request16 {
	class MukCondenserRequest16DataBuilder : IBuilder<IMukCondenserRequest16Data> {
		private readonly IList<byte> _bytes;
		public MukCondenserRequest16DataBuilder(IList<byte> bytes) {
			_bytes = bytes;
		}

		public IMukCondenserRequest16Data Build() {
			return new MukCondenserMukCondenserRequest16DataSimple {
				MukCondenserWorkmodeCommandFromKsm =
					new MukCondenserWorkmodeCommandFromKsmSimple {
						RegulatorIsWorking = (_bytes[8] & 0x01) == 0x01, // zb bit 0
						RegulatorIsWorkingParking = (_bytes[8] & 0x02) == 0x02, // zb bit 1
						Washing = (_bytes[8] & 0x08) == 0x08, // zb bit 3
						FanSpeedIsMax = (_bytes[8] & 0x10) == 0x10, // zb bit 4
						FanIsOff = (_bytes[8] & 0x20) == 0x20, // zb bit 5
						ManualMode = (_bytes[8] & 0x40) == 0x40, // zb bit 6
						ForceTurnOnSecondStageOnly = _bytes[8].GetBit(7)
					},
				PressureSetting = _bytes[9] * 256 + _bytes[10], // word #1
				Reserve14 = _bytes[11] * 256 + _bytes[12] // word #2
			};
		}
	}
}

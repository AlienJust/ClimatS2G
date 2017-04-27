using System.Collections.Generic;
using AlienJust.Support.Numeric.Bits;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Request16 {
	public class Request16DataBuilder : IBuilder<IMukFanVaporizerRequest16Data> {
		private readonly IList<byte> _bytes;
		public Request16DataBuilder(IList<byte> bytes) {
			_bytes = bytes;
		}

		public IMukFanVaporizerRequest16Data Build() {

			var deltaTSettingRaw = _bytes[17] * 256 + _bytes[18];
			return new Request16Data {
				CurrentKsmCommandWorkmode =
					new KsmCommandWorkmodeSimple {
						WorkConfiguration = _bytes[7] & 0x0F,
						ForceEmersonOffByLowPressure = _bytes[7] .GetBit(4),

						AutomaticMode = (_bytes[8] & 0x01) == 0x01, // zb bit 0
						ForceHeatRegulator = (_bytes[8] & 0x02) == 0x02, // zb bit 1, zb bit 2 skipped
						ForceHeatModePwm100 = (_bytes[8] & 0x08) == 0x08, // zb bit 3
						ForceCoolMode = (_bytes[8] & 0x10) == 0x10, // zb bit 4
						HeatModeAndCoolModeAreOff = (_bytes[8] & 0x20) == 0x20, // zb bit 5
						ManualMode = (_bytes[8] & 0x40) == 0x40, // zb bit 6
						ForceHeatModePwm50 = (_bytes[8] & 0x80) == 0x80 // zb bit 7
					},
				OuterTemperature = _bytes[9] * 256 + _bytes[10], // word #1
				InnerTemperature = (_bytes[11] * 256 + _bytes[12]) * .01, // word #2
				IsSlave = _bytes[13].GetBit(2),  // word #3, high byte, bit 2
				FanSpeed = _bytes[14], // word #3
				DeltaT = _bytes[15] * 256 + _bytes[16], // word #4, bit 01
				DeltaTSetting = deltaTSettingRaw == 25 || deltaTSettingRaw == 0 ? 0.0: (deltaTSettingRaw - 25) * 0.1// word #5, 19 & 20 bytes are CRC
			};
		}
	}
}
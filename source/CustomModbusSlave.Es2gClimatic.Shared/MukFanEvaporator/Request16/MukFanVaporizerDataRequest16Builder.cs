using System.Collections.Generic;
using AlienJust.Support.Numeric.Bits;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Request16 {
	internal class MukFanVaporizerDataRequest16Builder : IBuilder<IMukFanVaporizerDataRequest16> {
		private readonly IList<byte> _bytes;
		public MukFanVaporizerDataRequest16Builder(IList<byte> bytes) {
			_bytes = bytes;
		}

		public IMukFanVaporizerDataRequest16 Build() {
			var deltaTSettingRaw = _bytes[17] * 256 + _bytes[18];
			
			return new MukFanVaporizerDataRequest16Simple {
				CurrentKsmCommandWorkmode =
					new KsmCommandWorkmodeSimple {
						WorkConfiguration = _bytes[7] & 0x0F,
						ForceEmersonOffByLowPressure = _bytes[7] .GetBit(4),

						AutomaticMode = _bytes[8].GetBit(0), // zb bit 0
						ForceHeatRegulator = _bytes[8].GetBit(1),
						ForceHeatModePwm100 = _bytes[8].GetBit(2),
						ForceCoolMode = _bytes[8].GetBit(3),
						AutomaticModeWithoutSetControl = _bytes[8].GetBit(4),
						HeatModeAndCoolModeAreOff = _bytes[8].GetBit(5),
						ManualMode = _bytes[8].GetBit(6),
						ForceHeatModePwm50 = _bytes[8].GetBit(7)
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
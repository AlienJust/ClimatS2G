using System.Collections.Generic;
using AlienJust.Support.Collections;
using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Request16 {
	class MukFanAirExhausterRequest16DataBuilder: IBuilder<IMukFanAirExhausterRequest16Data> {
		private readonly IList<byte> _bytes;

		public MukFanAirExhausterRequest16DataBuilder(IList<byte> bytes) {
			_bytes = bytes;
		}
		public IMukFanAirExhausterRequest16Data Build() {
			return new MukFanAirExhausterRequest16DataSimple {
				MukFanAirExhausterWorkmodeCommandFromKsm = new MukFanAirExhausterWorkmodeCommandFromKsmSimple {
					RegulatorIsWorking = _bytes[8].GetBit(0)
				},
				TemperatureOuterAir = new BytesPair(_bytes[9], _bytes[10]).HighFirstSignedValue,
				AttributesRegistryByte = new AttributesRegistryByteSimple {SalonMaster = _bytes[12].GetBit(0)},
				FanLevelSetting = new BytesPair(_bytes[13], _bytes[14]).HighFirstSignedValue,
				Reserve517 = new BytesPair(_bytes[15], _bytes[16]).HighFirstUnsignedValue,
				Reserve518 = new BytesPair(_bytes[17], _bytes[18]).HighFirstUnsignedValue
			};
		}
	}
}

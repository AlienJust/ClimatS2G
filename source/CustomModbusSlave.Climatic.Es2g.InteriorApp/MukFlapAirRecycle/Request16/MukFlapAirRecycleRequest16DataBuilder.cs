using System.Collections.Generic;
using AlienJust.Support.Collections;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Request16 {
	class MukFlapAirRecycleRequest16DataBuilder : IBuilder<IMukFlapAirRecycleRequest16Data> {
		private readonly IList<byte> _bytes;

		public MukFlapAirRecycleRequest16DataBuilder(IList<byte> bytes) {
			_bytes = bytes;
		}

		public IMukFlapAirRecycleRequest16Data Build() {
			return new MukFlapAirRecycleRequest16DataSimple {
				MukFlapAirRecycleWorkmodeCommandFromKsm = new BytesPair(_bytes[7], _bytes[8]).HighFirstUnsignedValue,
				TemperatureOuterAir = new BytesPair(_bytes[9], _bytes[10]).HighFirstSignedValue,
				TemperatureInnerAir = new BytesPair(_bytes[11], _bytes[12]).HighFirstSignedValue * 0.01,
				FanLevelSetting = new BytesPair(_bytes[13], _bytes[14]).HighFirstSignedValue,
				ControlOfHiVoltageContactorRaw = new BytesPair(_bytes[15], _bytes[16]).HighFirstUnsignedValue,
				ControlOfHiVoltageContactorDescription = new ControlOfHiVoltageContactorBuilder(_bytes[16]).Build(),
				Reserve624 = new BytesPair(_bytes[17], _bytes[18]).HighFirstUnsignedValue
			};
		}
	}
}

using System.Collections.Generic;
using AlienJust.Support.Collections;
using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Request16 {
	class MukFlapAirRequest16DataBuilder : IBuilder<IMukFlapAirRequest16Data> {
		private readonly IList<byte> _bytes;
		public MukFlapAirRequest16DataBuilder(IList<byte> bytes) {
			_bytes = bytes;
		}

		public IMukFlapAirRequest16Data Build() {
			var bitsRegistry = new BytesPair(_bytes[15], _bytes[16]); // word #4
			return new Request16Data(new KsmCommandWorkmodeAndUnparsedValueSimple(_bytes[7] * 256 + _bytes[8]), // word #0 TODO: выводить только младший байт!?
				_bytes[9] * 256 + _bytes[10], // word #1
				(_bytes[11] * 256 + _bytes[12]) * 0.01, // word #2
				_bytes[13] * 256 + _bytes[14], // word #3
				bitsRegistry.HighFirstUnsignedValue, 
				bitsRegistry.HighFirstUnsignedValue.GetBit(0),
				_bytes[17] * 256 + _bytes[18] // word #5
				);
		}
	}
}

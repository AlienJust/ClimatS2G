using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Request16 {
	class Request16DataBuilder : IBuilder<IRequest16Data> {
		private readonly IList<byte> _bytes;
		public Request16DataBuilder(IList<byte> bytes) {
			_bytes = bytes;
		}

		public IRequest16Data Build() {
			return new Request16Data(
				new KsmCommandWorkmodeSimple(_bytes[7] * 256 + _bytes[8]), // word #0
				_bytes[9] * 256 + _bytes[10], // word #1
				_bytes[11] * 256 + _bytes[12], // word #2
				_bytes[13] * 256 + _bytes[14], // word #3
				
				(_bytes[16] & 0x01) == 0x01, // word #4, bit 01

				_bytes[17] * 256 + _bytes[18] // word #5
				// 19 & 20 is CRC
				);
		}
	}
}
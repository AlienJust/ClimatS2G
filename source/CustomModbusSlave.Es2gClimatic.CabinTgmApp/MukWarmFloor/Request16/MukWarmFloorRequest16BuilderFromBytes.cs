using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Request16 {
	class MukWarmFloorRequest16BuilderFromBytes : IBuilder<IMukWarmFloorRequest16Data> {
		private readonly IList<byte> _request;

		public MukWarmFloorRequest16BuilderFromBytes(IList<byte> request) {
			_request = request;
		}
		public IMukWarmFloorRequest16Data Build() {
			var result = new MukWarmFloorRequest16DataSimple {
				WorkModeReceivedFromKsm = _request[8],
				TemperatureOutside = _request[9] * 256 + _request[10],
				TemperatureInside = _request[11] * 256 + _request[12]
			};
			return result;
		}
	}
}

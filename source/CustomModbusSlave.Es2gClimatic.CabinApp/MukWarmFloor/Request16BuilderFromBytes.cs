using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor {
	class Request16BuilderFromBytes : IBuilder<IRequest16> {
		private readonly IList<byte> _request;

		public Request16BuilderFromBytes(IList<byte> request) {
			_request = request;
		}
		public IRequest16 Build() {
			//TODO: Parse request
			var result = new Request16Simple {
				WorkModeReceivedFromKsm = _request[8],
				TemperatureOutside = _request[9] * 256 + _request[10],
				TemperatureInside = _request[11] * 256 + _request[12]
			};
			return result;
		}
	}
}
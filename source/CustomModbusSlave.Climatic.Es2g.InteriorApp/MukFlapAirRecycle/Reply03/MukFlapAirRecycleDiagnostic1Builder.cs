using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03 {
	class MukFlapAirRecycleDiagnostic1Builder : IBuilder<IMukFlapAirRecycleDiagnostic1> {
		private readonly int _absoluteValue;
		public MukFlapAirRecycleDiagnostic1Builder(int absoluteValue) {
			_absoluteValue = absoluteValue;
		}

		public IMukFlapAirRecycleDiagnostic1 Build() {
			return new MukFlapAirRecycleDiagnostic1 {
				HighVoltageKeyDriverLinkError = _absoluteValue.GetBit(4),
				SensorOneWire1Error = _absoluteValue.GetBit(6),
				SensorOneWire2Error = _absoluteValue.GetBit(7)
			};
		}
	}
}

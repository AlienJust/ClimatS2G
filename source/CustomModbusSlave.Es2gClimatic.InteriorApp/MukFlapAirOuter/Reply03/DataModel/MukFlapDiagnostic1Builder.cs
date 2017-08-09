using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.SimpleRelease;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Build {
	class MukFlapDiagnostic1Builder : IBuilder<IMukFlapDiagnostic1> {
		private readonly int _absoluteValue;
		public MukFlapDiagnostic1Builder(int absoluteValue) {
			_absoluteValue = absoluteValue;
		}

		public IMukFlapDiagnostic1 Build() {
			return new MukFlapDiagnostic1 {
				NoEmersionControllerAnswer = _absoluteValue.GetBit(4),
				SensorOneWire1Error = _absoluteValue.GetBit(6),
				SensorOneWire2Error = _absoluteValue.GetBit(7)
			};
		}
	}
}
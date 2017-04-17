using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.SimpleRelease;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Build {
	class MukFlapOuterAirDiagnostic2Builder : IBuilder<IMukFlapOuterAirDiagnostic2> {
		private readonly int _absoluteValue;
		public MukFlapOuterAirDiagnostic2Builder(int absoluteValue) {
			_absoluteValue = absoluteValue;
		}

		public IMukFlapOuterAirDiagnostic2 Build() {
			return new MukFlapOuterAirOuterAirDiagnostic2(
				(_absoluteValue & 0x01) == 0x01, // zb bit 0
				(_absoluteValue & 0x02) == 0x02, // zb bit 1
				(_absoluteValue & 0x04) == 0x04, // zb bit 2
				(_absoluteValue & 0x08) == 0x08, // zb bit 3
				_absoluteValue.GetBit(4),
				(_absoluteValue & 0x20) == 0x20, // zb bit 5
				(_absoluteValue & 0x40) == 0x40 // zb bit 6
				);
		}
	}
}
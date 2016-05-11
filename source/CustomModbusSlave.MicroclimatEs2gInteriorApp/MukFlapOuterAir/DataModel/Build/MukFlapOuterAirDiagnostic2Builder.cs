using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Build {
	class MukFlapOuterAirDiagnostic2Builder : IBuilder<IMukFlapDiagnostic2> {
		private readonly int _absoluteValue;
		public MukFlapOuterAirDiagnostic2Builder(int absoluteValue) {
			_absoluteValue = absoluteValue;
		}

		public IMukFlapDiagnostic2 Build() {
			return new MukFlapOuterAirDiagnostic2(
				(_absoluteValue & 0x01) == 0x01, // zb bit 0
				(_absoluteValue & 0x02) == 0x02, // zb bit 1
				(_absoluteValue & 0x04) == 0x04, // zb bit 2
				(_absoluteValue & 0x08) == 0x08, // zb bit 3

				(_absoluteValue & 0x20) == 0x20, // zb bit 5
				(_absoluteValue & 0x40) == 0x40 // zb bit 6
				);
		}
	}
}
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Build {
	class MukFlapDiagnostic2Builder : IBuilder<IMukFlapDiagnostic2> {
		private readonly int _absoluteValue;
		public MukFlapDiagnostic2Builder(int absoluteValue) {
			_absoluteValue = absoluteValue;
		}

		public IMukFlapDiagnostic2 Build() {
			return new MukFlapDiagnostic2(
				(_absoluteValue & 0x40) == 0x20, // zb bit 5
				(_absoluteValue & 0x40) == 0x40 // zb bit 6
				);
		}
	}
}
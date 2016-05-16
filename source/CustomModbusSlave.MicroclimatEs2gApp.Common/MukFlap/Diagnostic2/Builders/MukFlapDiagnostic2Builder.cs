using CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DataModel.SimpleReleases;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DataModel.Builders {
	public class MukFlapDiagnostic2Builder : IBuilder<IMukFlapDiagnostic2> {
		private readonly int _absoluteValue;
		public MukFlapDiagnostic2Builder(int absoluteValue) {
			_absoluteValue = absoluteValue;
		}

		public IMukFlapDiagnostic2 Build() {
			return new MukFlapDiagnostic2(
				(_absoluteValue & 0x20) == 0x20, // zb bit 5
				(_absoluteValue & 0x40) == 0x40 // zb bit 6
				);
		}
	}
}
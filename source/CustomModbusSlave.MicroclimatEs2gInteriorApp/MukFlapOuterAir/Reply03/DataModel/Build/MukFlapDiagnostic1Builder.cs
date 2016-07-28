using CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.Reply03.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.Reply03.DataModel.SimpleRelease;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.Reply03.DataModel.Build {
	class MukFlapDiagnostic1Builder : IBuilder<IMukFlapDiagnostic1> {
		private readonly int _absoluteValue;
		public MukFlapDiagnostic1Builder(int absoluteValue) {
			_absoluteValue = absoluteValue;
		}

		public IMukFlapDiagnostic1 Build() {
			return new MukFlapDiagnostic1(
				(_absoluteValue & 0x40) == 0x10,
				(_absoluteValue & 0x40) == 0x40, 
				(_absoluteValue & 0x80) == 0x80
				);
		}
	}
}
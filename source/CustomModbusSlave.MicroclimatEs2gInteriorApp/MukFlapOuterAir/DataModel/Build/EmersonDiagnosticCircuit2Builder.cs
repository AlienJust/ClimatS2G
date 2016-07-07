using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.DataModel.SimpleRelease;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Build {
	class EmersonDiagnosticCircuit2Builder : IBuilder<IEmersonDiagnosticCircuit2> {
		private readonly int _absoluteValue;
		public EmersonDiagnosticCircuit2Builder(int absoluteValue) {
			_absoluteValue = absoluteValue;
		}

		public IEmersonDiagnosticCircuit2 Build() {
			return new EmersonDiagnosticCircuit2(
				(_absoluteValue & 0x01) == 0x01,
				(_absoluteValue & 0x02) == 0x02,
				(_absoluteValue & 0x04) == 0x04,
				(_absoluteValue & 0x08) == 0x08,
				(_absoluteValue & 0x10) == 0x10
				);
		}
	}
}
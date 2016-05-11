using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Build {
	class EmersonDiagnosticCircuit1Builder : IBuilder<IEmersonDiagnosticCircuit1> {
		private readonly int _absoluteValue;
		public EmersonDiagnosticCircuit1Builder(int absoluteValue) {
			_absoluteValue = absoluteValue;
		}

		public IEmersonDiagnosticCircuit1 Build() {
			return new EmersonDiagnosticCircuit1(
				(_absoluteValue & 0x01) == 0x01,
				(_absoluteValue & 0x02) == 0x02,
				(_absoluteValue & 0x04) == 0x04,
				(_absoluteValue & 0x08) == 0x08,
				(_absoluteValue & 0x10) == 0x10,
				(_absoluteValue & 0x20) == 0x20,
				(_absoluteValue & 0x40) == 0x40
				);
		}
	}
}
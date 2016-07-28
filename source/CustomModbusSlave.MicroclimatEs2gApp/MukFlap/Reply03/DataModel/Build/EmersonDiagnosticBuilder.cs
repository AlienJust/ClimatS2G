using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Reply03.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Reply03.DataModel.Build {
	class EmersonDiagnosticBuilder : IBuilder<IEmersonDiagnostic> {
		private readonly int _absoluteValue;
		public EmersonDiagnosticBuilder(int absoluteValue) {
			_absoluteValue = absoluteValue;
		}

		public IEmersonDiagnostic Build() {
			return new EmersonDiagnostic(
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
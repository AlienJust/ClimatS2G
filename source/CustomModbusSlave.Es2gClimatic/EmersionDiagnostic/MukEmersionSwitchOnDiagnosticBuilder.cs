using CustomModbusSlave.Es2gClimatic;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.EmersionDiagnostic {
	public class MukEmersionSwitchOnDiagnosticBuilder : IBuilder<IMukEmersionSwitchOnDiagnostic> {
		private readonly int _absoluteValue;
		public MukEmersionSwitchOnDiagnosticBuilder(int absoluteValue) {
			_absoluteValue = absoluteValue;
		}

		public IMukEmersionSwitchOnDiagnostic Build() {
			return new MukEmersionSwitchOnDiagnosticSimple(
				(_absoluteValue & 0x01) == 0x01, // zb bit 0
				(_absoluteValue & 0x02) == 0x02, // zb bit 1
				(_absoluteValue & 0x04) == 0x04 // zb bit 2
				);
		}
	}
}
namespace CustomModbusSlave.Es2gClimatic.Shared.OneWire.Diagnostic {
	public class MukFlapDiagnosticOneWireSensorBuilder : IBuilder<IMukFlapDiagnosticOneWireSensor> {
		private readonly int _data;
		public MukFlapDiagnosticOneWireSensorBuilder(int data) {
			_data = data;
		}

		public IMukFlapDiagnosticOneWireSensor Build() {
			return new MukFlapDiagnosticOneWireSensor(
				(_data & 0x80) == 0x80, // zbbit 7
				new OneWireSensorErrorCodeSimple(_data & 0xFF7F), // error code
				(_data & 0xFF7F) < 5 ? (int?)(_data & 0xFF7F) : null // errors count
				);
		}
	}
}

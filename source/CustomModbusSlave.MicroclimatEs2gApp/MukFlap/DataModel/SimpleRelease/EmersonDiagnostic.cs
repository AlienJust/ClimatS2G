using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease {
	class EmersonDiagnostic : IEmersonDiagnostic {
		public bool ErrorValve1 { get; private set; }
		public bool ErrorValve2 { get; private set; }
		public bool ErrorPressureSensor1 { get; private set; }
		public bool ErrorPressureSensor2 { get; private set; }
		public bool ErrorTemperatureSensor1 { get; private set; }
		public bool ErrorTemperatureSensor2 { get; private set; }
		public bool ErrorTemperatureSensor3 { get; private set; }
	}
}
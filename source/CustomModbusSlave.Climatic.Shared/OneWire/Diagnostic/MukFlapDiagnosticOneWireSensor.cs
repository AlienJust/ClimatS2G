namespace CustomModbusSlave.Es2gClimatic.Shared.OneWire.Diagnostic {
	public class MukFlapDiagnosticOneWireSensor : IMukFlapDiagnosticOneWireSensor {
		public MukFlapDiagnosticOneWireSensor(bool oneWireSensorError, IOneWireSensorErrorCode errorCode, int? linkErrorCount) {
			OneWireSensorError = oneWireSensorError;
			ErrorCode = errorCode;
			LinkErrorCount = linkErrorCount;
		}

		public bool OneWireSensorError { get; }

		public IOneWireSensorErrorCode ErrorCode { get; }

		public int? LinkErrorCount { get; }
	}
}

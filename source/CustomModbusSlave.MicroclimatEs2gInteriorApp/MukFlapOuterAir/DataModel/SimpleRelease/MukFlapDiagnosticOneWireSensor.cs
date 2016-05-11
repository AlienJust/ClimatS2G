using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease {
	class MukFlapDiagnosticOneWireSensor : IMukFlapDiagnosticOneWireSensor {
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
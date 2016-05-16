namespace CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DiagnosticOneWire {
	public interface IMukFlapDiagnosticOneWireSensor {
		bool OneWireSensorError { get; }
		IOneWireSensorErrorCode ErrorCode { get; }
		int? LinkErrorCount { get; }
	}
}
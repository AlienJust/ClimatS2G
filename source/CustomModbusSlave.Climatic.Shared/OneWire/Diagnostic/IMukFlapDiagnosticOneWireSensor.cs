namespace CustomModbusSlave.Es2gClimatic.Shared.OneWire.Diagnostic {
	public interface IMukFlapDiagnosticOneWireSensor {
		bool OneWireSensorError { get; }
		IOneWireSensorErrorCode ErrorCode { get; }
		int? LinkErrorCount { get; }
	}
}

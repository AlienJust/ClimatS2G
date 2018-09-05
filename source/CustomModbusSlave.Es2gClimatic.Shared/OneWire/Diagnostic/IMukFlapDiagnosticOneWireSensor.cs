namespace CustomModbusSlave.Es2gClimatic.Shared.MukFlap.DiagnosticOneWire {
	public interface IMukFlapDiagnosticOneWireSensor {
		bool OneWireSensorError { get; }
		IOneWireSensorErrorCode ErrorCode { get; }
		int? LinkErrorCount { get; }
	}
}

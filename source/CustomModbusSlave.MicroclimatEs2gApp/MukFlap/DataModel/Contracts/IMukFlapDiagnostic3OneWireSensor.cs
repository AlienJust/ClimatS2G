namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap {
	interface IMukFlapDiagnostic3OneWireSensor {
		bool OneWireSensorError { get; }
		OneWireSensorErrorCode ErrorCode { get; }
	}
}
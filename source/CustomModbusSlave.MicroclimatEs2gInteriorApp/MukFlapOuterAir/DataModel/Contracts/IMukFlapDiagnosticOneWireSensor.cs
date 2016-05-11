namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
	interface IMukFlapDiagnosticOneWireSensor {
		bool OneWireSensorError { get; }
		IOneWireSensorErrorCode ErrorCode { get; }
		int? LinkErrorCount { get; }
	}
}
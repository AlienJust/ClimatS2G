namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
	interface IMukFlapDiagnostic3OneWireSensor {
		bool OneWireSensorError { get; }
		OneWireSensorErrorCode ErrorCode { get; }
	}
}
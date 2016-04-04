namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
	class MukFlapDiagnostic3OneWireSensor : IMukFlapDiagnostic3OneWireSensor {
		public bool OneWireSensorError { get; private set; }
		public OneWireSensorErrorCode ErrorCode { get; private set; }
	}
}
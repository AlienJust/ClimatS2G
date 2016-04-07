namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
	internal interface IMukFlapReply03Telemetry {
		ushort FlapPosition { get; }
		int TemperatureAddress1 { get; }
		int TemperatureAddress2 { get; }
		IIncomingSignals IncomingSignals { get; }
		byte OutgoingSignals { get; }
		ushort AnalogInput { get; }
		IMukFlapWorkmodeStage AutomaticModeStage { get; }
		IMukFlapDiagnostic1 Diagnostic1 { get; }
		IMukFlapDiagnostic2 Diagnostic2 { get; }
		IMukFlapDiagnosticOneWireSensor Diagnostic3OneWire1 { get; }
		IMukFlapDiagnosticOneWireSensor Diagnostic4OneWire2 { get; }
		IEmersonDiagnostic EmersonDiagnostic { get; }
		double EmersonTemperature { get; }
		double EmersonPressure { get; }
		string EmersonValveSetting { get; }
		ushort FirmwareBuildNumber { get; }
	}
}
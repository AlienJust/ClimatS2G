namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
	internal interface IMukFlapReply03Telemetry {
		int FlapPosition { get; }
		double TemperatureAddress1 { get; }
		double TemperatureAddress2 { get; }
		IIncomingSignals IncomingSignals { get; }
		byte OutgoingSignals { get; }
		double AnalogInput { get; }
		IMukFlapWorkmodeStage AutomaticModeStage { get; }
		IMukFlapDiagnostic1 Diagnostic1 { get; }
		IMukFlapDiagnostic2 Diagnostic2 { get; }
		IMukFlapDiagnosticOneWireSensor Diagnostic3OneWire1 { get; }
		IMukFlapDiagnosticOneWireSensor Diagnostic4OneWire2 { get; }

		IEmersonDiagnosticCircuit1 EmersonDiagnosticCircuit1 { get; }
		double EmersonTemperatureCircuit1 { get; }
		double EmersonPressureCircuit1 { get; }
		int EmersonValveSettingCircuit1 { get; }

		IEmersonDiagnosticCircuit2 EmersonDiagnosticCircuit2 { get; }
		double EmersonTemperatureCircuit2 { get; }
		double EmersonPressureCircuit2 { get; }
		int EmersonValveSettingCircuit2 { get; }

		int FirmwareBuildNumber { get; }
	}
}
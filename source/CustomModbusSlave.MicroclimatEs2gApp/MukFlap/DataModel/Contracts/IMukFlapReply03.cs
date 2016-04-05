namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
	internal interface IMukFlapReply03 {
		ushort FlapPosition { get; }
		int TemperatureAddress1 { get; }
		int TemperatureAddress2 { get; }
		IIncomingSignals IncomingSignals { get; }
		byte OutgoingSignals { get; }
		ushort AnalogInput { get; }
		MukFlapWorkmodeStage AutomaticModeStage { get; }
		IMukFlapDiagnostic1 Diagnostic1 { get; }
		IMukFlapDiagnostic2 Diagnostic2 { get; }
		IMukFlapDiagnostic3OneWireSensor Diagnostic3OneWire1 { get; }
		IMukFlapDiagnostic3OneWireSensor Diagnostic4OneWire2 { get; }
		IEmersonDiagnostic EmersonDiagnostic { get; }
		string EmersonTemperature { get; }
		string EmersonPressure { get; }
		string EmersonValveSetting { get; }
		ushort FirmwareBuildNumber { get; }
	}
}
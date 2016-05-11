namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
	interface IMukFlapDiagnostic1 {
		bool NoEmersionControllerAnswer { get; }
		bool SensorOneWire1Error { get; }
		bool SensorOneWire2Error { get; }
	}
}
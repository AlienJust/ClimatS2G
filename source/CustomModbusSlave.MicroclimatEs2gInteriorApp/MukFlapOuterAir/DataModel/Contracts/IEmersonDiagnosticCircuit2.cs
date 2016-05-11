namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
	interface IEmersonDiagnosticCircuit2 {
		bool NotAvailable { get; }
		bool LowPressure { get; }
		bool HighOverheat { get; }
		bool LowOverheat { get; }
		bool Freeze { get; }
	}
}
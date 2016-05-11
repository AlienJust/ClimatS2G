namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
	interface IEmersonDiagnosticCircuit1 {
		bool NotAvailable { get; }
		bool LowPressure { get; }
		bool HighOverheat { get; }
		bool LowOverheat { get; }
		bool Freeze { get; }
		bool MaximumTemperature { get; }
		bool MinimumTemperature { get; }
	}
}
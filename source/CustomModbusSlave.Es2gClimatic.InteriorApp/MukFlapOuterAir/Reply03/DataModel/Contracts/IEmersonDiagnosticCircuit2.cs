namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts {
	interface IEmersonDiagnosticCircuit2 {
		bool NotAvailable { get; }
		bool LowPressure { get; }
		bool HighOverheat { get; }
		bool LowOverheat { get; }
		bool Freeze { get; }
	}
}
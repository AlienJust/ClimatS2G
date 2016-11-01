using CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.Reply03.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.Reply03.DataModel.SimpleRelease {
	class EmersonDiagnosticCircuit2 : IEmersonDiagnosticCircuit2 {
		public EmersonDiagnosticCircuit2(bool notAvailable, bool lowPressure, bool highOverheat, bool lowOverheat, bool freeze) {
			NotAvailable = notAvailable;
			LowPressure = lowPressure;
			HighOverheat = highOverheat;
			LowOverheat = lowOverheat;
			Freeze = freeze;
		}

		public bool NotAvailable { get; }
		public bool LowPressure { get; }
		public bool HighOverheat { get; }
		public bool LowOverheat { get; }
		public bool Freeze { get; }
	}
}
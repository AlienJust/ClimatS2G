using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel {
	class EmersonDiagnosticCircuit1 : IEmersonDiagnosticCircuit1 {
		public EmersonDiagnosticCircuit1(bool notAvailable, bool lowPressure, bool highOverheat, bool lowOverheat, bool freeze, bool maximumTemperature, bool minimumTemperature) {
			NotAvailable = notAvailable;
			LowPressure = lowPressure;
			HighOverheat = highOverheat;
			LowOverheat = lowOverheat;
			Freeze = freeze;
			MaximumTemperature = maximumTemperature;
			MinimumTemperature = minimumTemperature;
		}

		public bool NotAvailable { get; }
		public bool LowPressure { get; }
		public bool HighOverheat { get; }
		public bool LowOverheat { get; }
		public bool Freeze { get; }
		public bool MaximumTemperature { get; }
		public bool MinimumTemperature { get; }
	}
}

﻿using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel {
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

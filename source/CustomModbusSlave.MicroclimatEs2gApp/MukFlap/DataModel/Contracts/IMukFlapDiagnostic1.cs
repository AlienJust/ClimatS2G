namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
	interface IMukFlapDiagnostic1 {
		bool NoEmersionControllerAnswer { get; }
		bool SensorOneWire1Error { get; }
		bool SensorOneWire2Error { get; }
	}

	class MukFlapDiagnostic1 : IMukFlapDiagnostic1 {
		public bool NoEmersionControllerAnswer { get; private set; }
		public bool SensorOneWire1Error { get; private set; }
		public bool SensorOneWire2Error { get; private set; }
	}
}
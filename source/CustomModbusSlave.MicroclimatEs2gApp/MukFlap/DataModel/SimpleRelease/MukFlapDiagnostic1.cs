using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease {
	class MukFlapDiagnostic1 : IMukFlapDiagnostic1 {
		public bool NoEmersionControllerAnswer { get; private set; }
		public bool SensorOneWire1Error { get; private set; }
		public bool SensorOneWire2Error { get; private set; }
	}
}
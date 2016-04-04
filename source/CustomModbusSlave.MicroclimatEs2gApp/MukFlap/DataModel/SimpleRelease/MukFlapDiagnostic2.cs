using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease {
	class MukFlapDiagnostic2 : IMukFlapDiagnostic2 {
		public bool OsShowsFlapDoesNotReachLimitPositions { get; private set; }
		public bool OsShowsFlapDoesNotReach50Percent { get; private set; }
	}
}
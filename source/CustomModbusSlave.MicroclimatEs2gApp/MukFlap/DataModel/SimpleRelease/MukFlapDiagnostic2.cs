using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease {
	class MukFlapDiagnostic2 : IMukFlapDiagnostic2 {
		public MukFlapDiagnostic2(bool osShowsFlapDoesNotReachLimitPositions, bool osShowsFlapDoesNotReach50Percent) {
			OsShowsFlapDoesNotReachLimitPositions = osShowsFlapDoesNotReachLimitPositions;
			OsShowsFlapDoesNotReach50Percent = osShowsFlapDoesNotReach50Percent;
		}

		public bool OsShowsFlapDoesNotReachLimitPositions { get; }

		public bool OsShowsFlapDoesNotReach50Percent { get; }
	}
}
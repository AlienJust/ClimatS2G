using CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DataModel.SimpleReleases {
	public class MukFlapDiagnostic2 : IMukFlapDiagnostic2 {
		public MukFlapDiagnostic2(bool osShowsFlapDoesNotReachLimitPositions, bool osShowsFlapDoesNotReach50Percent) {
			OsShowsFlapDoesNotReachLimitPositions = osShowsFlapDoesNotReachLimitPositions;
			OsShowsFlapDoesNotReach50Percent = osShowsFlapDoesNotReach50Percent;
		}

		public bool OsShowsFlapDoesNotReachLimitPositions { get; }

		public bool OsShowsFlapDoesNotReach50Percent { get; }
	}
}
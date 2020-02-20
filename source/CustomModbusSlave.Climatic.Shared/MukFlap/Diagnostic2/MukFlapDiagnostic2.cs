namespace CustomModbusSlave.Es2gClimatic.Shared.MukFlap.Diagnostic2 {
	public class MukFlapDiagnostic2 : IMukFlapDiagnostic2 {
		public MukFlapDiagnostic2(bool osShowsFlapDoesNotReachLimitPositions, bool osShowsFlapDoesNotReach50Percent) {
			OsShowsFlapDoesNotReachLimitPositions = osShowsFlapDoesNotReachLimitPositions;
			OsShowsFlapDoesNotReach50Percent = osShowsFlapDoesNotReach50Percent;
		}

		public bool OsShowsFlapDoesNotReachLimitPositions { get; }

		public bool OsShowsFlapDoesNotReach50Percent { get; }
	}
}

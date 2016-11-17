namespace CustomModbusSlave.Es2gClimatic.Shared.MukFlap.Diagnostic2 {
	public interface IMukFlapDiagnostic2 {
		bool OsShowsFlapDoesNotReachLimitPositions { get; }
		bool OsShowsFlapDoesNotReach50Percent { get; }
	}
}
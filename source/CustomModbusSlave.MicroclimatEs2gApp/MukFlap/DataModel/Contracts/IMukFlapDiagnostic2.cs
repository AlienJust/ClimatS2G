namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap {
	interface IMukFlapDiagnostic2 {
		bool OsShowsFlapDoesNotReachLimitPositions { get; }
		bool OsShowsFlapDoesNotReach50Percent { get; }
	}
}
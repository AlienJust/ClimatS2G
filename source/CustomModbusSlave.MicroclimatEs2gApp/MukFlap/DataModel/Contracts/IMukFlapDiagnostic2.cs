namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
	interface IMukFlapDiagnostic2 {
		bool OsShowsFlapDoesNotReachLimitPositions { get; }
		bool OsShowsFlapDoesNotReach50Percent { get; }
	}
}
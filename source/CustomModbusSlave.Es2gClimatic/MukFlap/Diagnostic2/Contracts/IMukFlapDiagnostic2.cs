namespace CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DataModel.Contracts {
	public interface IMukFlapDiagnostic2 {
		bool OsShowsFlapDoesNotReachLimitPositions { get; }
		bool OsShowsFlapDoesNotReach50Percent { get; }
	}
}
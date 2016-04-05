using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease {
	class MukFlapDiagnostic2 : IMukFlapDiagnostic2 {
		private readonly bool _osShowsFlapDoesNotReachLimitPositions;
		private readonly bool _osShowsFlapDoesNotReach50Percent;
		public MukFlapDiagnostic2(bool osShowsFlapDoesNotReachLimitPositions, bool osShowsFlapDoesNotReach50Percent) {
			_osShowsFlapDoesNotReachLimitPositions = osShowsFlapDoesNotReachLimitPositions;
			_osShowsFlapDoesNotReach50Percent = osShowsFlapDoesNotReach50Percent;
		}

		public bool OsShowsFlapDoesNotReachLimitPositions {
			get { return _osShowsFlapDoesNotReachLimitPositions; }
		}

		public bool OsShowsFlapDoesNotReach50Percent {
			get { return _osShowsFlapDoesNotReach50Percent; }
		}
	}
}
namespace CustomModbusSlave.Es2gClimatic.Shared.EmersionDiagnostic {
	class MukEmersionSwitchOnDiagnosticSimple : IMukEmersionSwitchOnDiagnostic {
		public MukEmersionSwitchOnDiagnosticSimple(bool mukIsSwitchingEmersionOn, bool mukIsWatingFor5MinutesAfterLastSwitchingEmersionOn, bool mukIsWaitingFor5MinutesWhileEmersionIsSwithedOn) {
			MukIsSwitchingEmersionOn = mukIsSwitchingEmersionOn;
			MukIsWatingFor5MinutesAfterLastSwitchingEmersionOn = mukIsWatingFor5MinutesAfterLastSwitchingEmersionOn;
			MukIsWaitingFor5MinutesWhileEmersionIsSwithedOn = mukIsWaitingFor5MinutesWhileEmersionIsSwithedOn;
		}

		public bool MukIsSwitchingEmersionOn { get; }
		public bool MukIsWatingFor5MinutesAfterLastSwitchingEmersionOn { get; }
		public bool MukIsWaitingFor5MinutesWhileEmersionIsSwithedOn { get; }
	}
}

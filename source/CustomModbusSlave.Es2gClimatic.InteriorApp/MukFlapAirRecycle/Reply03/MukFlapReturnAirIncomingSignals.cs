namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03 {
	internal sealed class MukFlapReturnAirIncomingSignals : IMukFlapReturnAirIncomingSignals {
		public MukFlapReturnAirIncomingSignals(bool controlOfContactor1, bool controlOfContactor2, bool flagOfDualSystemTrain) {
			ControlOfContactor1 = controlOfContactor1;
			ControlOfContactor2 = controlOfContactor2;
			FlagOfDualSystemTrain = flagOfDualSystemTrain;
		}

		public bool ControlOfContactor1 { get; }
		public bool ControlOfContactor2 { get; }
		public bool FlagOfDualSystemTrain { get; }
	}
}
namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03 {
	internal interface IMukFlapReturnAirIncomingSignals {
		bool ControlOfContactor1 { get; }
		bool ControlOfContactor2 { get; }
		bool FlagOfDualSystemTrain { get; }
	}
}

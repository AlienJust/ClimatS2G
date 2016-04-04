namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap {
	interface IIncomingSignals {
		bool SignalToTurnOnEmersion1 { get; }
		bool SignalToTurnOnEmersion2 { get; }
	}
}
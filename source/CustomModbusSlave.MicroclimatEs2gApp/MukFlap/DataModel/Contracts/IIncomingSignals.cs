namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
	interface IIncomingSignals {
		bool SignalToTurnOnEmersion1 { get; }
		bool SignalToTurnOnEmersion2 { get; }
	}
}
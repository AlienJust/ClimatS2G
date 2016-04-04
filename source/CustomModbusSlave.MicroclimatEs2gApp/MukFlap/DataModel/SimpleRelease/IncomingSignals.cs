namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
	class IncomingSignals : IIncomingSignals {
		public bool SignalToTurnOnEmersion1 { get; private set; }
		public bool SignalToTurnOnEmersion2 { get; private set; }
	}
}
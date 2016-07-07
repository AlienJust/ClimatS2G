using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.DataModel.SimpleRelease {
	class IncomingSignals : IIncomingSignals {
		public IncomingSignals(bool signalToTurnOnEmersion1, bool signalToTurnOnEmersion2) {
			SignalToTurnOnEmersion1 = signalToTurnOnEmersion1;
			SignalToTurnOnEmersion2 = signalToTurnOnEmersion2;
		}

		public bool SignalToTurnOnEmersion1 { get; }

		public bool SignalToTurnOnEmersion2 { get; }
	}
}
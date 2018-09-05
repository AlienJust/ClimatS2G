using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.SimpleRelease {
	class IncomingSignals : IIncomingSignals {
		public IncomingSignals(bool signalToTurnOnEmersion1, bool signalToTurnOnEmersion2) {
			SignalToTurnOnEmersion1 = signalToTurnOnEmersion1;
			SignalToTurnOnEmersion2 = signalToTurnOnEmersion2;
		}

		public bool SignalToTurnOnEmersion1 { get; }

		public bool SignalToTurnOnEmersion2 { get; }
	}
}

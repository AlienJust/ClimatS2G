using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease {
	class IncomingSignals : IIncomingSignals {
		private readonly bool _signalToTurnOnEmersion1;
		private readonly bool _signalToTurnOnEmersion2;

		public IncomingSignals(bool signalToTurnOnEmersion1, bool signalToTurnOnEmersion2) {
			_signalToTurnOnEmersion1 = signalToTurnOnEmersion1;
			_signalToTurnOnEmersion2 = signalToTurnOnEmersion2;
		}

		public bool SignalToTurnOnEmersion1 {
			get { return _signalToTurnOnEmersion1; }
		}

		public bool SignalToTurnOnEmersion2 {
			get { return _signalToTurnOnEmersion2; }
		}
	}
}
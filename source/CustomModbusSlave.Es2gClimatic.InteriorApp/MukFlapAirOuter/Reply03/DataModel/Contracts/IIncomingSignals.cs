namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts {
	interface IIncomingSignals {
		bool SignalToTurnOnEmersion1 { get; }
		bool SignalToTurnOnEmersion2 { get; }
	}
}

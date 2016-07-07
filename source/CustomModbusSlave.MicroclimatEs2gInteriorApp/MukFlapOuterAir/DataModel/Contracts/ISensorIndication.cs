namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.DataModel.Contracts {
	interface ISensorIndication<out T> {
		bool NoLinkWithSensor { get; }
		T Indication { get; }
	}
}
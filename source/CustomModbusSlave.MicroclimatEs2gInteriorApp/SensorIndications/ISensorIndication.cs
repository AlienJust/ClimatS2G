namespace CustomModbusSlave.MicroclimatEs2gApp.SensorIndications {
	interface ISensorIndication<out T> {
		bool NoLinkWithSensor { get; }
		T Indication { get; }
	}
}
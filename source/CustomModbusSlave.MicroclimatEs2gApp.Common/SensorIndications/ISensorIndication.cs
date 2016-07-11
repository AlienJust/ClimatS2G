namespace CustomModbusSlave.MicroclimatEs2gApp.SensorIndications {
	public interface ISensorIndication<out T> {
		bool NoLinkWithSensor { get; }
		T Indication { get; }
	}
}
namespace CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DiagnosticOneWire {
	public interface IOneWireSensorErrorCode {
		int AbsoluteValue { get; }
		OneWireSensorErrorCode KnownValue { get; }
	}
}
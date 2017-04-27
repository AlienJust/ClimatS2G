namespace CustomModbusSlave.Es2gClimatic.Shared.MukFlap.DiagnosticOneWire {
	public interface IOneWireSensorErrorCode {
		int AbsoluteValue { get; }
		OneWireSensorErrorCode KnownValue { get; }
	}
}
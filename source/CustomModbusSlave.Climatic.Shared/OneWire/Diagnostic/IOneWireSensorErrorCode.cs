namespace CustomModbusSlave.Es2gClimatic.Shared.OneWire.Diagnostic {
	public interface IOneWireSensorErrorCode {
		int AbsoluteValue { get; }
		OneWireSensorErrorCode KnownValue { get; }
	}
}

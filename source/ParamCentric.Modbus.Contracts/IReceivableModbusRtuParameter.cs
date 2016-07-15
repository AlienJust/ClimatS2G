namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	public interface IReceivableModbusRtuParameter : IReceivableParameter {
		byte CommandCode { get; }
		byte Address { get; }
		int ZeroBasedParameterNumber { get; }
	}
}
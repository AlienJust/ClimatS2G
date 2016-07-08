namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	interface IReceivableModbusRtuParameter : IReceivableParameter {
		byte CommandCode { get; }
		int ZeroBasedParameterNumber { get; }
		//void ReceiveValue(BytesPair value);
	}
}
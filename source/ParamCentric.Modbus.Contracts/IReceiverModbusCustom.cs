namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	public interface IReceiverModbusCustom {
		void RegisterParamToReceive(IReceivableModbusCustomParameter parameter);
	}
}
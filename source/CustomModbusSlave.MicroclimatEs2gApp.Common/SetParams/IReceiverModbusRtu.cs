namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	public interface IReceiverModbusRtu {
		void RegisterParamToReceive(IReceivableModbusRtuParameter parameter);
	}
}
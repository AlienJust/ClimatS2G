using System.Collections.Generic;

namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	public interface IReceivableModbusCustomParameter : IParameter {
		byte CommandCode { get; }
		byte Address { get; }
		void ReceiveData(IList<byte> dataBytes);
	}
}
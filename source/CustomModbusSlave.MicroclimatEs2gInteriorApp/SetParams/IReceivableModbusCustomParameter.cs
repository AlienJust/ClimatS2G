using System.Collections.Generic;

namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	interface IReceivableModbusCustomParameter : IParameter {
		byte CommandCode { get; }
		void ReceiveData(IList<byte> dataBytes);
	}
}
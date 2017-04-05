using System.Collections.Generic;
using ParamCentric.Common.Contracts;

namespace ParamCentric.Modbus.Contracts {
	public interface IReceivableModbusCustomParameter : IParameter {
		byte CommandCode { get; }
		byte Address { get; }
		void ReceiveData(IList<byte> dataBytes);
	}
}
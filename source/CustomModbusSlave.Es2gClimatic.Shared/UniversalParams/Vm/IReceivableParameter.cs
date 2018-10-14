using System.Collections.Generic;

namespace ParamControls.Vm {
	public interface IReceivableParameter {
		//void SetReceivedValueFromCommandPart(IList<byte> commandPartDataWithoutHeaderAndCrc); // DriverSide
		string ReceiveName { get; }
		IList<byte> ReceivedRawValue { get; } // UserSide
		event NotifyDataReceivedDelegate NotifyDataReceived; // UserSide
	}
}

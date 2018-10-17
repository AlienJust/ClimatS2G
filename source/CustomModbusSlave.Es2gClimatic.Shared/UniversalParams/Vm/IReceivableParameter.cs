using System.Collections.Generic;

namespace ParamControls.Vm {
	
	/// <summary>
	/// Represent some portion of received data
	/// </summary>
	/// <typeparam name="T">Type of received data</typeparam>
	public interface IReceivableParameter<out T> {
		string ReceiveName { get; }
		//IList<byte> ReceivedRawValue { get; } // UserSide
		T ReceivedRawValue { get; }
		event NotifyDataReceivedDelegate NotifyDataReceived; // UserSide
	}
}

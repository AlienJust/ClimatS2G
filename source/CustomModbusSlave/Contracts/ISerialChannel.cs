using System;

namespace CustomModbusSlave.Contracts {
	public interface ISerialChannel {
		event CommandHearedDelegate CommandHeared;
		event CommandHearedWithReplyPossibilityDelegate CommandHearedWithReplyPossibility;
		//void SelectPortAsync(string portName, int baudRate, Action<Exception> comPortOpenedCallbackAction);
		void SelectPortAsync(ISerialPortContainer portContainer, Action<Exception> comPortOpenedCallbackAction);
		void CloseCurrentPortAsync(Action<Exception> comPortClosedCallbackAction);
	}
}

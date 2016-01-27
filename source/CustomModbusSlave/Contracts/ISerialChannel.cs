using System;

namespace CustomModbusSlave.Contracts {
	public interface ISerialChannel {
		event CommandHearedDelegate CommandHeared;
		void SelectPortAsync(string portName, int baudRate, Action<Exception> comPortOpenedCallbackAction);
		void CloseCurrentPortAsync(Action<Exception> comPortClosedCallbackAction);
	}
}
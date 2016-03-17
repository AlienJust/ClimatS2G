using System;

namespace CustomModbusSlave.Contracts {
	public interface ISerialPortContainer : ISendAbility {
		byte[] ReadBytes(int count, TimeSpan timeout);
		void CloseCurrentPort();
		void SelectPort(string portName, int baudRate);
	}
}
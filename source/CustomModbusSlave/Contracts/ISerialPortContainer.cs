namespace CustomModbusSlave.Contracts {
	public interface ISerialPortContainer {
		byte[] ReadBytes(int count, int timeoutSeconds);
		void CloseCurrentPort();
		void SelectPort(string portName, int baudRate);
	}
}
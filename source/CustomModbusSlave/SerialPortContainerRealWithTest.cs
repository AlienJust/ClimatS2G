using System;
using CustomModbusSlave.Contracts;

namespace CustomModbusSlave {
	
	/*public class SerialPortContainerRealWithTest : ISerialPortContainer {
		private readonly string _testPortName;
		private readonly ISerialPortContainer _realPortContainer;
		private readonly ISerialPortContainer _testContainer;

		private ISerialPortContainer _currentPortConatiner;

		public SerialPortContainerRealWithTest(string testPortName, ISerialPortContainer realPortContainer, ISerialPortContainer testContainer) {
			_testPortName = testPortName;
			_realPortContainer = realPortContainer;
			_testContainer = testContainer;

			_currentPortConatiner = null;
		}

		public byte[] Read(int count, TimeSpan timeout) {
			if (_currentPortConatiner == null) throw new NullReferenceException("_currentPortConatiner is null");
			return _currentPortConatiner.Read(count, timeout);
		}

		public void Close() {
			_currentPortConatiner?.Close();
		}

		public void SelectPort(string portName, int baudRate) {
			Close();
			_currentPortConatiner = portName != _testPortName ? _realPortContainer : _testContainer;
			_currentPortConatiner.SelectPort(portName, baudRate);
		}

		public void Send(byte[] bytes) {
			if (_currentPortConatiner == null) throw new NullReferenceException("_currentPortConatiner is null");
			_currentPortConatiner.Send(bytes);
		}
	}*/
}
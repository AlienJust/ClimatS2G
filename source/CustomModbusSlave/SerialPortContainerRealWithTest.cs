using System;
using System.IO.Ports;
using CustomModbusSlave.Contracts;

namespace CustomModbusSlave {
	
	public class SerialPortContainerRealWithTest : ISerialPortContainer {
		private SerialPort _port;
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

		public byte[] ReadBytes(int count, TimeSpan timeout) {
			if (_currentPortConatiner == null) throw new NullReferenceException("_currentPortConatiner is null");
			return _currentPortConatiner.ReadBytes(count, timeout);
		}

		public void CloseCurrentPort() {
			if (_currentPortConatiner != null)
				_currentPortConatiner.CloseCurrentPort();
		}

		public void SelectPort(string portName, int baudRate) {
			CloseCurrentPort();
			_currentPortConatiner = portName != _testPortName ? _realPortContainer : _testContainer;
			_currentPortConatiner.SelectPort(portName, baudRate);
		}
	}
}
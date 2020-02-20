using System;
using System.Collections.Generic;
using System.Threading;
using CustomModbusSlave.Contracts;

namespace CustomModbusSlave {
	public class SerialPortContainerTest : ISerialPortContainer, ISerialPortContainerWithProgress {
		private readonly List<byte> _bytes;

		private bool _isOpen;
		private int _currrentPosition;
		

		public SerialPortContainerTest() {
			_isOpen = false;
			_currrentPosition = 0;

			_bytes = new List<byte>();
		}

		public SerialPortContainerTest(IList<byte> additionalBytes) : this() {
			_bytes.AddRange(additionalBytes);
		}


		public byte[] Read(int count, TimeSpan timeout) {
			if (!_isOpen) throw new Exception("Test port is not opened");
			var resultBytes = new byte[count];

			for (int i = 0; i < count; ++i) {
				resultBytes[i] = _bytes[_currrentPosition];
				_currrentPosition = _currrentPosition + 1;
				if (_currrentPosition == _bytes.Count) {
					_currrentPosition = 0;
				}
			}
			Thread.Sleep(1);
			return resultBytes;
		}

		public void Close() {
			//if (!_isOpen) throw new Exception("Port already closed");
			_isOpen = false;
		}

		public void Open() {
			_isOpen = true;
		}

		public bool IsOpen => _isOpen;
		
		public void Send(byte[] bytes) {
			// do nothing;
			//Console.WriteLine("TEST SENDER SEND: " + bytes.ToText());
		}

		public double ProgressPercentage => 100.0 * _currrentPosition / _bytes.Count;
	}
}

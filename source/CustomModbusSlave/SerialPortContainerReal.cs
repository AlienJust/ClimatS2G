using System;
using System.IO.Ports;
using AlienJust.Support.Serial;
using CustomModbusSlave.Contracts;

namespace CustomModbusSlave {
	public class SerialPortContainerReal : ISerialPortContainer {
		private SerialPort _port;

		public byte[] ReadBytes(int count, int timeoutSeconds) {
			if (_port == null) throw new NullReferenceException("Serial port is null");
			if (!_port.IsOpen) throw new Exception("Serial port is not opened");

			var extender = new SerialPortExtender(_port, Console.WriteLine);
			return extender.ReadBytes(count, timeoutSeconds, false);
		}

		public void CloseCurrentPort() {
			if (_port != null)
			{
				if (_port.IsOpen)
				{
					_port.Close();
				}
			}
		}

		public void SelectPort(string portName, int baudRate) {
			CloseCurrentPort();
			
			_port = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
			_port.Open();
		}
	}
}
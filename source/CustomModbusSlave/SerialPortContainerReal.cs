using System;
using System.IO.Ports;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Serial;
using AlienJust.Support.Text;
using CustomModbusSlave.Contracts;

namespace CustomModbusSlave {
	public class SerialPortContainerReal : ISerialPortContainer {
		public static readonly ILogger Logger = new RelayActionLogger(Console.WriteLine, new DateTimeFormatter(" > "));
		private SerialPort _port;

		public byte[] ReadBytes(int count, TimeSpan timeout) {
			if (_port == null) throw new NullReferenceException("Serial port is null");
			if (!_port.IsOpen) throw new Exception("Serial port is not opened");

			var extender = new SerialPortExtender(_port, Console.WriteLine);
			return extender.ReadBytes(count, timeout, false);
		}

		public void CloseCurrentPort() {
			if (_port != null) {
				if (_port.IsOpen) {
					_port.Close();
				}
			}
		}

		public void SelectPort(string portName, int baudRate) {
			CloseCurrentPort();

			_port = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
			_port.Open();
		}

		public void Send(byte[] bytes) {
			if (_port == null) throw new NullReferenceException("Serial port is null");
			if (!_port.IsOpen) throw new Exception("Serial port is not opened");

			_port.Write(bytes, 0, bytes.Length);
			Logger.Log("Sended >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> " + bytes.ToText());
		}
	}
}
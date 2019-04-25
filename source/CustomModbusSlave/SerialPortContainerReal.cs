using System;
using System.IO.Ports;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Serial;
using AlienJust.Support.Text;
using CustomModbusSlave.Contracts;

namespace CustomModbusSlave
{
    public class SerialPortContainerReal : ISerialPortContainer
    {
        //public static readonly ILogger Logger = new RelayActionLogger(Console.WriteLine, new DateTimeFormatter(" > "));
        private readonly SerialPort _port;

        public SerialPortContainerReal(string portName, int baudRate)
        {
            _port = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
            _port.Open();
        }

        public byte[] Read(int count, TimeSpan timeout)
        {
            if (_port == null) throw new NullReferenceException("Serial port is null");
            if (!_port.IsOpen) throw new Exception("Serial port is not opened");

            var extender = new SerialPortExtenderNoLog(_port);
            return extender.ReadBytes(count, timeout, false);
        }

        public void Close()
        {
            if (_port != null)
            {
                if (_port.IsOpen)
                {
                    _port.Close();
                }
            }
        }

        public void Open()
        {
            if (!_port.IsOpen) _port.Open();
        }

        public bool IsOpen => _port.IsOpen;


        public void Send(byte[] bytes)
        {
            if (_port == null) throw new NullReferenceException("Serial port is null");
            if (!_port.IsOpen) throw new Exception("Serial port is not opened");

            _port.Write(bytes, 0, bytes.Length);
            //_logger.Log("Sended >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> " + bytes.ToText());
        }

        public override string ToString()
        {
            return _port.PortName + ", скорость обмена: " + _port.BaudRate + " б/с";
        }
    }
}

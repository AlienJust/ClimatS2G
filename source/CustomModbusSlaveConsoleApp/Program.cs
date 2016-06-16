using System;
using System.IO;
using System.Linq;
using AlienJust.Support.Text;
using CustomModbusSlave;
using CustomModbusSlave.Contracts;
using DataAbstractionLevel.Low.PsnConfig;

namespace CustomModbusSlaveConsoleApp
{
	class Program {
		private const string ArgStartPortName = "-portname:";
		private const string ArgStartBaudRate = "-baudrate:";
		static void Main(string[] args) {
			var argPortName = args.First(a => a.StartsWith(ArgStartPortName)).Split(':')[1];
			var argBaudRate = int.Parse(args.First(a => a.StartsWith(ArgStartBaudRate)).Split(':')[1]);

			var psnConfig = new PsnProtocolConfigurationLoaderFromXml(Path.Combine(Environment.CurrentDirectory, "psn.Микроклимат-ЭС2ГП-салон.xml")).LoadConfiguration();

			var serialPortContainer = new SerialPortContainerReal();
			var sch = new SerialChannel(
				new CommandPartSearcherPsnConfigBasedFast(psnConfig),
				serialPortContainer, serialPortContainer);
			sch.CommandHeared += SchOnCommandHeared;
			sch.SelectPortAsync(argPortName, argBaudRate, null);
			//Thread.Sleep(5000);
			//sch.SelectPortAsync("COM2");
			//Thread.Sleep(5000);
			
			Console.ReadKey();
			sch.CloseCurrentPortAsync(null);
			sch.StopWork();
			Console.ReadKey();
		}

		private static void SchOnCommandHeared(ICommandPart commandPart) {
			Console.WriteLine("============================== Command part found! =============================== CMD: " + commandPart.CommandCode + ", ADDR: " + commandPart.Address + ", BODY: " + commandPart.ReplyBytes.ToText());
		}
	}
}

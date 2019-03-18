using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AlienJust.Adaptation.ConsoleLogger;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Text;
using AlienJust.Support.Text.Contracts;
using CustomModbusSlave;
using CustomModbusSlave.Contracts;
using DataAbstractionLevel.Low.PsnConfig;

namespace CustomModbusSlaveConsoleApp
{
    class Program
    {
        private static ILoggerWithStackTrace _logConsoleYellow;
        private const string NoStackInfoText = "[NO STACK INFO]";
        private const string LogSeporator = " > ";

        private const string ArgStartPortName = "-portname:";
        private const string ArgStartBaudRate = "-baudrate:";

        static void Main(string[] args)
        {
            _logConsoleYellow = new RelayLoggerWithStackTrace(
                new RelayLogger(new ColoredConsoleLogger(ConsoleColor.Yellow, ConsoleColor.Black),
                    new ChainedFormatter(new List<ITextFormatter>
                    {
                        new ThreadFormatter(LogSeporator, true, false, false), new DateTimeFormatter(LogSeporator)
                    })),
                new StackTraceFormatterWithNullSuport(LogSeporator, NoStackInfoText));

            var argPortName = args.First(a => a.StartsWith(ArgStartPortName)).Split(':')[1];
            var argBaudRate = int.Parse(args.First(a => a.StartsWith(ArgStartBaudRate)).Split(':')[1]);

            var psnConfig = new PsnProtocolConfigurationLoaderFromXml(Path.Combine(Environment.CurrentDirectory, "psn.S2G-climatic-interior.xml")).LoadConfiguration();

            var serialPortContainer = new SerialPortContainerReal(argPortName, argBaudRate);
            var sch = new SerialChannel(
                new CommandPartSearcherPsnConfigBasedFast(psnConfig), _logConsoleYellow);
            sch.CommandHeared += SchOnCommandHeared;
            sch.SelectPortAsync(serialPortContainer, null);
            //Thread.Sleep(5000);
            //sch.SelectPortAsync("COM2");
            //Thread.Sleep(5000);

            Console.ReadKey();
            sch.CloseCurrentPortAsync(null);
            sch.StopWork();
            Console.ReadKey();
        }

        private static void SchOnCommandHeared(ICommandPart commandPart)
        {
            Console.WriteLine("============================= Command part found! ============================== CMD: " +
                              commandPart.CommandCode + ", ADDR: " + commandPart.Address + ", BODY: " +
                              commandPart.ReplyBytes.ToText());
        }
    }
}
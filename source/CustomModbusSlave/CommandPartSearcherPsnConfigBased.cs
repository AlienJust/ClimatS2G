using System;
using System.Collections.Generic;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Text;
using CustomModbusSlave.Contracts;
using DataAbstractionLevel.Low.PsnConfig.Contracts;
using DataAbstractionLevel.Low.PsnData.Contracts;

namespace CustomModbusSlave {
	public class CommandPartSearcherPsnConfigBased : ICommandPartSearcher {
		public static readonly ILogger Logger = new RelayActionLogger(Console.WriteLine, new DateTimeFormatter(" > "));
		private readonly IPsnProtocolConfiguration _psnProtocolConfiguration;
		public CommandPartSearcherPsnConfigBased(IPsnProtocolConfiguration psnProtocolConfiguration) {
			_psnProtocolConfiguration = psnProtocolConfiguration;
		}


		public void SearchForCommands(List<byte> incomingBuffer, ICommandPartFoundListener listener) {
			IPsnCommandPartSearcher commandPartSearcher = new PsnCommandPartSearcherStandart();
			foreach (var commandPart in _psnProtocolConfiguration.CommandParts)
			{
				Logger.Log("Searching for command part: " + commandPart.PartName);
				for (int i = 0; i < incomingBuffer.Count; ++i)
				{
					if (incomingBuffer.Count - i >= commandPart.Length)
					{
						try
						{
							//_logger.Log(_incomingBuffer.Skip(i).Take(commandPart.Length).ToList().ToText());
							var confirmation = commandPartSearcher.IsHereCmdPart(incomingBuffer, i, commandPart);
							if (confirmation == PsnCommandPartConfirmation.EverythyngIsOk)
							{
								var commandPartBytes = new List<byte>();
								for (int j = i; j < i + commandPart.Length; ++j)
								{
									commandPartBytes.Add(incomingBuffer[i]);
									incomingBuffer.RemoveAt(i);
								}
								Logger.Log("COMMAND PART FOUND <<<<<<<<<<<<<<<<<<<<<<<<<<");
								listener.CommandPartFound(new CommandPartSimple((byte)commandPart.Address.DefinedValue, (byte)commandPart.CommandCode.DefinedValue, commandPartBytes));
								i--; // to check from the same position again after removing items from list
							}
							else
							{
								if (confirmation == PsnCommandPartConfirmation.WrongFirstCrcByte || confirmation == PsnCommandPartConfirmation.WrongSecondCrcByte)
								{
									Logger.Log("Command part was found, but CRC was wrong");
								}
							}
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex);
						}
					}
					else
					{
						break;
					}
				}
			}
		}
	}
}
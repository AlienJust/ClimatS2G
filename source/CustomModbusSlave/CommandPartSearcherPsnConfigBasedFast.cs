using System;
using System.Collections.Generic;
using System.Linq;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Text;
using CustomModbusSlave.Contracts;
using DataAbstractionLevel.Low.PsnConfig.Contracts;
using DataAbstractionLevel.Low.PsnData.Contracts;

namespace CustomModbusSlave {
	public class CommandPartSearcherPsnConfigBasedFast : ICommandPartSearcher {
		public static readonly ILogger Logger = new RelayActionLogger(Console.WriteLine, new DateTimeFormatter(" > "));
		private readonly List<IPsnProtocolCommandPartConfiguration> _cmdPartConfigs;
		private readonly int _cmdPartsCount;

		private readonly IPsnCommandPartSearcher _commandPartSearcher;
		public CommandPartSearcherPsnConfigBasedFast(IPsnProtocolConfiguration psnProtocolConfiguration) {
			_cmdPartConfigs = psnProtocolConfiguration.CommandParts.ToList();
			_cmdPartsCount = _cmdPartConfigs.Count;
			_commandPartSearcher = new PsnCommandPartSearcherStandart();
		}


		public void SearchForCommands(IList<byte> incomingBuffer, ICommandPartFoundListener listener) {
			for (int x = 0; x <_cmdPartsCount; ++x) {
				var commandPart = _cmdPartConfigs[x];
				for (int i = 0; i < incomingBuffer.Count; ++i)
				{
					if (incomingBuffer.Count - i >= commandPart.Length)
					{
						//Logger.Log("Searching for command part: " + commandPart.PartName);
						try
						{
							//_logger.Log(_incomingBuffer.Skip(i).Take(commandPart.Length).ToList().ToText());
							var confirmation = _commandPartSearcher.IsHereCmdPart(incomingBuffer, i, commandPart);
							if (confirmation == PsnCommandPartConfirmation.EverythyngIsOk)
							{
								//Logger.Log("Command found at i = " + i + ", buffer length = " + incomingBuffer.Count + ", commandPart.Length = " + commandPart.Length);
								var commandPartBytes = new List<byte>();
								commandPartBytes.AddRange(incomingBuffer.Skip(i).Take(commandPart.Length));
								int removeBytesCount = i + commandPart.Length;
								for (int j = 0; j < removeBytesCount; ++j) // clearing from zero to remove all the junk bytes
								{
									incomingBuffer.RemoveAt(0);
								}
								Logger.Log("COMMAND PART FOUND <<<<<<<<<<<<<<<<<<<<<<<<<< " + commandPart.PartName);
								listener.CommandPartFound(new CommandPartSimple((byte)commandPart.Address.DefinedValue, (byte)commandPart.CommandCode.DefinedValue, commandPartBytes));
								i = -1; // because next iteration will ++i
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
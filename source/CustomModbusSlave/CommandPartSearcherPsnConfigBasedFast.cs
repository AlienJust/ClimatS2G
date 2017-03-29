using System;
using System.Collections.Generic;
using System.Linq;
using AlienJust.Support.Loggers;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.Text;
using CustomModbusSlave.Contracts;
using DataAbstractionLevel.Low.PsnConfig.Contracts;
using DataAbstractionLevel.Low.PsnData;
using DataAbstractionLevel.Low.PsnData.Contracts;

namespace CustomModbusSlave {
	public class CommandPartSearcherPsnConfigBasedFast : ICommandPartSearcher {
		//public static readonly ILogger Logger = new RelayActionLogger(Console.WriteLine, new DateTimeFormatter(" > "));
		private readonly List<IPsnProtocolCommandPartConfiguration> _cmdPartConfigs;
		private readonly int _cmdPartsCount;

		//private const string CommandFoundMessageStart = "COMMAND PART FOUND <<<<<<<<<<<<<<<<<<<<<<<<<< ";

		private readonly IPsnCommandPartSearcher _commandPartSearcher;


		public CommandPartSearcherPsnConfigBasedFast(IPsnProtocolConfiguration psnProtocolConfiguration) {
			_cmdPartConfigs = psnProtocolConfiguration.CommandParts.ToList();
			_cmdPartsCount = _cmdPartConfigs.Count;

			_commandPartSearcher = new PsnCommandPartSearcherStandart();
		}


		public void SearchForCommands(List<byte> incomingBuffer, ICommandPartFoundListener listener) {
			for (int i = 0; i < incomingBuffer.Count; ++i) {
				for (int x = 0; x < _cmdPartsCount; ++x) {
					var commandPart = _cmdPartConfigs[x];

					if (incomingBuffer.Count - i >= commandPart.Length) {
						try {
							var confirmation = _commandPartSearcher.IsHereCmdPart(incomingBuffer.ToArray(), i, commandPart);
							if (confirmation == PsnCommandPartConfirmation.EverythyngIsOk) {
								var commandPartBytes = new List<byte>();
								commandPartBytes.AddRange(incomingBuffer.Skip(i).Take(commandPart.Length));

								// clean buffer bytes before command and command's bytes:
								int removeBytesCount = i + commandPart.Length;
								incomingBuffer.RemoveRange(0, removeBytesCount);
								i = -1; // because the next cycle iteration will increase i by 1 and "i" will be zero

								//Logger.Log($"{CommandFoundMessageStart}{commandPart.PartName}");
								listener.CommandPartFound(new CommandPartSimple((byte)commandPart.Address.DefinedValue, (byte)commandPart.CommandCode.DefinedValue, commandPartBytes));
								break;
							}
						}
						catch (Exception ex) {
							//Logger.Log(ex);
						}
					}
				}
			}
		}
	}
}
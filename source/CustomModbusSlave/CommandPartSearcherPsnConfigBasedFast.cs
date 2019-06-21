using System.Collections.Generic;
using System.Linq;
using CustomModbusSlave.Contracts;
using DataAbstractionLevel.Low.PsnConfig.Contracts;
using DataAbstractionLevel.Low.PsnData;
using DataAbstractionLevel.Low.PsnData.Contracts;

namespace CustomModbusSlave {
	public class CommandPartSearcherPsnConfigBasedFast : ICommandPartSearcher {
		private readonly List<IPsnProtocolCommandPartConfiguration> _cmdPartConfigs;
		private readonly int _cmdPartsCount;


		private readonly IPsnCommandPartSearcher _commandPartSearcher;


		public CommandPartSearcherPsnConfigBasedFast(IPsnProtocolConfiguration psnProtocolConfiguration) {
			_cmdPartConfigs = psnProtocolConfiguration.CommandParts.ToList();
			_cmdPartsCount = _cmdPartConfigs.Count;

			_commandPartSearcher = new PsnCommandPartSearcherAllDefinedValuesAndCrc();
		}


		public void SearchForCommands(List<byte> incomingBuffer, ICommandPartFoundListener listener) {
			for (int i = 0; i < incomingBuffer.Count; ++i) {
				for (int x = 0; x < _cmdPartsCount; ++x) {
					var commandPart = _cmdPartConfigs[x];

					if (incomingBuffer.Count - i >= commandPart.Length) {
                        try
                        {
							var confirmation = _commandPartSearcher.IsHereCmdPart(incomingBuffer.ToArray(), i, commandPart); // TODO: IS NOT SO FAST CAUSE OF ARRAY
							if (confirmation == PsnCommandPartConfirmation.EverythyngIsOk) {
                                var commandPartBytes = incomingBuffer.Skip(i).Take(commandPart.Length).ToArray();
                                //var commandPartBytes = new List<byte>();
                                //commandPartBytes.AddRange(incomingBuffer.Skip(i).Take(commandPart.Length));

                                // clean buffer bytes before command and command's bytes:
                                int removeBytesCount = i + commandPart.Length;
								incomingBuffer.RemoveRange(0, removeBytesCount);
								i = -1; // because the next cycle iteration will increase i by 1 and "i" will be zero

								listener.CommandPartFound(new CommandPartSimple((byte)commandPart.Address.DefinedValue, (byte)commandPart.CommandCode.DefinedValue, commandPartBytes, commandPart));
								break;
							}
						}
						catch
                        {
						}
					}
				}
			}
		}
	}
}

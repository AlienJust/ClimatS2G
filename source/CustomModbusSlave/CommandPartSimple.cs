using System.Collections.Generic;
using CustomModbusSlave.Contracts;
using DataAbstractionLevel.Low.PsnConfig.Contracts;

namespace CustomModbusSlave
{
    class CommandPartSimple : ICommandPart
    {
        public CommandPartSimple(byte address, byte commandCode, byte[] replyBytes, IPsnProtocolCommandPartConfiguration cmdPartConfiguration)
        {
            Address = address;
            CommandCode = commandCode;
            ReplyBytes = replyBytes;
            PsnPartConfig = cmdPartConfiguration;
        }

        public byte Address { get; }

        public byte CommandCode { get; }

        public byte[] ReplyBytes { get; }

        public IPsnProtocolCommandPartConfiguration PsnPartConfig { get; }

    }
}

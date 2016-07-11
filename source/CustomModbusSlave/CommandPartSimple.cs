using System.Collections.Generic;
using CustomModbusSlave.Contracts;

namespace CustomModbusSlave {
	class CommandPartSimple : ICommandPart {
		public CommandPartSimple(byte address, byte commandCode, IList<byte> replyBytes) {
			Address = address;
			CommandCode = commandCode;
			ReplyBytes = replyBytes;
		}

		public byte Address { get; }

		public byte CommandCode { get; }

		public IList<byte> ReplyBytes { get; }
	}
}
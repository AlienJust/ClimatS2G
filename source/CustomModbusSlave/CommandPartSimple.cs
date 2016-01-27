using System.Collections.Generic;
using CustomModbusSlave.Contracts;

namespace CustomModbusSlave {
	class CommandPartSimple : ICommandPart {
		private readonly byte _address;
		private readonly byte _commandCode;
		private readonly IList<byte> _replyBytes;
		public CommandPartSimple(byte address, byte commandCode, IList<byte> replyBytes) {
			_address = address;
			_commandCode = commandCode;
			_replyBytes = replyBytes;
		}

		public byte Address {
			get { return _address; }
		}

		public byte CommandCode {
			get { return _commandCode; }
		}

		public IList<byte> ReplyBytes {
			get { return _replyBytes; }
		}
	}
}
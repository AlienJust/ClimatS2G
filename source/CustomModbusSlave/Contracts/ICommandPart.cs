using System.Collections.Generic;

namespace CustomModbusSlave.Contracts {
	public interface ICommandPart {
		byte Address { get; }
		byte CommandCode { get; }
		IList<byte> ReplyBytes { get; }
	}
}

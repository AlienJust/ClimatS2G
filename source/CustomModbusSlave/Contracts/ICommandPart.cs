using DataAbstractionLevel.Low.PsnConfig.Contracts;
using System.Collections.Generic;

namespace CustomModbusSlave.Contracts {
	public interface ICommandPart {
		byte Address { get; }
		byte CommandCode { get; }
		byte[] ReplyBytes { get; }
        IPsnProtocolCommandPartConfiguration PsnPartConfig { get; }
    }
}

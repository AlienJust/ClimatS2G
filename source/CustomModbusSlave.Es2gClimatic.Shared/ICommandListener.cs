using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared {
	public interface ICommandListener {
		void ReceiveCommand(byte addr, byte code, IList<byte> data);
	}
}
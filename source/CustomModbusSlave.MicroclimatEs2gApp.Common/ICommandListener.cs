using System.Collections.Generic;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common {
	public interface ICommandListener {
		void ReceiveCommand(byte addr, byte code, IList<byte> data);
	}
}
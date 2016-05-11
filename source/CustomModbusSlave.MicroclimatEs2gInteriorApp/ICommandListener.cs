using System.Collections.Generic;

namespace CustomModbusSlave.MicroclimatEs2gApp {
	internal interface ICommandListener {
		void ReceiveCommand(byte addr, byte code, IList<byte> data);
	}
}
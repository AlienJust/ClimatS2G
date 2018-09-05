using System.Collections.Generic;

namespace CustomModbusSlave.Contracts {
	public interface ICommandPartSearcher {
		void SearchForCommands(List<byte> incomingBuffer, ICommandPartFoundListener listener);
	}
}

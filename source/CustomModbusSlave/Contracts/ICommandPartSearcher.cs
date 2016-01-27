using System.Collections.Generic;

namespace CustomModbusSlave.Contracts {
	public interface ICommandPartSearcher {
		void SearchForCommands(IList<byte> incomingBuffer, ICommandPartFoundListener listener);
	}
}
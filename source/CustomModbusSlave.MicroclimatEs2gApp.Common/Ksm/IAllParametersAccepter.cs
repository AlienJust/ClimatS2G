using System.Collections.Generic;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	public interface IAllParametersAccepter {
		void AcceptCommandAllParameters(IList<byte> bytes);
	}
}
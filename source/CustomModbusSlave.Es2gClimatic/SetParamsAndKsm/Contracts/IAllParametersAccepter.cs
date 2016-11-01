using System.Collections.Generic;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.Contracts {
	public interface IAllParametersAccepter {
		void AcceptCommandAllParameters(IList<byte> bytes);
	}
}
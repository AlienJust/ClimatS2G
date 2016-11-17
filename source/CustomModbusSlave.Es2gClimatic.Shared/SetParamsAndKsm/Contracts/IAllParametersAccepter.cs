using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.Contracts {
	public interface IAllParametersAccepter {
		void AcceptCommandAllParameters(IList<byte> bytes);
	}
}
using System.Collections.Generic;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	internal interface IAllParametersAccepter {
		void AcceptCommandAllParameters(IList<byte> bytes);
	}
}
using System.Collections.Generic;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.UniversalParams {
	interface IGroup : IGroupItem {
		IList<IGroupItem> Children { get; }
	}
}

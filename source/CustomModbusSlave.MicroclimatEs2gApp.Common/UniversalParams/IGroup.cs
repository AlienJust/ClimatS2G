using System.Collections.Generic;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.UniversalParams {
	public interface IGroup : IGroupItem {
		IList<IGroupItem> Children { get; }
	}
}

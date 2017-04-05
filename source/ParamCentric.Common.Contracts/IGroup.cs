using System.Collections.Generic;

namespace ParamCentric.Common.Contracts {
	public interface IGroup : IGroupItem {
		IList<IGroupItem> Children { get; }
	}
}

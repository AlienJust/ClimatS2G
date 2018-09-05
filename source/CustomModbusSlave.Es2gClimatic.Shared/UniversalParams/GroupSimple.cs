using System.Collections.Generic;
using ParamCentric.Common.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams {
	public class GroupSimple : IGroup {
		public GroupSimple(string name, IList<IGroupItem> children) {
			Name = name;
			Children = children;
		}

		public string Name { get; }
		public IList<IGroupItem> Children { get; }
	}
}

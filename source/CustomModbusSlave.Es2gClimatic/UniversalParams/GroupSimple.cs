using System.Collections.Generic;
using CustomModbusSlave.MicroclimatEs2gApp.Common.UniversalParams;

namespace CustomModbusSlave.Es2gClimatic.UniversalParams {
	public class GroupSimple : IGroup {
		public GroupSimple(string name, IList<IGroupItem> children) {
			Name = name;
			Children = children;
		}

		public string Name { get; }
		public IList<IGroupItem> Children { get; }
	}
}
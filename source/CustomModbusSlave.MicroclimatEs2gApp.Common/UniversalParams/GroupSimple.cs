using System.Collections.Generic;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.UniversalParams {
	public class GroupSimple : IGroup {
		public GroupSimple(string name, IList<IGroupItem> children) {
			Name = name;
			Children = children;
		}

		public string Name { get; }
		public IList<IGroupItem> Children { get; }
	}
}
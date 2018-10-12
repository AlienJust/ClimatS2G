using System.Collections.Generic;

namespace ParamControls.Vm {
	public sealed class ParameterListViewModel : IDisplayParameter, IDisplayGroup {
		
		public string DisplayName { get; }
		public ParameterListViewModel(string displayName, IList<IDisplayParameter> groupItems) {
			DisplayName = displayName;
			GroupItems = groupItems;
		}

		public IList<IDisplayParameter> GroupItems { get; }
	}

	public interface IDisplayGroup {
		IList<IDisplayParameter> GroupItems { get; }
	}
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ParamControls.Vm;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm {
	public sealed class GroupParamViewModel : IDisplayParameter, IDisplayGroup {
		public string DisplayName { get; }

		public GroupParamViewModel(string displayName, IEnumerable<IDisplayParameter> groupItems) {
			DisplayName = displayName;

			GroupItems = new ObservableCollection<IDisplayParameter>();
			if (groupItems != null) {
				foreach (var item in groupItems) {
					GroupItems.Add(item);
				}
			}
		}

		public ObservableCollection<IDisplayParameter> GroupItems { get; }
	}
}
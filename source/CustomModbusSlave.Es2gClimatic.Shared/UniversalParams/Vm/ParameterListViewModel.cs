using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ParamControls.Vm {
	public sealed class ParameterListViewModel : IChartReadyParameterVm, IDisplayGroup {
		
		public string DisplayName { get; }
		public string FullName { get; }

		public ParameterListViewModel(string fullNamePreffix, string displayName, IEnumerable<IChartReadyParameterVm> groupItems) {
			FullName = fullNamePreffix + ": displayName";
			DisplayName = displayName;
			
			GroupItems = new ObservableCollection<IChartReadyParameterVm>();
			foreach (var item in groupItems) {
				GroupItems.Add(item);
			}
		}

		public ObservableCollection<IChartReadyParameterVm> GroupItems { get; }

		// TODO: implement or not?
		public double ChartValue { get; }
		public bool IsChecked { get; set; }
		public IDisplayParameter DisplayParameter { get; }
		public IList<IDisplayParameter> DisplayParameters { get; }
	}
}

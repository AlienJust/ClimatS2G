using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ParamControls.Vm {
	public sealed class ParameterListViewModel : IChartReadyParameterViewModel, IDisplayGroup {
		
		public string DisplayName { get; }
		//public string UniqueName { get; }

		public ParameterListViewModel(string displayName, IEnumerable<IChartReadyParameterViewModel> groupItems) {
			//UniqueName = fullNamePreffix + ": " + displayName";
			DisplayName = displayName;
			
			GroupItems = new ObservableCollection<IChartReadyParameterViewModel>();
			foreach (var item in groupItems) {
				GroupItems.Add(item);
			}
		}

		public ObservableCollection<IChartReadyParameterViewModel> GroupItems { get; }

		// TODO: implement or not?
		public double ChartValue { get; }
		public bool IsChecked { get; set; }
		public IDisplayParameter DisplayParameter { get; }
		public IList<IDisplayParameter> DisplayParameters { get; }
	}
}

using System.Collections.Generic;

namespace ParamControls.Vm {
	public sealed class ParameterListViewModel : IChartReadyParameter, IDisplayGroup {
		
		public string DisplayName { get; }
		public ParameterListViewModel(string displayName, IList<IChartReadyParameter> groupItems) {
			DisplayName = displayName;
			GroupItems = groupItems;
		}

		public IList<IChartReadyParameter> GroupItems { get; }

		// TODO: implement or not?
		public double ChartValue { get; }
		public bool IsChecked { get; set; }
		public IDisplayParameter DisplayParameter { get; }
		public IList<IDisplayParameter> DisplayParameters { get; }
	}

	public interface IDisplayGroup {
		IList<IChartReadyParameter> GroupItems { get; }
	}
}

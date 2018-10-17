using System.Collections.ObjectModel;

namespace ParamControls.Vm {
	public interface IDisplayGroup : IDisplayParameter {
		ObservableCollection<IChartReadyParameterViewModel> GroupItems { get; }
	}
}
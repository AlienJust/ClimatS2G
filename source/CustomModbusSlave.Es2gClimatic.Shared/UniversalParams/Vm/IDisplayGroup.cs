using System.Collections.ObjectModel;

namespace ParamControls.Vm {
	public interface IDisplayGroup : IDisplayParameter {
		ObservableCollection<IChartReadyParameterVm> GroupItems { get; }
	}
}
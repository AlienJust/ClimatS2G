using System.Collections.ObjectModel;

namespace ParamControls.Vm {
	public interface IDisplayGroup : IDisplayParameter {
		ObservableCollection<IDisplayParameter> GroupItems { get; }
	}
}
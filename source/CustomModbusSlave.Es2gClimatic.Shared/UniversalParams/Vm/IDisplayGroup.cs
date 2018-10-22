using System.Collections.ObjectModel;
using ParamControls.Vm;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm {
	public interface IDisplayGroup : IDisplayParameter {
		bool IsGroupExpanded { get; set; }
		
		ObservableCollection<IDisplayParameter> ParametersAndGroups { get; }
		void AddParameterOrGroup(IDisplayParameter parameter);
	}
}
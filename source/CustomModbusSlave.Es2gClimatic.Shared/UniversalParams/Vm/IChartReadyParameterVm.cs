using System.Collections.Generic;

namespace ParamControls.Vm {
	public interface IChartReadyParameterVm : IDisplayParameter {
		bool IsChecked { get; set; }
		IList<IDisplayParameter> DisplayParameters { get; } //
	}
}
using System.Collections.Generic;

namespace ParamControls.Vm {
	public interface IChartReadyParameterViewModel : IDisplayParameter {
		bool IsChecked { get; set; }
		IList<IDisplayParameter> DisplayParameters { get; } //
	}
}
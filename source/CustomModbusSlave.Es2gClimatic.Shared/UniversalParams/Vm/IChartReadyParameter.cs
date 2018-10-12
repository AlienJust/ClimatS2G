using System.Collections.Generic;

namespace ParamControls.Vm {
	//public interface IChartReadyParameter<out T> : IChartReadyParameter, IDisplayParameter<T> {
		//IDisplayParameter<T> DisplayParameter { get; }
	//}

	public interface IChartReadyParameter {
		double ChartValue { get; }
		bool IsChecked { get; set; }
		IDisplayParameter DisplayParameter { get; }
		IList<IDisplayParameter> DisplayParameters { get; }
	}
}
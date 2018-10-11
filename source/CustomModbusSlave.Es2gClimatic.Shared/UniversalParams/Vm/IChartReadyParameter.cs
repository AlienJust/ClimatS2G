﻿namespace ParamControls.Vm {
	public interface IChartReadyParameter<out T> : IChartReadyParameter, IDisplayParameter<T> {
		IDisplayParameter<T> DisplayParameter { get; }
	}

	public interface IChartReadyParameter : IDisplayParameter {
		double ChartValue { get; }
		bool IsChecked { get; set; }
	}
}
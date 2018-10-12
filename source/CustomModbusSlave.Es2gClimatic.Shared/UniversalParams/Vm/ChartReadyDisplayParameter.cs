using System;
using System.Collections.Generic;
using AlienJust.Support.ModelViewViewModel;

namespace ParamControls.Vm {
	public sealed class ChartReadyDisplayParameter<T> : ViewModelBase, IChartReadyParameter where T : IEquatable<T> {
		private readonly IDisplayParameter<T> _parameter;
		private readonly IList<IDisplayParameter> _parameters;
		private readonly Func<T, double> _chartDataGetter;
		private readonly Action<bool, IChartReadyParameter> _isCheckedChanged;
		private bool _isChecked;

		/*public ChartReadyDisplayParameter(string displayName, IReceivableParameter parameterModel, IThreadNotifier uiNotifier, Func<IList<byte>, T> displayValueGetter, T fallbackValue, Func<T, double> chartDataGetter, Action<bool, IChartReadyParameter> isCheckedChanged) : base(displayName, parameterModel, uiNotifier, displayValueGetter, fallbackValue) {
			_chartDataGetter = chartDataGetter;
			_isCheckedChanged = isCheckedChanged;
			_isChecked = false;
		}*/

		public ChartReadyDisplayParameter(IDisplayParameter<T> parameter, Func<T, double> chartDataGetter, Action<bool, IChartReadyParameter> isCheckedChanged) {
			_parameter = parameter;
			_chartDataGetter = chartDataGetter;
			_isCheckedChanged = isCheckedChanged;

			_parameters= new List<IDisplayParameter>{_parameter};
			_isChecked = false;
		}

		public double ChartValue => _chartDataGetter.Invoke(_parameter.DisplayValue);

		public bool IsChecked {
			get => _isChecked;
			set {
				if (_isChecked != value) {
					_isChecked = value;
					RaisePropertyChanged(()=>IsChecked);
					_isCheckedChanged(value, this);
				}
			}
		}

		public IDisplayParameter DisplayParameter => _parameter;
		public IList<IDisplayParameter> DisplayParameters => _parameters;
	}
}
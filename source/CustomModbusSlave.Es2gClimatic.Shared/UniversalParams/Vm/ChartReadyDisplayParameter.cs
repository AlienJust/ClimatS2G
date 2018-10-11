using System;
using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;

namespace ParamControls.Vm {
	public sealed class ChartReadyDisplayParameter<T> : RelayParameterViewModel<T>, IChartReadyParameter where T : IEquatable<T> {
		private readonly Func<T, double> _chartDataGetter;
		private readonly Action<bool, IChartReadyParameter> _isCheckedChanged;
		private readonly double _chartValue;
		private bool _isChecked;

		public ChartReadyDisplayParameter(string displayName, IReceivableParameter parameterModel, IThreadNotifier uiNotifier, Func<IList<byte>, T> displayValueGetter, T fallbackValue, Func<T, double> chartDataGetter, Action<bool, IChartReadyParameter> isCheckedChanged) : base(displayName, parameterModel, uiNotifier, displayValueGetter, fallbackValue) {
			_chartDataGetter = chartDataGetter;
			_isCheckedChanged = isCheckedChanged;
			_isChecked = false;
		}

		public double ChartValue => _chartDataGetter.Invoke(DisplayValue);

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
	}
}
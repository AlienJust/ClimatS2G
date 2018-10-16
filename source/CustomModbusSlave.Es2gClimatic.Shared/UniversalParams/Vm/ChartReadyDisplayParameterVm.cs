using System;
using System.Collections.Generic;
using AlienJust.Support.ModelViewViewModel;
using DrillingRig.ConfigApp.AppControl.ParamLogger;

namespace ParamControls.Vm {
	public sealed class ChartReadyDisplayParameterVm<T> : ViewModelBase, IChartReadyParameterVm where T : IEquatable<T> {
		private readonly IDisplayParameter<T> _parameter;
		private readonly Func<T, double> _chartDataGetter;
		private readonly ParameterLogType _parameterLogType; // holding link for further proper unsubscribing
		private readonly IParameterLogger _parameterLogger;
		private bool _isChecked;

		public IList<IDisplayParameter> DisplayParameters { get; }
		public string DisplayName => _parameter.DisplayName;
		public string FullName => _parameter.FullName;

		public ChartReadyDisplayParameterVm(IDisplayParameter<T> parameter, Func<T, double> chartDataGetter, ParameterLogType parameterLogType, IParameterLogger parameterLogger) {
			_parameter = parameter;
			_chartDataGetter = chartDataGetter;
			_parameterLogType = parameterLogType;
			_parameterLogger = parameterLogger;

			DisplayParameters= new List<IDisplayParameter>{_parameter};
			_isChecked = false;

			switch (_parameterLogType) {
				case ParameterLogType.Analogue:
					_parameter.DisplayParameterValueMaybeChanged += ParameterOnDisplayParameterValueMaybeChanged;
					break;
				case ParameterLogType.Discrete:
					_parameter.DisplayParameterValueMaybeChanged += ParameterOnDisplayParameterValueMaybeChangedBool;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(parameterLogType), parameterLogType, null);
			}
			
		}

		private void ParameterOnDisplayParameterValueMaybeChanged() {
			if (_isChecked) {
				try {
					_parameterLogger.LogAnalogueParameter(FullName, ChartValue);
				}
				catch (Exception e) {
					Console.WriteLine(e);
				}
				
			}
		}

		private void ParameterOnDisplayParameterValueMaybeChangedBool() {
			if (_isChecked) {
				try {
					_parameterLogger.LogDiscreteParameter(FullName, ChartValue > 0.5);
				}
				catch (Exception e) {
					Console.WriteLine(e);
				}
			}
		}

		public double ChartValue {
			get {
				if (_parameter.IsValueFallbackOrUnknown) throw new Exception("Parameter is not ready to be charted");
				return _chartDataGetter.Invoke(_parameter.DisplayValue);
			}
		}

		public bool IsChecked {
			get => _isChecked;
			set {
				if (_isChecked != value) {
					_isChecked = value;
					RaisePropertyChanged(()=>IsChecked);
				}
			}
		}
	}

	public enum ParameterLogType {
		Analogue,
		Discrete
	}
}
using System;
using System.Collections.Generic;
using AlienJust.Support.ModelViewViewModel;
using DrillingRig.ConfigApp.AppControl.ParamLogger;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm {
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TR">Received data type</typeparam>
	/// <typeparam name="TD">Display data type</typeparam>
	public sealed class ChartParamViewModel<TR, TD> : ViewModelBase, IChartReadyParameterViewModel /*where TD : IEquatable<TD>*/ {
		private readonly string _uniqNamePrefix;
		private readonly IRecvParam<TR> _recvParam;
		private readonly IDisplayParameter<TD> _parameter;
		private readonly Func<TR, double> _chartDataGetter;
		private readonly ParameterLogType _parameterLogType; // TODO: holding link for further proper unsubscribe in later
		private readonly IParameterLogger _parameterLogger;
		private bool _isChecked;

		public IList<IDisplayParameter> DisplayParameters { get; }
		public string DisplayName => _uniqNamePrefix + ": " + _parameter.DisplayName;

		public ChartParamViewModel(IRecvParam<TR> recvParam, IDisplayParameter<TD> parameter, Func<TR, double> chartDataGetter, ParameterLogType parameterLogType, IParameterLogger parameterLogger, params string[] uniqNamePrefix) {
			_uniqNamePrefix = string.Join(", ", uniqNamePrefix);
			_recvParam = recvParam;
			_parameter = parameter;
			_chartDataGetter = chartDataGetter;
			_parameterLogType = parameterLogType;
			_parameterLogger = parameterLogger;

			DisplayParameters = new List<IDisplayParameter> {_parameter};
			_isChecked = false;


			switch (_parameterLogType) {
				case ParameterLogType.Analogue:
					_recvParam.NotifyDataReceived += ParameterOnDisplayParameterValueMaybeChanged;
					break;
				case ParameterLogType.Discrete:
					_recvParam.NotifyDataReceived += ParameterOnDisplayParameterValueMaybeChangedBool;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(parameterLogType), parameterLogType, null);
			}
		}

		private void ParameterOnDisplayParameterValueMaybeChanged() {
			if (_isChecked) {
				try {
					_parameterLogger.LogAnalogueParameter(DisplayName, ChartValue);
				}
				catch (Exception e) {
					Console.WriteLine("CANNOT LOG ANALOGUE PARAMETER: exception=" + e);
				}
			}
		}

		private void ParameterOnDisplayParameterValueMaybeChangedBool() {
			if (_isChecked) {
				try {
					_parameterLogger.LogDiscreteParameter(DisplayName, ChartValue > 0.5);
				}
				catch (Exception e) {
					Console.WriteLine("CANNOT LOG DISCRETE PARAMETER: exception=" + e);
				}
			}
		}

		private double ChartValue {
			get {
				try {
					return _chartDataGetter.Invoke(_recvParam.ReceivedRawValue); // Invoked in receiving thread!!!1
				}
				catch (Exception e) {
					throw new Exception("Parameter is not ready to be charted", e);
				}
			}
		}

		public bool IsChecked {
			get => _isChecked;
			set {
				if (_isChecked != value) {
					_isChecked = value;
					RaisePropertyChanged(() => IsChecked);
				}
			}
		}
	}
}
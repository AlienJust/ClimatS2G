using System;
using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;

namespace ParamControls.Vm {
	public sealed class RelayParameterBlocking : IReceivableParameter {
		private readonly Func<IList<byte>, IList<byte>> _dataGetter;
		private IList<byte> _receivedRawValue;
		private readonly object _receivedRawValueSyncObj;

		public IList<byte> ReceivedRawValue {
			get { lock(_receivedRawValueSyncObj)return _receivedRawValue; }
			private set { lock(_receivedRawValueSyncObj)_receivedRawValue = value; }
		}

		public event NotifyDataReceivedDelegate NotifyDataReceived;

		public RelayParameterBlocking(Func<IList<byte>, IList<byte>> dataGetter) {
			_receivedRawValueSyncObj = new object();
			_dataGetter = dataGetter;
		}
		
		public void SetReceivedValueFromCommandPart(IList<byte> commandPartDataWithoutHeaderAndCrc) {
			ReceivedRawValue = _dataGetter(commandPartDataWithoutHeaderAndCrc); // TODO: Invoke when received, could lag driver thread!
			NotifyDataReceived?.Invoke();
		}
	}


	public sealed class RelayParameterViewModel<T> : ViewModelBase, IDisplayParameter<T> where T: IEquatable<T> {
		private readonly IReceivableParameter _parameterModel;
		private readonly IThreadNotifier _uiNotifier;
		private readonly Func<IList<byte>, T> _displayValueGetter;
		private T _displayValue;

		public RelayParameterViewModel(string displayName, IReceivableParameter parameterModel, IThreadNotifier uiNotifier, Func<IList<byte>, T> displayValueGetter) {
			DisplayName = displayName;
			_parameterModel = parameterModel;
			_uiNotifier = uiNotifier;
			_displayValueGetter = displayValueGetter;
			_parameterModel.NotifyDataReceived += ParameterModelOnNotifyDataReceived;
		}

		private void ParameterModelOnNotifyDataReceived() {
			_uiNotifier.Notify(()=>DisplayValue = _displayValueGetter(_parameterModel.ReceivedRawValue)); // TODO: many calls could lag interface!
		}

		public string DisplayName { get; }

		public T DisplayValue {
			get => _displayValue;
			set {
				if (!_displayValue.Equals(value)) {
					_displayValue = value;
					RaisePropertyChanged(() => DisplayValue);
				}
			}
		}
	}


	public interface IDisplayParameter {
		string DisplayName { get; }
	}

	public interface IDisplayParameter<out T> : IDisplayParameter {
		T DisplayValue { get; }
	}

	public interface IChartReadyParameter : IReceivableParameter {
		double ChartValue { get; }
	}
	/*
	class ChartReadyParameterSimple : IChartReadyParameter {
		private readonly IReceivableParameter _receivableParameter;
		private readonly Func<IList<byte>, double> _chartDataFunc;
		public ChartReadyParameterSimple(IReceivableParameter receivableParameter, Func<IList<byte>, double> chartDataFunc) {
			_receivableParameter = receivableParameter;
			_chartDataFunc = chartDataFunc;
		}

		public void SetReceivedValueFromCommandPart(IList<byte> commandPartDataWithoutHeaderAndCrc) {
			_receivableParameter.SetReceivedValueFromCommandPart(commandPartDataWithoutHeaderAndCrc);
			_receivableParameter.NotifyDataReceived += ReceivableParameterOnNotifyDataReceived;
		}

		private void ReceivableParameterOnNotifyDataReceived() {
			NotifyDataReceived?.Invoke();
		}

		public IList<byte> ReceivedRawValue => _receivableParameter.ReceivedRawValue;
		public event NotifyDataReceivedDelegate NotifyDataReceived;

		public double ChartValue => _chartDataFunc.Invoke(ReceivedRawValue);
	}
	*/



	public delegate void NotifyDataReceivedDelegate();


	public interface IReceivableParameter {
		void SetReceivedValueFromCommandPart(IList<byte> commandPartDataWithoutHeaderAndCrc); // DriverSide

		IList<byte> ReceivedRawValue { get; } // UserSide
		event NotifyDataReceivedDelegate NotifyDataReceived; // UserSide
	}
}

using System;
using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;

namespace ParamControls.Vm {
	public class RelayParameterViewModel<T> : ViewModelBase, IDisplayParameter<T> where T : IEquatable<T> {
		private readonly IReceivableParameter _parameterModel;
		private readonly IThreadNotifier _uiNotifier;
		private readonly Func<IList<byte>, T> _displayValueGetter;
		private readonly T _fallbackValue;
		private T _displayValue;

		public RelayParameterViewModel(string displayName, IReceivableParameter parameterModel, IThreadNotifier uiNotifier, Func<IList<byte>, T> displayValueGetter, T fallbackValue) {
			DisplayName = displayName;
			_parameterModel = parameterModel;
			_uiNotifier = uiNotifier;
			_displayValueGetter = displayValueGetter;
			_fallbackValue = fallbackValue;
			_parameterModel.NotifyDataReceived += ParameterModelOnNotifyDataReceived;
		}

		private void ParameterModelOnNotifyDataReceived() {
			Console.WriteLine("Parameter received from _parameterModel");
			_uiNotifier.Notify(() => {
				try {
					DisplayValue = _displayValueGetter(_parameterModel.ReceivedRawValue);
				}
				catch (Exception e) {
					Console.WriteLine(e);
					DisplayValue = _fallbackValue;
				}

			}); // TODO: many calls could lag interface!
		}

		public string DisplayName { get; }

		public T DisplayValue {
			get => _displayValue;
			set {
				try {
					if (!_displayValue.Equals(value)) {
						_displayValue = value;
						RaisePropertyChanged(() => DisplayValue);
					}
				}
				catch (Exception e) {
					Console.WriteLine(e);
					//throw;
				}
			}
		}
	}
}
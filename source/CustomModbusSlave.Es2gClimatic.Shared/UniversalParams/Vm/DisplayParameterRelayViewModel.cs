using System;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;

namespace ParamControls.Vm {
	public class DisplayParameterRelayViewModel<T> : ViewModelBase, IDisplayParameter<T> where T : IEquatable<T> {
		private readonly IReceivableParameter _parameterModel;
		private readonly IThreadNotifier _uiNotifier;
		
		private readonly Func<IList<byte>, T> _displayValueGetter;
		private T _displayValue;
		
		private bool _isValueFallback;
		private readonly T _fallbackValue;
		
		private bool _isValueUnknown;
		private T _unknownValue;

		public string FullName { get; }
		public string DisplayName { get; }

		public DisplayParameterRelayViewModel(string fullNamePreffix, string displayName, IReceivableParameter parameterModel, IThreadNotifier uiNotifier, Func<IList<byte>, T> displayValueGetter, T fallbackValue, T unknownValue) {
			FullName = fullNamePreffix + ": " + displayName;
			DisplayName = displayName;
			
			_parameterModel = parameterModel;
			_uiNotifier = uiNotifier;
			_displayValueGetter = displayValueGetter;
			_fallbackValue = fallbackValue;

			_unknownValue = unknownValue;
			_displayValue = unknownValue;
			_isValueUnknown = true;

			_isValueFallback = false;
			
			_parameterModel.NotifyDataReceived += ParameterModelOnNotifyDataReceived;
		}

		private void ParameterModelOnNotifyDataReceived() {
			_uiNotifier.Notify(() => {
				try {
					DisplayValue = _displayValueGetter(_parameterModel.ReceivedRawValue);
					Console.WriteLine(FullName + " raw is " + _parameterModel.ReceivedRawValue.ToText());
					Console.WriteLine(FullName + " v is " + DisplayValue + " and t is " + typeof(T).FullName);
				}
				catch (Exception e) {
					Console.WriteLine(e);
					DisplayValue = _fallbackValue;
				}
			}); // TODO: many calls could lag interface!
		}


		public T DisplayValue {
			get => _displayValue;
			set {
				try {
					if (_displayValue == null) {
						if (value != null) {
							_displayValue = value;
							RaisePropertyChanged(() => DisplayValue);
						}
					}
					else if (!_displayValue.Equals(value)) {
						_displayValue = value;
						RaisePropertyChanged(() => DisplayValue);
					}
					DisplayParameterValueMaybeChanged?.Invoke();
					IsValueFallback = false; 
					IsValueUnknown = false;
				}
				catch(Exception ex) {
					DisplayValue = _fallbackValue;
					IsValueFallback = true;
					IsValueUnknown = false;
					Console.WriteLine(ex);
				}
			}
		}


		public bool IsValueFallback {
			get => _isValueFallback;
			set {
				if (_isValueFallback != value) {
					_isValueFallback = value;
					RaisePropertyChanged(()=>IsValueFallback);
					RaisePropertyChanged(()=>IsValueFallbackOrUnknown);
					RaisePropertyChanged(()=>IsValueNotFallbackAndNotUnknown);
					RaisePropertyChanged((()=>BackgroundColor));
				}
			}
		}
		
		public bool IsValueUnknown {
			get => _isValueUnknown;
			set {
				if (_isValueUnknown != value) {
					_isValueUnknown = value;
					RaisePropertyChanged(()=>IsValueUnknown);
					RaisePropertyChanged(()=>IsValueFallbackOrUnknown);
					RaisePropertyChanged(()=>IsValueNotFallbackAndNotUnknown);
					RaisePropertyChanged((()=>BackgroundColor));
				}
			}
		}

		public bool IsValueFallbackOrUnknown => _isValueFallback || _isValueUnknown;
		public bool IsValueNotFallbackAndNotUnknown => !IsValueFallbackOrUnknown;

		public void SetValueIsUnknown() {
			IsValueUnknown = true;
			IsValueFallback = false;
			DisplayValue = _unknownValue;
		}

		public Colors BackgroundColor {
			get {
				if (_isValueFallback) return Colors.PaleVioletRed;
				if (_isValueUnknown) return Colors.Yellow;
				return Colors.Transparent;
			}
		}

		public event DisplayParameterValueMaybeChangedDelegate DisplayParameterValueMaybeChanged;
	}
}
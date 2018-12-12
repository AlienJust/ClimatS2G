using System;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm {
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TDisplay">Type of display data</typeparam>
	/// <typeparam name="TReceive">Type of received data</typeparam>
	public class DispParamViewModel<TDisplay, TReceive> : ViewModelBase, IDisplayParameter<TDisplay> where TDisplay : IEquatable<TDisplay> {
		private readonly IRecvParam<TReceive> _parameterModel;
		private readonly IThreadNotifier _uiNotifier;
		
		private readonly Func<TReceive, TDisplay> _displayValueGetter;
		private TDisplay _displayValue;
		
		private bool _isValueFallback;
		private readonly TDisplay _fallbackValue;
		
		private bool _isValueUnknown;
		private readonly TDisplay _unknownValue;

		//public string UniqueName { get; }
		public string DisplayName { get; }

		public DispParamViewModel(string displayName, IRecvParam<TReceive> parameterModel, IThreadNotifier uiNotifier, Func<TReceive, TDisplay> displayValueGetter, TDisplay fallbackValue, TDisplay unknownValue) {
			//UniqueName = fullNamePreffix + ": " + displayName;
			//DisplayName = uniqNamePrefix + ": " + displayName;
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
				}
				catch {
					DisplayValue = _fallbackValue;
				}
			}); // TODO: many calls could lag interface!
		}


		public TDisplay DisplayValue {
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
					//DisplayParameterValueMaybeChanged?.Invoke();
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

		//public event DisplayParameterValueMaybeChangedDelegate DisplayParameterValueMaybeChanged;
	}
}
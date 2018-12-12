using System;
using System.Windows.Input;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm {
	public class DispParamSettableViewModel<TDisplay, TRaw, TDisplaySet> : ViewModelBase, IDisplayParameter<TDisplay>, IDispsetParameter<TDisplaySet> where TDisplay : IEquatable<TDisplay> {
		private readonly IRecvParam<TRaw> _recvModel;

		private readonly IThreadNotifier _uiNotifier;

		private readonly Func<TRaw, TDisplay> _displayValueGetter;
		private TDisplay _displayValue;

		private bool _isValueFallback;
		private readonly TDisplay _fallbackValue;

		private bool _isValueUnknown;
		private readonly TDisplay _unknownValue;
		private readonly Action<TDisplaySet> _sendValueValidator;
		public string DisplayName { get; }


		private TDisplaySet _dispsetValue;

		private bool _isDispsetValueFallback;
		private readonly TDisplaySet _fallbackDispsetValue;

		private bool _isDispsetValueUnknown;
		private readonly TDisplaySet _unknownDispsetValue;
		private readonly Action _setParameter;
		private readonly DependedCommand _setCommand;

		public DispParamSettableViewModel(string displayName, IRecvParam<TRaw> recvModel, IThreadNotifier uiNotifier, Func<TRaw, TDisplay> displayValueGetter, TDisplay fallbackValue, TDisplay unknownValue, Action<TDisplaySet> sendValueValidator, TDisplaySet fallbackDispsetValue, TDisplaySet unknownDispsetValue, Action setParameter) {
			DisplayName = displayName;

			_recvModel = recvModel;
			_uiNotifier = uiNotifier;
			_displayValueGetter = displayValueGetter;

			_fallbackValue = fallbackValue;
			_unknownValue = unknownValue;
			_displayValue = unknownValue;
			_isValueUnknown = true;
			_isValueFallback = false;


			_sendValueValidator = sendValueValidator;
			_fallbackDispsetValue = fallbackDispsetValue;
			_unknownDispsetValue = unknownDispsetValue;
			_dispsetValue = unknownDispsetValue;
			_isDispsetValueUnknown = true;
			_isDispsetValueFallback = false;

			_setParameter = setParameter;
			_setCommand = new DependedCommand(Set, () => IsDispsetValueNotFallbackAndNotUnknown);
			_setCommand.AddDependOnProp(this, () => IsDispsetValueNotFallbackAndNotUnknown);


			_recvModel.NotifyDataReceived += ParameterModelOnNotifyDataReceived;
		}

		private void Set() {
			_setParameter.Invoke();
		}

		private void ParameterModelOnNotifyDataReceived() {
			_uiNotifier.Notify(() => {
				try {
					DisplayValue = _displayValueGetter(_recvModel.ReceivedRawValue);
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
				catch (Exception ex) {
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
					RaisePropertyChanged(() => IsValueFallback);
					RaisePropertyChanged(() => IsValueFallbackOrUnknown);
					RaisePropertyChanged(() => IsValueNotFallbackAndNotUnknown);
					RaisePropertyChanged(() => BackgroundColor);
				}
			}
		}

		public bool IsValueUnknown {
			get => _isValueUnknown;
			set {
				if (_isValueUnknown != value) {
					_isValueUnknown = value;
					RaisePropertyChanged(() => IsValueUnknown);
					RaisePropertyChanged(() => IsValueFallbackOrUnknown);
					RaisePropertyChanged(() => IsValueNotFallbackAndNotUnknown);
					RaisePropertyChanged(() => BackgroundColor);
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


		public TDisplaySet DispsetValue {
			get => _dispsetValue;
			set {
				try {
					if (_dispsetValue == null) {
						if (value != null) {
							_dispsetValue = value;
							_sendValueValidator.Invoke(value); // if throws - we fallback
							
							RaisePropertyChanged(() => DispsetValue);
						}
					}
					else if (!_dispsetValue.Equals(value)) {
						_dispsetValue = value;
						_sendValueValidator.Invoke(value); // if throws - we fallback
						RaisePropertyChanged(() => DispsetValue);
					}

					//DisplayParameterValueMaybeChanged?.Invoke();
					IsDispsetValueFallback = false;
					IsDispsetValueUnknown = false;
				}
				catch (Exception ex) {
					_dispsetValue = _fallbackDispsetValue;
					RaisePropertyChanged(() => DispsetValue);
					IsDispsetValueFallback = true;
					IsDispsetValueUnknown = false;
					Console.WriteLine(ex);
				}
			}
		}


		public bool IsDispsetValueFallback {
			get => _isDispsetValueFallback;
			set {
				if (_isDispsetValueFallback != value) {
					_isDispsetValueFallback = value;
					RaisePropertyChanged(() => IsDispsetValueFallback);
					RaisePropertyChanged(() => IsDispsetValueFallbackOrUnknown);
					RaisePropertyChanged(() => IsDispsetValueNotFallbackAndNotUnknown);
				}
			}
		}

		public bool IsDispsetValueUnknown {
			get => _isDispsetValueUnknown;
			set {
				if (_isDispsetValueUnknown != value) {
					_isDispsetValueUnknown = value;
					RaisePropertyChanged(() => IsDispsetValueUnknown);
					RaisePropertyChanged(() => IsDispsetValueFallbackOrUnknown);
					RaisePropertyChanged(() => IsDispsetValueNotFallbackAndNotUnknown);
				}
			}
		}

		public bool IsDispsetValueFallbackOrUnknown => _isDispsetValueFallback || _isDispsetValueUnknown;
		public bool IsDispsetValueNotFallbackAndNotUnknown => !IsDispsetValueFallbackOrUnknown;


		public ICommand SetCommand => _setCommand;
	}
}
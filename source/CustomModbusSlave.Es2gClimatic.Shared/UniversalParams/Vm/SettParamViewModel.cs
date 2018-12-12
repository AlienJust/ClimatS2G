using System;
using System.Windows.Input;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm {
	public class SettParamViewModel<TDisplaySet> : ViewModelBase, IDispsetParameter<TDisplaySet> {

		private readonly IThreadNotifier _uiNotifier;
		
		private readonly Action<TDisplaySet> _sendValueValidator;
		public string DisplayName { get; }

		private TDisplaySet _dispsetValue;

		private bool _isDispsetValueFallback;
		private readonly TDisplaySet _fallbackDispsetValue;

		private bool _isDispsetValueUnknown;
		private readonly TDisplaySet _unknownDispsetValue;
		private readonly Action<TDisplaySet, Action<Exception>> _asyncParamSetter;
		private readonly DependedCommand _setCommand;
		private Colors _backgroundColor;

		public SettParamViewModel(string displayName, IThreadNotifier uiNotifier, Action<TDisplaySet> sendValueValidator, TDisplaySet fallbackDispsetValue, TDisplaySet unknownDispsetValue, Action<TDisplaySet, Action<Exception>> asyncParamSetter) {
			DisplayName = displayName;

			_uiNotifier = uiNotifier;

			_sendValueValidator = sendValueValidator;
			_fallbackDispsetValue = fallbackDispsetValue;
			_unknownDispsetValue = unknownDispsetValue;
			_dispsetValue = unknownDispsetValue;
			_isDispsetValueUnknown = true;
			_isDispsetValueFallback = false;

			_backgroundColor = Colors.Transparent;

			_asyncParamSetter = asyncParamSetter;
			_setCommand = new DependedCommand(Set, () => IsDispsetValueNotFallbackAndNotUnknown);
			_setCommand.AddDependOnProp(this, () => IsDispsetValueNotFallbackAndNotUnknown);
		}

		private void Set() {
			// TODO: check thread safity:
			_asyncParamSetter.Invoke(DispsetValue, ex => {
				if (ex != null) _uiNotifier.Notify(()=>BackgroundColor = Colors.OrangeRed);
				else _uiNotifier.Notify(()=>BackgroundColor = Colors.LimeGreen);
			});
		}

		public Colors BackgroundColor {
			get => _backgroundColor;
			set {
				if (_backgroundColor != value) {
					_backgroundColor = value;
					RaisePropertyChanged(()=>BackgroundColor);
				}
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
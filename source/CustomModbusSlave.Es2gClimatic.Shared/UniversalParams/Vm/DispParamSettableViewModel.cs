using System;
using System.Windows.Input;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm
{
    public class DispParamSettableViewModel<TDisplay, TRaw, TSet> : ViewModelBase, IDisplayAndSettableParameter<TDisplay, TSet> where TDisplay : IEquatable<TDisplay>
    {
        private readonly IRecvParam<TRaw> _recvModel;

        private readonly IThreadNotifier _uiNotifier;

        private readonly Func<TRaw, TDisplay> _displayValueGetter;
        private TDisplay _displayValue;

        private bool _isValueFallback;
        private readonly TDisplay _fallbackValue;

        private bool _isValueUnknown;
        private readonly TDisplay _unknownValue;
        private readonly Action<TSet> _sendValueValidator;
        public string DisplayName { get; }


        private TSet _settValue;

        private bool _isSettValueFallback;
        private readonly TSet _fallbackSettValue;

        private bool _isSettValueUnknown;
        private readonly TSet _unknownSettValue;
        private readonly Action<TSet, Action<Exception>> _asyncParamSetter;
        private readonly DependedCommand _setCommand;

        private Colors _backgroundColor;

        public DispParamSettableViewModel(
            string displayName,
            IRecvParam<TRaw> recvModel,
            IThreadNotifier uiNotifier,
            Func<TRaw, TDisplay> displayValueGetter,
            TDisplay fallbackValue,
            TDisplay unknownValue,
            Action<TSet> sendValueValidator,
            TSet fallbackSettValue,
            TSet unknownSettValue,
            Action<TSet, Action<Exception>> asyncParamSetter)
        {
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
            _fallbackSettValue = fallbackSettValue;
            _unknownSettValue = unknownSettValue;
            _settValue = unknownSettValue;
            _isSettValueUnknown = true;
            _isSettValueFallback = false;

            _backgroundColor = Colors.Transparent;

            _asyncParamSetter = asyncParamSetter;
            _setCommand = new DependedCommand(Set, () => IsSettValueNotFallbackAndNotUnknown);
            _setCommand.AddDependOnProp(this, () => IsSettValueNotFallbackAndNotUnknown);


            _recvModel.NotifyDataReceived += ParameterModelOnNotifyDataReceived;
        }

        private void Set()
        {
            BackgroundColor = Colors.Yellow;
            // TODO: check thread safety:
            _asyncParamSetter.Invoke(SettValue, ex =>
            {
                if (ex != null) _uiNotifier.Notify(() => BackgroundColor = Colors.OrangeRed);
                else _uiNotifier.Notify(() => BackgroundColor = Colors.LimeGreen);
            });
        }

        private void ParameterModelOnNotifyDataReceived()
        {
            _uiNotifier.Notify(() =>
            {
                try
                {
                    DisplayValue = _displayValueGetter(_recvModel.ReceivedRawValue);
                }
                catch
                {
                    DisplayValue = _fallbackValue;
                }
            }); // TODO: many calls could lag interface!
        }

        public TDisplay DisplayValue
        {
            get => _displayValue;
            set
            {
                try
                {
                    if (_displayValue == null)
                    {
                        if (value != null)
                        {
                            _displayValue = value;
                            RaisePropertyChanged(() => DisplayValue);
                        }
                    }
                    else if (!_displayValue.Equals(value))
                    {
                        _displayValue = value;
                        RaisePropertyChanged(() => DisplayValue);
                    }

                    //DisplayParameterValueMaybeChanged?.Invoke();
                    IsValueFallback = false;
                    IsValueUnknown = false;
                }
                catch (Exception ex)
                {
                    DisplayValue = _fallbackValue;
                    IsValueFallback = true;
                    IsValueUnknown = false;
                    Console.WriteLine(ex);
                }
            }
        }


        public bool IsValueFallback
        {
            get => _isValueFallback;
            set
            {
                if (_isValueFallback != value)
                {
                    _isValueFallback = value;
                    RaisePropertyChanged(() => IsValueFallback);
                    RaisePropertyChanged(() => IsValueFallbackOrUnknown);
                    RaisePropertyChanged(() => IsValueNotFallbackAndNotUnknown);
                    RaisePropertyChanged(() => BackgroundColor);
                }
            }
        }

        public bool IsValueUnknown
        {
            get => _isValueUnknown;
            set
            {
                if (_isValueUnknown != value)
                {
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


        public void SetValueIsUnknown()
        {
            IsValueUnknown = true;
            IsValueFallback = false;
            DisplayValue = _unknownValue;
        }

        public TSet SettValue
        {
            get => _settValue;
            set
            {
                try
                {
                    if (_settValue == null)
                    {
                        if (value != null)
                        {
                            _settValue = value;
                            _sendValueValidator.Invoke(value); // if throws - we fallback
                            RaisePropertyChanged(() => SettValue);
                        }
                    }
                    else if (!_settValue.Equals(value))
                    {
                        _settValue = value;
                        _sendValueValidator.Invoke(value); // if throws - we fallback
                        RaisePropertyChanged(() => SettValue);
                    }

                    //DisplayParameterValueMaybeChanged?.Invoke();
                    IsSettValueFallback = false;
                    IsSettValueUnknown = false;
                }
                catch (Exception ex)
                {
                    _settValue = _fallbackSettValue;
                    RaisePropertyChanged(() => SettValue);
                    IsSettValueFallback = true;
                    IsSettValueUnknown = false;
                    Console.WriteLine(ex);
                }
            }
        }


        public bool IsSettValueFallback
        {
            get => _isSettValueFallback;
            set
            {
                if (_isSettValueFallback != value)
                {
                    _isSettValueFallback = value;
                    RaisePropertyChanged(() => IsSettValueFallback);
                    RaisePropertyChanged(() => IsSettValueFallbackOrUnknown);
                    RaisePropertyChanged(() => IsSettValueNotFallbackAndNotUnknown);
                }
            }
        }

        public bool IsSettValueUnknown
        {
            get => _isSettValueUnknown;
            set
            {
                if (_isSettValueUnknown != value)
                {
                    _isSettValueUnknown = value;
                    RaisePropertyChanged(() => IsSettValueUnknown);
                    RaisePropertyChanged(() => IsSettValueFallbackOrUnknown);
                    RaisePropertyChanged(() => IsSettValueNotFallbackAndNotUnknown);
                }
            }
        }

        public bool IsSettValueFallbackOrUnknown => _isSettValueFallback || _isSettValueUnknown;
        public bool IsSettValueNotFallbackAndNotUnknown => !IsSettValueFallbackOrUnknown;


        public ICommand SetCommand => _setCommand;


        public Colors BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                if (_backgroundColor != value)
                {
                    _backgroundColor = value;
                    RaisePropertyChanged(() => BackgroundColor);
                }
            }
        }
    }
}
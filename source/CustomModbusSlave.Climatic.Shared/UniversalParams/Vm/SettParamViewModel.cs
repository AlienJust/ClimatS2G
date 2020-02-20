using System;
using System.Windows.Input;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm
{
    public class SettParamViewModel<TSet> : ViewModelBase, ISettParameter<TSet>
    {
        private readonly IThreadNotifier _uiNotifier;

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

        public SettParamViewModel(
            string displayName,
            IThreadNotifier uiNotifier,
            Action<TSet> sendValueValidator,
            TSet fallbackSettValue,
            TSet unknownSettValue,
            Action<TSet, Action<Exception>> asyncParamSetter)
        {
            DisplayName = displayName;
            _uiNotifier = uiNotifier;
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
        }

        private void Set()
        {
            BackgroundColor = Colors.Yellow;
            // TODO: check thread safity:
            _asyncParamSetter.Invoke(SettValue, ex =>
            {
                if (ex != null) _uiNotifier.Notify(() => BackgroundColor = Colors.OrangeRed);
                else _uiNotifier.Notify(() => BackgroundColor = Colors.LimeGreen);
            });
        }

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
    }
}
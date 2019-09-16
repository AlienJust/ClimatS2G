using System;
using System.Collections.Generic;
using System.Windows.Input;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    internal sealed class ParameterSetterViewModelSimple : ViewModelBase, IParameterSetterViewModel
    {
        public IList<ParameterPreselectedValue> CustomValueList { get; }

        private ParameterPreselectedValue _selectedValue;
        public ParameterPreselectedValue SelectedValue
        {
            get => _selectedValue;
            set
            {
                if (value != _selectedValue)
                {
                    _selectedValue = value;
                    RaisePropertyChanged(() => SelectedValue);
                    Value = _selectedValue.Value;
                    Console.WriteLine("Selected value was changed to " + _selectedValue.Value);
                }
            }
        }

        private readonly RelayCommand _setValue;
        private readonly IThreadNotifier _uiNotifier;

        public ICommand SetValue { get => _setValue; }

        private double _value;
        public double Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    RaisePropertyChanged(() => Value);
                }
            }
        }

        private LastSetStateResult _lastSet;
        public LastSetStateResult LastSet
        {
            get => _lastSet; set
            {
                if (_lastSet != value)
                {
                    _lastSet = value;
                    RaisePropertyChanged(() => LastSet);
                }
            }
        }

        public ParameterSetterViewModelSimple(IParameterSetter parameterSetter, IThreadNotifier uiNotifier, IParameterInjectionConfiguration injectionConfiguration)
        {
            _uiNotifier = uiNotifier;
            CustomValueList = injectionConfiguration.PreselectedValueList;

            _lastSet = LastSetStateResult.Unknown;

            _setValue = new RelayCommand(() =>
            {
                Console.WriteLine($"Setting for param {injectionConfiguration.ZeroBasedParameterNumber} value " + injectionConfiguration.GetValue(_value));
                parameterSetter.SetParameterAsync(injectionConfiguration.ZeroBasedParameterNumber, injectionConfiguration.GetValue(_value), ex =>
                {
                    _uiNotifier.Notify(() =>
                    {
                        if (ex != null) LastSet = LastSetStateResult.Unsuccess;
                        else LastSet = LastSetStateResult.Success;
                    });
                });
            });
        }
    }
}
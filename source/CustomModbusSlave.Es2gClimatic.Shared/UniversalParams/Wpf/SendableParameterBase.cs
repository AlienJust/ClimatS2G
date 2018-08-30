using System;
using System.Windows.Input;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.Conversion.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbus.Slave.FastReply.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Wpf {
  public class SendableParameterBase<TRaw> : ViewableSettableParameterBase<TRaw> where TRaw : struct {
    public readonly int _paramIndex;
    private readonly IParameterSetter _parameterSetter; // param setter can be generic too
    private readonly IThreadNotifier _uiNotifier;
    private readonly IBuilderOneToOne<TRaw, BytesPair> _sendConverter;

    //private readonly RelayCommand _resetCommand;
    private readonly DependedCommand _setCommand;
    private bool _isEnabled;
    private Colors _lastOperationColor;

    public SendableParameterBase(int paramIndex, string name, TRaw? defaultValue, IBuilderOneToOne<TRaw, string> stringBuilder, string toolTipText,
      IParameterSetter parameterSetter, IThreadNotifier uiNotifier, IBuilderOneToOne<TRaw, BytesPair> sendConverter)
      : base(paramIndex.ToString(), name, defaultValue, stringBuilder, toolTipText) {
      _isEnabled = true;
      _paramIndex = paramIndex;
      _parameterSetter = parameterSetter;
      _uiNotifier = uiNotifier;
      _sendConverter = sendConverter;
      //_resetCommand = new RelayCommand(Reset, () => ReceivedValueFormatted.HasValue);
      _setCommand = new DependedCommand(Set, () => FormattedValue != null && IsEnabled);
      _setCommand.AddDependOnProp(this, () => FormattedValue);
      _setCommand.AddDependOnProp(this, () => IsEnabled);
      _lastOperationColor = Colors.Transparent;
    }


    private void Set() {
      try {
        var valueToSend = _sendConverter.Build(FormattedValue.RawValue);
        IsEnabled = false;
        LastOperationColor = Colors.RoyalBlue;
        _parameterSetter.SetParameterAsync(_paramIndex, valueToSend.HighFirstUnsignedValue, exception => {
          _uiNotifier.Notify(() => {
            try {
              if (exception != null) {
                throw new Exception("Произошла ошибка во время установки параметра", exception);
              }

              LastOperationColor = Colors.Green;
            }
            catch //(Exception ex) {
            {
              // TODO: log exception to console
              //Console.WriteLine(ex);
              LastOperationColor = Colors.OrangeRed;
            }
            finally {
              IsEnabled = true;
            }
          });
        });
      }
      catch {
        LastOperationColor = Colors.IndianRed;
        IsEnabled = true;
      }
    }

    public bool IsEnabled {
      get => _isEnabled;
      set {
        if (_isEnabled != value) {
          _isEnabled = value;
          RaisePropertyChanged(() => IsEnabled);
          _setCommand.RaiseCanExecuteChanged();
        }
      }
    }

    public Colors LastOperationColor {
      get => _lastOperationColor;
      set {
        if (_lastOperationColor != value) {
          _lastOperationColor = value;
          RaisePropertyChanged(() => LastOperationColor);
        }
      }
    }

    //public ICommand ResetCommand => _resetCommand;
    public ICommand SetCommand => _setCommand;
  }
}
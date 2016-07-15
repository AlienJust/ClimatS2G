using System;
using System.Windows.Input;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.DoubleBytesPairConverter;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;

namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	public class SettableParameterViewModel : ViewModelBase, IReceivableParameter, IDisplayableParameter<double?>, ISettableByUserParameter<double?>, ISettableBytesPairtParameter {
		private readonly IDoubleBytesPairConverter _doubleBytesPairConverter;
		private readonly IParameterSetter _parameterSetter;
		private readonly IThreadNotifier _uiNotifier;
		private double? _formattedValue;
		private readonly RelayCommand _resetCommand;
		private readonly RelayCommand _setCommand;
		private bool _isEnabled;
		private Colors _lastOperationColor;
		private BytesPair? _receivedBytesValue;


		public int ParamIndex { get; }
		public string Name { get; }
		public double MaxValue { get; }
		public double MinValue { get; }

		public string StringFormat { get; set; }

		public SettableParameterViewModel(int paramIndex, string name, double maxValue, double minValue, double? formattedValue, string stringFormat, IDoubleBytesPairConverter doubleBytesPairConverter, IParameterSetter parameterSetter, IThreadNotifier uiNotifier) {
			_doubleBytesPairConverter = doubleBytesPairConverter;
			_parameterSetter = parameterSetter;
			_uiNotifier = uiNotifier;
			ParamIndex = paramIndex;
			Name = name;
			MaxValue = maxValue;
			MinValue = minValue;
			FormattedValue = formattedValue;
			StringFormat = stringFormat;

			_isEnabled = true;
			_lastOperationColor = Colors.Transparent;

			_resetCommand = new RelayCommand(Reset, () => ReceivedValueFormatted.HasValue);
			_setCommand = new RelayCommand(Set, () => _formattedValue.HasValue && IsEnabled);
		}

		private void Set() {
			if (!BytesValue.HasValue) return;

			IsEnabled = false;
			LastOperationColor = Colors.RoyalBlue;
			_parameterSetter.SetParameterAsync(ParamIndex, BytesValue.Value.HighFirstUnsignedValue, exception => {
				_uiNotifier.Notify(() => {
					try {
						if (exception != null) {
							throw new Exception("Произошла ошибка во время установки параметра", exception);
						}
						// if all ok:
						LastOperationColor = Colors.Green;
					}
					catch (Exception ex) {
						// TODO: log exception to console
						LastOperationColor = Colors.OrangeRed;
					}
					finally {
						IsEnabled = true;
					}
				});
			});
		}

		public bool IsEnabled {
			get { return _isEnabled; }
			set {
				if (_isEnabled != value) {
					_isEnabled = value;
					RaisePropertyChanged(() => IsEnabled);
					_setCommand.RaiseCanExecuteChanged();
				}
			}
		}

		private void Reset() {
			FormattedValue = ReceivedValueFormatted;
		}

		public double? FormattedValue {
			get {
				return _formattedValue;

			}
			set {
				if (_formattedValue != value) {
					_formattedValue = value;
					RaisePropertyChanged(() => FormattedValue);
					RaisePropertyChanged(() => BytesValue);
					_setCommand.RaiseCanExecuteChanged();
				}

			}
		}

		public BytesPair? BytesValue {
			get {
				if (!_formattedValue.HasValue) return null;
				return _doubleBytesPairConverter.ConvertToBytesPairHighFirst(_formattedValue.Value);
			}
			set {
				// used property FormattedValue instead of field _formattedValue to raise property changed event if needed
				if (!value.HasValue) FormattedValue = null;
				else FormattedValue = _doubleBytesPairConverter.ConvertToSomething(value.Value);
			}
		}

		public double? ReceivedValueFormatted {
			get {
				if (!_receivedBytesValue.HasValue) return null;
				return _doubleBytesPairConverter.ConvertToSomething(_receivedBytesValue.Value);
			}
		}

		public BytesPair? ReceivedBytesValue {
			set {
				if (_receivedBytesValue != value) {
					_receivedBytesValue = value;

					RaisePropertyChanged(() => ReceivedValueFormatted);
					_resetCommand.RaiseCanExecuteChanged();
					//_setCommand.RaiseCanExecuteChanged();
				}
			}
		}

		public Colors LastOperationColor {
			get { return _lastOperationColor; }
			set {
				if (_lastOperationColor != value) {
					_lastOperationColor = value;
					RaisePropertyChanged(() => LastOperationColor);
				}
			}
		}

		public ICommand ResetCommand => _resetCommand;
		public ICommand SetCommand => _setCommand;
	}
}

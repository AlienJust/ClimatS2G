using System;
using System.Windows.Input;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using AlienJust.Support.Collections;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;

namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams.AnyParameter {
	class SettableAnyParameterViewModel : ViewModelBase {
		private readonly IDoubleBytesPairConverter _doubleBytesPairConverter;
		private readonly IParameterSetter _parameterSetter;
		private readonly IThreadNotifier _uiNotifier;
		private double? _doubleValue;
		private double? _receivedDoubleValue;
		private readonly RelayCommand _resetCommand;
		private readonly RelayCommand _setCommand;
		private bool _isEnabled;
		private Colors _lastOperationColor;


		public int ParamIndex { get; }
		public string Name { get; }
		public double MaxValue { get; }
		public double MinValue { get; }
		
		public string StringFormat { get; set; }

		public SettableAnyParameterViewModel(int paramIndex, string name, double maxValue, double minValue, double? doubleValue, string stringFormat, IDoubleBytesPairConverter doubleBytesPairConverter, IParameterSetter parameterSetter, IThreadNotifier uiNotifier) {
			_doubleBytesPairConverter = doubleBytesPairConverter;
			_parameterSetter = parameterSetter;
			_uiNotifier = uiNotifier;
			ParamIndex = paramIndex;
			Name = name;
			MaxValue = maxValue;
			MinValue = minValue;
			DoubleValue = doubleValue;
			StringFormat = stringFormat;

			_receivedDoubleValue = null;
			_isEnabled = true;
			_lastOperationColor = Colors.Transparent;

			_resetCommand = new RelayCommand(Reset, ()=>_receivedDoubleValue.HasValue);
			_setCommand = new RelayCommand(Set, () => _doubleValue.HasValue && IsEnabled);
		}

		private void Set() {
			if (!UshortValue.HasValue) return;

			IsEnabled = false;
			LastOperationColor = Colors.RoyalBlue;
			_parameterSetter.SetParameterAsync(ParamIndex, UshortValue.Value.HighFirstUnsignedValue, exception => {
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

		public bool IsEnabled
		{
			get { return _isEnabled; }
			set
			{
				if (_isEnabled != value) {
					_isEnabled = value;
					RaisePropertyChanged(()=>IsEnabled);
					_setCommand.RaiseCanExecuteChanged();
				}
			}
		}

		private void Reset() {
			DoubleValue = _receivedDoubleValue;
		}

		public double? DoubleValue
		{
			get
			{
				return _doubleValue; 
				
			}
			set
			{
				if (_doubleValue != value) {
					_doubleValue = value;
					RaisePropertyChanged(()=>DoubleValue);
					RaisePropertyChanged(() => UshortValue);
					_setCommand.RaiseCanExecuteChanged();
				}
				
			}
		}

		public BytesPair? UshortValue
		{
			get
			{
				if (!_doubleValue.HasValue) return null;
				return _doubleBytesPairConverter.ConvertToBytesPairHighFirst(_doubleValue.Value);
			}
			set
			{
				// used property FormattedValue instead of field _doubleValue to raise property changed event if needed
				if (!value.HasValue) DoubleValue = null;
				else DoubleValue = _doubleBytesPairConverter.ConvertToDoubleHighFirst(value.Value);
			}
		}

		public double? ReceivedDoubleValue
		{
			get { return _receivedDoubleValue; }
			set
			{
				if (_receivedDoubleValue != value) {
					_receivedDoubleValue = value;
					RaisePropertyChanged(()=> ReceivedDoubleValue);
					RaisePropertyChanged(() => ReceivedUshortValue);
					_resetCommand.RaiseCanExecuteChanged();
				}
			}
		}

		public BytesPair? ReceivedUshortValue {
			get {
				if (!_receivedDoubleValue.HasValue) return null;
				return _doubleBytesPairConverter.ConvertToBytesPairHighFirst(_receivedDoubleValue.Value);
			}
			set {
				// used property FormattedValue instead of field _doubleValue to raise property changed event if needed
				if (!value.HasValue) ReceivedDoubleValue = null;
				else ReceivedDoubleValue = _doubleBytesPairConverter.ConvertToDoubleHighFirst(value.Value);
			}
		}

		public Colors LastOperationColor {
			get { return _lastOperationColor; }
			set
			{
				if (_lastOperationColor != value) {
					_lastOperationColor = value;
					RaisePropertyChanged(()=> LastOperationColor);
				}
			}
		}

		public ICommand ResetCommand => _resetCommand;
		public ICommand SetCommand => _setCommand;
	}
}

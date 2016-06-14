using System;
using System.Windows.Input;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;

namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	class SettableParameterViewModel : ViewModelBase {
		private readonly IDoubleUshortConverter _doubleUshortConverter;
		private readonly IParameterSetter _parameterSetter;
		private double? _doubleValue;
		private double? _receivedValue;
		private readonly RelayCommand _resetCommand;
		private readonly RelayCommand _setCommand;
		private bool _isEnabled;


		public int ParamIndex { get; }
		public string Name { get; }
		public double MaxValue { get; }
		public double MinValue { get; }
		
		public string StringFormat { get; set; }

		public SettableParameterViewModel(int paramIndex, string name, double maxValue, double minValue, double? doubleValue, string stringFormat, IDoubleUshortConverter doubleUshortConverter, IParameterSetter parameterSetter) {
			_doubleUshortConverter = doubleUshortConverter;
			_parameterSetter = parameterSetter;
			ParamIndex = paramIndex;
			Name = name;
			MaxValue = maxValue;
			MinValue = minValue;
			DoubleValue = doubleValue;
			StringFormat = stringFormat;

			_receivedValue = null;
			_resetCommand = new RelayCommand(Reset, ()=>_receivedValue.HasValue);
			_setCommand = new RelayCommand(Set, () => _doubleValue.HasValue);
		}

		private void Set() {
			IsEnabled = false;
			_parameterSetter.SetParameterAsync(ParamIndex, UshortValue.Value, exception => {
				try {
					if (exception != null) {
						throw new Exception("Произошла ошибка во время установки параметра", exception);
					}

					// TODO: mark last operation has succeed
				}
				catch (Exception ex) {
					// TODO: mark last operation has failed
					// TODO: log exception to console
				}
				finally {
					// TODO: unlock interface
					IsEnabled = true;
				}
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
				}
			}
		}

		private void Reset() {
			DoubleValue = _receivedValue;
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

		public ushort? UshortValue
		{
			get
			{
				if (!_doubleValue.HasValue) return null;
				return _doubleUshortConverter.ConvertToUshort(_doubleValue.Value);
			}
			set
			{
				// used property DoubleValue instead of field _doubleValue to raise property changed event if needed
				if (!value.HasValue) DoubleValue = null;
				else DoubleValue = _doubleUshortConverter.ConvertToDouble(value.Value);
			}
		}

		public double? ReceivedValue
		{
			get { return _receivedValue; }
			set
			{
				if (_receivedValue != value) {
					_receivedValue = value;
					RaisePropertyChanged(()=> ReceivedValue);

					_resetCommand.RaiseCanExecuteChanged();
				}
			}
		}

		public ICommand ResetCommand => _resetCommand;
		public ICommand SetCommand => _resetCommand;
	}
}

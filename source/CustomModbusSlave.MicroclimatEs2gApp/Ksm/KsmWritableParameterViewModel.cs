using System;
using System.Windows.Input;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class KsmWritableParameterViewModel : ViewModelBase, IKsmWritableParameterViewModel, ICurrentValueSettable {
		private readonly int _zeroBasedParameterIndex;
		private readonly IParameterSetter _parameterSetter;
		private readonly IDoubleUshortConverter _converter;
		private readonly ICommand _setCommand;
		private readonly ICommand _resetCommand;
		private double _valueToSet;
		private double _currentValue;
		private bool _isWaitingForSetComplete;


		public string Name { get; }

		public KsmWritableParameterViewModel(int zeroBasedParameterIndex, string name, IParameterSetter parameterSetter, IDoubleUshortConverter converter) {
			_zeroBasedParameterIndex = zeroBasedParameterIndex;
			_parameterSetter = parameterSetter;
			_converter = converter;
			Name = name;

			_setCommand = new DependedCommand(SetValue, () => !IsWaitingForSetComplete);
			_resetCommand = new RelayCommand(() => {

			});
		}

		public bool IsWaitingForSetComplete {
			get { return _isWaitingForSetComplete; }
			set {
				if (IsWaitingForSetComplete != value) {
					_isWaitingForSetComplete = value;
					RaisePropertyChanged(() => IsWaitingForSetComplete);
				}
			}
		}

		private void SetValue() {
			IsWaitingForSetComplete = true;
			try {
				var valueToSet = _converter.ConvertToUshort(_valueToSet);
				_parameterSetter.SetParameterAsync(_zeroBasedParameterIndex, valueToSet, exception => {
					try {
						if (exception != null) {
							throw exception;
							//
						}
					}
					catch (Exception ex) {
						// TODO: notify user about setting failed OR:
						// TODO: notify user something goes bad
					}
					finally {
						IsWaitingForSetComplete = false;
					}
				});
			}
			catch (Exception ex) {
				// TODO: notify user about something went really bad (cannot send set command!)
				IsWaitingForSetComplete = false;
			}
		}

		public double ValueToSet {
			get { return _valueToSet; }
			set {
				if (_valueToSet != value) {
					_valueToSet = value;
					RaisePropertyChanged(() => ValueToSet);
				}
			}
		}

		public double CurrentValue {
			get {
				return _currentValue;

			}

			private set {
				if (_currentValue != value) {
					_currentValue = value;
					RaisePropertyChanged(() => CurrentValue);
				}
			}
		}

		public void SetCurrentValue(double currentValue) {
			CurrentValue = currentValue;
		}
	}
}
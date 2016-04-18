using System;
using System.Collections.Generic;
using System.Windows.Input;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class KsmDataViewModel : ViewModelBase, IParameterSetter {
		private readonly IThreadNotifier _notifier;

		private readonly List<KsmWritableParameterViewModel> _parameterVmList;
		public KsmDataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;
			_parameterVmList = new List<KsmWritableParameterViewModel>();
			for (int i = 0; i < 50; ++i) {
				_parameterVmList.Add(new KsmWritableParameterViewModel(i, "Параметр " + (i + 1), this, new DoubleUshortConverterSimple()));
			}
		}

		public List<KsmWritableParameterViewModel> ParameterVmList => _parameterVmList;

		public void SetParameterAsync(int zeroBasedParameterNumber, ushort value, Action<Exception> callback) {
			// тут должна быть очередь потокобезопасная
		}
	}

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

	internal interface IParameterSetter {
		void SetParameterAsync(int zeroBasedParameterNumber, ushort value, Action<Exception> callback);
	}

	internal interface ICurrentValueSettable {
		void SetCurrentValue(double currentValue);
	}

	internal interface IKsmWritableParameterViewModel {
		string Name { get; }
		double ValueToSet { get; set; }
		double CurrentValue { get; }
	}
}

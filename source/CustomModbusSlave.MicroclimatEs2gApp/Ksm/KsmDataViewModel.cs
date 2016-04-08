using System.Collections.Generic;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class KsmDataViewModel : ViewModelBase {
		private readonly List<KsmWritableParameterViewModel> _parameterVmList;
		public KsmDataViewModel() {
			_parameterVmList = new List<KsmWritableParameterViewModel>();
			for (int i = 0; i < 50; ++i) {
				_parameterVmList.Add(new KsmWritableParameterViewModel(i, "Параметр "  + (i + 1)));
			}
		}

		public List<KsmWritableParameterViewModel> ParameterVmList => _parameterVmList;
	}

	class KsmWritableParameterViewModel : ViewModelBase, IKsmWritableParameterViewModel, ICurrentValueSettable {
		private readonly int _zeroBasedParameterIndex;
		private double _valueToSet;
		private double _currentValue;

		public string Name { get; }

		public KsmWritableParameterViewModel(int zeroBasedParameterIndex, string name) {
			_zeroBasedParameterIndex = zeroBasedParameterIndex;
			Name = name;
		}

		public double ValueToSet
		{
			get { return _valueToSet; }
			set
			{
				if (_valueToSet != value) {
					_valueToSet = value;
					RaisePropertyChanged(()=>ValueToSet);
				}
			}
		}

		public double CurrentValue {
			get
			{
				return _currentValue;
				
			}

			private set
			{
				if (_currentValue != value) {
					_currentValue = value;
					RaisePropertyChanged(()=>CurrentValue);
				}
			}
		}

		public void SetCurrentValue(double currentValue) {
			CurrentValue = currentValue;
		}
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

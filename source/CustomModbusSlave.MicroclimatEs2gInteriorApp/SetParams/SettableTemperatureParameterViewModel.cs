using AlienJust.Support.Concurrent.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;

namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	class SettableTemperatureParameterViewModel : SettableParameterViewModel {
		public SettableTemperatureParameterViewModel(int paramIndex, string name, double maxValue, double minValue, double? doubleValue, string stringFormat, IDoubleBytesPairConverter doubleBytesPairConverter, IParameterSetter parameterSetter, IThreadNotifier uiNotifier) : base(paramIndex, name, maxValue, minValue, doubleValue, stringFormat, doubleBytesPairConverter, parameterSetter, uiNotifier) { }
	}
}
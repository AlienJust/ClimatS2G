using AlienJust.Support.Concurrent.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.DoubleBytesPairConverter;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;

namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	public class SettableTemperatureParameterViewModel : SettableParameterViewModel {
		public SettableTemperatureParameterViewModel(int paramIndex, string name, double maxValue, double minValue, double? formattedValue, string stringFormat, IDoubleBytesPairConverter doubleBytesPairConverter, IParameterSetter parameterSetter, IThreadNotifier uiNotifier) : base(paramIndex, name, maxValue, minValue, formattedValue, stringFormat, doubleBytesPairConverter, parameterSetter, uiNotifier) { }
	}
}
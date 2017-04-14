using AlienJust.Support.Concurrent.Contracts;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.DoubleBytesPairConverter;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.ViewModel {
	public class SettableTemperatureParameterViewModel : SettableParameterViewModel {
		public SettableTemperatureParameterViewModel(int paramIndex, string name, double maxValue, double minValue, double? formattedValue, string stringFormat, IDoubleBytesPairConverter doubleBytesPairConverter, IParameterSetter parameterSetter, IThreadNotifier uiNotifier, string toolTipText) : base(paramIndex, name, maxValue, minValue, formattedValue, stringFormat, doubleBytesPairConverter, parameterSetter, uiNotifier, toolTipText) { }
	}
}
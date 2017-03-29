using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.DoubleBytesPairConverter;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.ViewModel;
using CustomModbus.Slave.FastReply.Contracts;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.SetParameters {
	class MukWarmFloorSetParamsViewModel : ViewModelBase {
		public SettableParameterViewModel SettableParam519 { get; }
		public SettableParameterViewModel SettableParam520 { get; }
		public SettableParameterViewModel SettableParam521 { get; }
		public SettableParameterViewModel SettableParam522 { get; }

		public MukWarmFloorSetParamsViewModel(IThreadNotifier uiNotifier, IParameterSetter parameterSetter) {
			SettableParam519 = new SettableParameterViewModel(519, "5.19 Резерв", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam520 = new SettableParameterViewModel(520, "5.20 Резерв", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam521 = new SettableParameterViewModel(521, "5.21 Резерв", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam522 = new SettableParameterViewModel(522, "5.22 Резерв", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
		}
	}
}

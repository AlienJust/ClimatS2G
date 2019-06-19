using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.DoubleBytesPairConverter;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.ViewModel;
using CustomModbus.Slave.FastReply.Contracts;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.SetParameters {
	class MukFlapSetParamsViewModel : ViewModelBase {
		public SettableParameterViewModel SettableParam223 { get; }
		public SettableParameterViewModel SettableParam224 { get; }
		public SettableParameterViewModel SettableParam225 { get; }
		public SettableParameterViewModel SettableParam226 { get; }
		public SettableParameterViewModel SettableParam227 { get; }
		public SettableParameterViewModel SettableParam228 { get; }
		public SettableParameterViewModel SettableParam229 { get; }
		public SettableParameterViewModel SettableParam230 { get; }
		public SettableParameterViewModel SettableParam231 { get; }

		public MukFlapSetParamsViewModel(IThreadNotifier uiNotifier, IParameterSetter parameterSetter) {
			SettableParam223 = new SettableParameterViewModel(223, "Ручной/автоматический режим. 1 = ручной режим", 1.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier, null);
			SettableParam224 = new SettableParameterViewModel(224, "Уставка ШИМ на заслонку в ручном режиме 0 - 255", 255.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier, null);

			SettableParam225 = new SettableParameterViewModel(225, "H – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier, null);
			SettableParam226 = new SettableParameterViewModel(226, "I – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier, null);
			SettableParam227 = new SettableParameterViewModel(227, "J – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier, null);
			SettableParam228 = new SettableParameterViewModel(228, "A – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier, null);
			SettableParam229 = new SettableParameterViewModel(229, "E – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier, null);
			SettableParam230 = new SettableParameterViewModel(230, "C – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier, null);
			SettableParam231 = new SettableParameterViewModel(231, "D – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier, null);
		}
	}
}

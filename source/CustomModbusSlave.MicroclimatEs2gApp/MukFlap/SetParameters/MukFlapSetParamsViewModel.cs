using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.DoubleBytesPairConverter;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.SetParameters {
	class MukFlapSetParamsViewModel : ViewModelBase {
		public SettableParameterViewModel SettableParam227 { get; }
		public SettableParameterViewModel SettableParam228 { get; }
		public SettableParameterViewModel SettableParam229 { get; }
		public SettableParameterViewModel SettableParam230 { get; }
		public SettableParameterViewModel SettableParam231 { get; }
		public SettableParameterViewModel SettableParam232 { get; }
		public SettableParameterViewModel SettableParam233 { get; }
		public SettableParameterViewModel SettableParam234 { get; }
		public SettableParameterViewModel SettableParam235 { get; }

		public MukFlapSetParamsViewModel(IThreadNotifier uiNotifier, IParameterSetter parameterSetter) {
			SettableParam227 = new SettableParameterViewModel(223, "Ручной/автоматический режим. 1 = ручной режим", 1.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam228 = new SettableParameterViewModel(224, "Уставка ШИМ на заслонку в ручном режиме 0 - 255", 255.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);

			SettableParam229 = new SettableParameterViewModel(225, "H – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam230 = new SettableParameterViewModel(226, "I – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam231 = new SettableParameterViewModel(227, "J – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam232 = new SettableParameterViewModel(228, "A – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam233 = new SettableParameterViewModel(229, "E – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam234 = new SettableParameterViewModel(230, "C – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam235 = new SettableParameterViewModel(231, "D – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
		}
	}
}

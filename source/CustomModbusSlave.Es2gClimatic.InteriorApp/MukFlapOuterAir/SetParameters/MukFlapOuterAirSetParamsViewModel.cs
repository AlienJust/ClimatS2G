using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.DoubleBytesPairConverter;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.SetParameters {
	class MukFlapOuterAirSetParamsViewModel : ViewModelBase {
		public SettableParameterViewModel SettableParam227 { get; }
		public SettableParameterViewModel SettableParam228 { get; }
		public SettableParameterViewModel SettableParam229 { get; }
		public SettableParameterViewModel SettableParam230 { get; }
		public SettableParameterViewModel SettableParam231 { get; }
		public SettableParameterViewModel SettableParam232 { get; }
		public SettableParameterViewModel SettableParam233 { get; }
		public SettableParameterViewModel SettableParam234 { get; }
		public SettableParameterViewModel SettableParam235 { get; }

		public MukFlapOuterAirSetParamsViewModel(IThreadNotifier uiNotifier, IParameterSetter parameterSetter) {
			SettableParam227 = new SettableParameterViewModel(227, "Ручной/автоматический режим. 1 = ручной режим", 1.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam228 = new SettableParameterViewModel(228, "Уставка ШИМ на заслонку в ручном режиме 0 - 255", 255.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);

			SettableParam229 = new SettableParameterViewModel(229, "H – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam230 = new SettableParameterViewModel(230, "I – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam231 = new SettableParameterViewModel(231, "J – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam232 = new SettableParameterViewModel(232, "A – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam233 = new SettableParameterViewModel(233, "E – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam234 = new SettableParameterViewModel(234, "C – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam235 = new SettableParameterViewModel(235, "D – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
		}
	}
}

using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.DoubleBytesPairConverter;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukAirExhauster.SetParameters {
	class MukAirExhausterSetParamsViewModel : ViewModelBase {
		public SettableParameterViewModel SettableParam619 { get; }
		public SettableParameterViewModel SettableParam620 { get; }

		public MukAirExhausterSetParamsViewModel(IThreadNotifier uiNotifier, IParameterSetter parameterSetter) {
			SettableParam619 = new SettableParameterViewModel(619, "Ручной/автоматический режим. 1 = ручной режим", 1.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam620 = new SettableParameterViewModel(620, "Уставка ШИМ вентилятора в ручном режиме 0 - 255", 255.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
		}
	}
}

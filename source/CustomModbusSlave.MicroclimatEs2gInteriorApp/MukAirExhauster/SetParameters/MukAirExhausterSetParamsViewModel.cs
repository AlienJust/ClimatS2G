using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukAirExhauster.SetParameters {
	class MukAirExhausterSetParamsViewModel : ViewModelBase {
		public SettableParameterViewModel SettableParam519 { get; }
		public SettableParameterViewModel SettableParam520 { get; }

		public MukAirExhausterSetParamsViewModel(IThreadNotifier uiNotifier, IParameterSetter parameterSetter) {
			SettableParam519 = new SettableParameterViewModel(519, "Ручной/автоматический режим. 1 = ручной режим", 1.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam520 = new SettableParameterViewModel(520, "Уставка ШИМ вентилятора в ручном режиме 0 - 255", 255.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
		}
	}
}

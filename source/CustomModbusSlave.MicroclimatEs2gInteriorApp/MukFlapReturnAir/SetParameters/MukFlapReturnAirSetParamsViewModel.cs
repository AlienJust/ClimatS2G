using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapReturnAir.SetParameters {
	class MukFlapReturnAirSetParamsViewModel : ViewModelBase {
		public SettableParameterViewModel SettableParam725 { get; }
		public SettableParameterViewModel SettableParam726 { get; }

		public MukFlapReturnAirSetParamsViewModel(IThreadNotifier uiNotifier, IParameterSetter parameterSetter) {
			SettableParam725 = new SettableParameterViewModel(725, "Ручной/автоматический режим. 1 = ручной режим", 1.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
			SettableParam726 = new SettableParameterViewModel(726, "Уставка ШИМ на заслонку в ручном режиме 0 - 255", 255.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier);
		}
	}
}

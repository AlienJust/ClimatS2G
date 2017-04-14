using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.DoubleBytesPairConverter;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.ViewModel;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapReturnAir.SetParameters {
	class MukFlapReturnAirSetParamsViewModel : ViewModelBase {
		public SettableParameterViewModel SettableParam725 { get; }
		public SettableParameterViewModel SettableParam726 { get; }

		public MukFlapReturnAirSetParamsViewModel(IThreadNotifier uiNotifier, IParameterSetter parameterSetter) {
			SettableParam725 = new SettableParameterViewModel(725, "Ручной/автоматический режим. 1 = ручной режим", 1.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier, null);
			SettableParam726 = new SettableParameterViewModel(726, "Уставка ШИМ на заслонку в ручном режиме 0 - 255", 255.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier, null);
		}
	}
}

using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.DoubleBytesPairConverter;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.ViewModel;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.SetParameters
{
    class MukAirExhausterSetParamsViewModel : ViewModelBase
    {
        public SettableParameterViewModel SettableParam619 { get; }
        public SettableParameterViewModel SettableParam620 { get; }

        public MukAirExhausterSetParamsViewModel(IThreadNotifier uiNotifier, IParameterSetter parameterSetter)
        {
            SettableParam619 = new SettableParameterViewModel(619, "Ручной/автоматический режим. 1 = ручной режим", 1.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier, null);
            SettableParam620 = new SettableParameterViewModel(620, "Уставка ШИМ вентилятора в ручном режиме 0 - 255", 255.0, 0.0, null, "f0", new DoubleBytesPairConverterSimpleUshort(), parameterSetter, uiNotifier, null);
        }
    }
}

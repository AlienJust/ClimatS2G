using CustomModbusSlave.MicroclimatEs2gApp.SetParams;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.ViewModel {
	public interface IKsmBitParameterViewModel : IReceivableParameter, IDisplayableParameter<bool?> {
	}
}
using CustomModbusSlave.MicroclimatEs2gApp.SetParams;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.ViewModel {
	public interface IKsmBitParameterViewModel : IReceivableParameter, IDisplayableParameter<bool?> {
	}
}
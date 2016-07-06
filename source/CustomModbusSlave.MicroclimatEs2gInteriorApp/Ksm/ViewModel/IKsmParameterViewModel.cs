using CustomModbusSlave.MicroclimatEs2gApp.SetParams;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm.ViewModel {
	internal interface IKsmParameterViewModel : IReceivableParameter {
		string ReceivedValueFormatted { get; }
	}
}
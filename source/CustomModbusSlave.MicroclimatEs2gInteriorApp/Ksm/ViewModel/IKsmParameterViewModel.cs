namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm.ViewModel {
	internal interface IKsmParameterViewModel : ICurrentSettableKsmParameter {
		string Name { get; }
		string ReceivedValueFormatted { get; }
	}
}
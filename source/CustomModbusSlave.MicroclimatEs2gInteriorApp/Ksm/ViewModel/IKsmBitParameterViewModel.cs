namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm.ViewModel {
	internal interface IKsmBitParameterViewModel : ICurrentSettableKsmParameter {
		string Name { get; }
		bool? ReceivedBitValue { get; }
	}
}
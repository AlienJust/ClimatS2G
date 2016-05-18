namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm.ViewModel {
	internal interface IKsmBitParameterViewModel : ISettableKsmParameter {
		string Name { get; }
		bool? ReceivedBitValue { get; }
	}
}
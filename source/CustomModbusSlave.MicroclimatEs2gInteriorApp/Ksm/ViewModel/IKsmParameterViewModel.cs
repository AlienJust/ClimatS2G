namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	internal interface IKsmParameterViewModel : ISettableKsmParameter {
		string Name { get; }
		bool ShowBits { get; }
		string ReceivedValueFormatted { get; }
	}
}
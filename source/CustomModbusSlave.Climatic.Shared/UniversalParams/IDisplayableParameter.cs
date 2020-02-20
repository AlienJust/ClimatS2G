namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	interface IDisplayableParameter<out T> : IParameter {
		T ReceivedValueFormatted { get; }
	}
}

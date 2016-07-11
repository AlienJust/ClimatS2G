namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	public interface IDisplayableParameter<out T> : IParameter {
		T ReceivedValueFormatted { get; }
	}
}
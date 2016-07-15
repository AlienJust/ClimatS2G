namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	public interface ISettableByUserParameter<T> : IParameter {
		T FormattedValue { get; set; }
	}
}
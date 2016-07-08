namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	interface ISettableByUserParameter<T> : IParameter {
		T FormattedValue { get; set; }
	}
}
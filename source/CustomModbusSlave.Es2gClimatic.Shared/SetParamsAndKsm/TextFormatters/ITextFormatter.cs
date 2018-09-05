namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters {
	public interface ITextFormatter<in T> {
		string Format(T value);
	}
}

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm.TextFormatters {
	internal interface ITextFormatter<in T> {
		string Format(T value);
	}
}
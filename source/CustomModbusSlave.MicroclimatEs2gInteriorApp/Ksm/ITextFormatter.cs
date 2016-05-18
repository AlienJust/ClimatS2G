namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	internal interface ITextFormatter<in T> {
		string Format(T value);
	}
}
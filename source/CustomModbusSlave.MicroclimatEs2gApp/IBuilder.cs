namespace CustomModbusSlave.MicroclimatEs2gApp {
	internal interface IBuilder<out T> {
		T Build();
	}
}
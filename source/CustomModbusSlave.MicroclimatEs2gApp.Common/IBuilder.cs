namespace CustomModbusSlave.MicroclimatEs2gApp {
	public interface IBuilder<out T> {
		T Build();
	}
}
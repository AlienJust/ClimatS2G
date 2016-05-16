namespace CustomModbusSlave.MicroclimatEs2gApp.Common {
	public interface IUpdateable<in T> {
		void Update(T updatedValue);
	}
}
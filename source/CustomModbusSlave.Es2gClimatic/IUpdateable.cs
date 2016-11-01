namespace CustomModbusSlave.Es2gClimatic {
	public interface IUpdateable<in T> {
		void Update(T updatedValue);
	}
}
namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	internal interface IUpdateable<in T> {
		void Update(T updatedValue);
	}
}
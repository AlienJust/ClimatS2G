namespace CustomModbusSlave.Es2gClimatic.Shared {
	public interface ICmdListener<out T> : ICommandListener {
		event DataReceivedDelegate<T> DataReceived;
	}
}
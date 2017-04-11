namespace CustomModbusSlave.Es2gClimatic.Shared {
	public interface ICmdListener<T> : ICommandListener {
		event DataReceivedDelegate<T> DataReceived;
	}
}
namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
	public interface IStdNotifier {
		void AddListener(ICmdListenerStd listener);

		void AddSerialChannel(SerialChannel channel);
		void RemoveSerialChannel(SerialChannel channel);
	}
}

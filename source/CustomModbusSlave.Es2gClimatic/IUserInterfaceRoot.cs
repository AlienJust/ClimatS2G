using AlienJust.Support.Concurrent.Contracts;

namespace CustomModbusSlave.Es2gClimatic {
	public interface IUserInterfaceRoot
	{
		IThreadNotifier Notifier { get; }
	}
}
using AlienJust.Support.Concurrent.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common {
	public interface IUserInterfaceRoot
	{
		IThreadNotifier Notifier { get; }
	}
}
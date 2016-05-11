using AlienJust.Support.Concurrent.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp {
	internal interface IUserInterfaceRoot
	{
		IThreadNotifier Notifier { get; }
	}
}
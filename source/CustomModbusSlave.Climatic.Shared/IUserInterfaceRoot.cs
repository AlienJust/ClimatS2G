using AlienJust.Support.Concurrent.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared
{
    public interface IUserInterfaceRoot
    {
        IThreadNotifier Notifier { get; }
    }
}
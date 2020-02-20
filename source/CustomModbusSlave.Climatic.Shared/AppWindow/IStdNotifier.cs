using CustomModbusSlave.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public interface IStdNotifier
    {
        void AddListener(ICmdListenerStd listener);

        void AddSerialChannel(ISerialChannel channel);
        void RemoveSerialChannel(ISerialChannel channel);
    }
}
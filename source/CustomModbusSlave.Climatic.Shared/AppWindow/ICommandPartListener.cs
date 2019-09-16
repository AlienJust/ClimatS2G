using System;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public interface ICommandPartListener
    {
        event EventHandler<CommandPartReceivedEventArgs> CommandPartReceived;
    }
}
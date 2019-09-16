using System;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public sealed class CommandPartReceivedEventArgs : EventArgs
    {
        public ICmdPartConfigAndBytes Data { get; private set; }

        public CommandPartReceivedEventArgs(ICmdPartConfigAndBytes data) : base()
        {
            Data = data;
        }
    }
}
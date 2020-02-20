using System;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public interface IParamListener
    {
        event EventHandler<ParameterValueReceivedEventArgs> ValueReceived;
    }
}
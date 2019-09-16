using System;

namespace CustomModbus.Slave.FastReply.Contracts
{
    public interface IParameterSetter
    {
        void SetParameterAsync(int zeroBasedParameterNumber, ushort value, Action<Exception> callback);
    }
}

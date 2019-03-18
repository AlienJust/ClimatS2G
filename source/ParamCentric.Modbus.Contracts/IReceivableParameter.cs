using AlienJust.Support.Collections;
using ParamCentric.Common.Contracts;

namespace ParamCentric.Modbus.Contracts
{
    public interface IReceivableParameter : IParameter
    {
        BytesPair? ReceivedBytesValue { set; }
    }
}
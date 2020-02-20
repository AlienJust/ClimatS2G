using AlienJust.Support.Collections;
using ParamCentric.Common.Contracts;

namespace ParamCentric.Modbus.Contracts
{
    public interface ISettableBytesPairParameter : IParameter
    {
        BytesPair? BytesValue { get; }
    }
}
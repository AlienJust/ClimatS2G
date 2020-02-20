using System.Collections.Generic;
using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.FrequencyConverter.F0Request
{
    public class RequestF0Builder : IBuilder<IRequestF0>
    {
        private readonly IList<byte> _bytes;

        public RequestF0Builder(IList<byte> bytes)
        {
            _bytes = bytes;
        }

        public IRequestF0 Build()
        {
            return new RequestF0Simple(_bytes[2], _bytes[3].GetBit(7));
        }
    }
}
using System.Collections.Generic;
using AlienJust.Support.Collections;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm
{
    public class CmdListenerKsmParams : CmdListenerBase<IList<BytesPair>>
    {
        public CmdListenerKsmParams(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) { }

        public override IList<BytesPair> BuildData(IList<byte> bytes)
        {
            var result = new BytesPair[(bytes.Count - 9) / 2];
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = new BytesPair(bytes[7 + i * 2], bytes[8 + i * 2]);
            }

            return result;
        }
    }
}
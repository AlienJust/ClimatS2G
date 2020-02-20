using DataAbstractionLevel.Low.PsnConfig.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CustomModbusSlave.Es2gClimatic.Shared
{
    public sealed class CmdListenerBytes : CmdListenerBase<IList<byte>>
    {
        public CmdListenerBytes(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) { }

        public override IList<byte> BuildData(byte[] bytes)
        {
            return bytes.Skip(2).Take(bytes.Length - 4).ToList();
        }
    }


    public interface ICmdPartConfigAndBytes
    {
        IPsnProtocolCommandPartConfiguration CmdPartConfig { get; }
        byte[] DataBytes { get; }

    }

    internal sealed class CmdPartConfigAndBytesSimple : ICmdPartConfigAndBytes
    {
        public IPsnProtocolCommandPartConfiguration CmdPartConfig { get; }
        public byte[] DataBytes { get; }
        public CmdPartConfigAndBytesSimple(IPsnProtocolCommandPartConfiguration config, byte[] bytes)
        {
            CmdPartConfig = config;
            DataBytes = bytes;
        }
    }

    public sealed class CmdListenerCmdPartAndBytes : CmdListenerBase<ICmdPartConfigAndBytes>
    {
        private IPsnProtocolCommandPartConfiguration _cmdPartConfig;
        public CmdListenerCmdPartAndBytes(IPsnProtocolCommandPartConfiguration config) :
            base((byte)config.Address.DefinedValue, (byte)config.CommandCode.DefinedValue, config.Length)
        {
            _cmdPartConfig = config;
            // TODO: can be done BETTER checking by using checkingAll defined params in command part
        }

        public override ICmdPartConfigAndBytes BuildData(byte[] bytes)
        {
            return new CmdPartConfigAndBytesSimple(_cmdPartConfig, bytes);
        }
    }
}
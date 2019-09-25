using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared
{
    public abstract class CmdListenerBase<T> : IStdCheckableCmdListener, ICmdListener<T>
    {
        public byte AddrToCheck { get; }
        public byte CodeToCheck { get; }
        public int Length { get; }

        protected CmdListenerBase(byte addrToCheck, byte codeToCheck, int length)
        {
            AddrToCheck = addrToCheck;
            CodeToCheck = codeToCheck;
            Length = length;
        }

        public event DataReceivedDelegate<T> DataReceived;

        public void ReceiveCommand(byte addr, byte code, byte[] data)
        {
            if (addr == AddrToCheck && code == CodeToCheck && Length == data.Length)
            {
                if (addr == 0x14 && code == 0x10)
                {
                    var x = true;
                }

                OnDataReceived(data, BuildData(data));
            }
        }

        public abstract T BuildData(byte[] bytes);

        protected virtual void OnDataReceived(byte[] bytes, T data)
        {
            DataReceived?.Invoke(bytes, data);
        }

        public override string ToString()
        {
            return $"Addr= {AddrToCheck}, Cmd={CodeToCheck}, Len={Length}";
        }
    }
}
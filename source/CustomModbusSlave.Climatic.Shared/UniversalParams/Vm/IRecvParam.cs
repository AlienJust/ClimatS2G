namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm
{
    /// <summary>
    /// Represent some portion of received data
    /// </summary>
    /// <typeparam name="T">Type of received data</typeparam>
    public interface IRecvParam<out T>
    {
        string ReceiveName { get; }

        //IList<byte> ReceivedRawValue { get; } // UserSide
        T ReceivedRawValue { get; }
        event NotifyDataReceivedDelegate NotifyDataReceived; // UserSide
    }

    public interface ISendParam<out T>
    {
        string SendName { get; }
        T SentRawValue { get; }
    }
}
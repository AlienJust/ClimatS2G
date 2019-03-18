namespace ParamCentric.Modbus.Contracts
{
    public interface IReceivableModbusRtuParameter : IReceivableParameter
    {
        byte CommandCode { get; }
        byte Address { get; }
        int ZeroBasedParameterNumber { get; }
    }
}
namespace ParamCentric.Modbus.Contracts
{
    public interface IReceiverModbusRtu
    {
        void RegisterParamToReceive(IReceivableModbusRtuParameter parameter);
    }
}
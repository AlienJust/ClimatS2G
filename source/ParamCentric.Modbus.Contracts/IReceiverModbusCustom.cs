namespace ParamCentric.Modbus.Contracts
{
    public interface IReceiverModbusCustom
    {
        void RegisterParamToReceive(IReceivableModbusCustomParameter parameter);
    }
}
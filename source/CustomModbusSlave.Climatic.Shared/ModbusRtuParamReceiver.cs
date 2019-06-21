using System.Collections.Generic;
using AlienJust.Support.Collections;
using ParamCentric.Modbus.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared
{
    public class ModbusRtuParamReceiver : IReceiverModbusRtu, ICmdListenerStd
    {
        private readonly List<IReceivableModbusRtuParameter> _params;

        public ModbusRtuParamReceiver()
        {
            _params = new List<IReceivableModbusRtuParameter>();
        }

        public void RegisterParamToReceive(IReceivableModbusRtuParameter parameter)
        {
            _params.Add(parameter);
        }

        public void ReceiveCommand(byte addr, byte code, byte[] data)
        {
            if (data.Length % 2 != 0)
            {
                // ответ ModbusRTU всегда нечетный! (запрос чётный)
                foreach (var parameter in _params)
                {
                    if (parameter.Address == addr && parameter.CommandCode == code)
                    {
                        try
                        {
                            parameter.ReceivedBytesValue = new BytesPair(
                                data[3 + parameter.ZeroBasedParameterNumber * 2],
                                data[4 + parameter.ZeroBasedParameterNumber * 2]);
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                }
            }
        }
    }
}
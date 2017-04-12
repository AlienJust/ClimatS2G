using System.Collections.Generic;
using AlienJust.Support.Collections;
using CustomModbusSlave.Es2gClimatic.Shared;
using ParamCentric.Modbus.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp {
	class ModbusRtuParamReceiver : IReceiverModbusRtu, ICommandListener {
		private readonly List<IReceivableModbusRtuParameter> _params;
		public ModbusRtuParamReceiver() {
			_params = new List<IReceivableModbusRtuParameter>();
		}

		public void RegisterParamToReceive(IReceivableModbusRtuParameter parameter) {
			_params.Add(parameter);
		}

		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (data.Count % 2 != 0) { // ответ ModbusRTU всегда нечетный! (запрос чётный)
				foreach (var parameter in _params) {
					if (parameter.Address == addr && parameter.CommandCode == code) {
						try {
							parameter.ReceivedBytesValue = new BytesPair(data[3 + parameter.ZeroBasedParameterNumber * 2], data[4 + parameter.ZeroBasedParameterNumber * 2]);
						}
						catch {
							// ignored
						}
					}
				}
			}
		}
	}
}
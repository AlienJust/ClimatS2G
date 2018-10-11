using System.Collections.Generic;

namespace ParamControls.Vm {
	/*
	class ChartReadyParameterSimple : IChartReadyParameter {
		private readonly IReceivableParameter _receivableParameter;
		private readonly Func<IList<byte>, double> _chartDataFunc;
		public ChartReadyParameterSimple(IReceivableParameter receivableParameter, Func<IList<byte>, double> chartDataFunc) {
			_receivableParameter = receivableParameter;
			_chartDataFunc = chartDataFunc;
		}

		public void SetReceivedValueFromCommandPart(IList<byte> commandPartDataWithoutHeaderAndCrc) {
			_receivableParameter.SetReceivedValueFromCommandPart(commandPartDataWithoutHeaderAndCrc);
			_receivableParameter.NotifyDataReceived += ReceivableParameterOnNotifyDataReceived;
		}

		private void ReceivableParameterOnNotifyDataReceived() {
			NotifyDataReceived?.Invoke();
		}

		public IList<byte> ReceivedRawValue => _receivableParameter.ReceivedRawValue;
		public event NotifyDataReceivedDelegate NotifyDataReceived;

		public double ChartValue => _chartDataFunc.Invoke(ReceivedRawValue);
	}
	*/


	public interface IReceivableParameter {
		//void SetReceivedValueFromCommandPart(IList<byte> commandPartDataWithoutHeaderAndCrc); // DriverSide
		string ReceiveName { get; }
		IList<byte> ReceivedRawValue { get; } // UserSide
		event NotifyDataReceivedDelegate NotifyDataReceived; // UserSide
	}
}

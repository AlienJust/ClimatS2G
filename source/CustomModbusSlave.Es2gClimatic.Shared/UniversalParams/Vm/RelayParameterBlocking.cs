using System;
using System.Collections.Generic;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace ParamControls.Vm {
	public sealed class RelayParameterBlocking : IReceivableParameter {
		private readonly Func<IList<byte>, IList<byte>> _dataGetter;
		private readonly ICmdListener<IList<byte>> _cmdPartListener;
		public string ReceiveName { get; }
		
		private IList<byte> _receivedRawValue;
		private readonly object _receivedRawValueSyncObj;
		
		public IList<byte> ReceivedRawValue {
			get { lock(_receivedRawValueSyncObj)return _receivedRawValue; }
			private set { lock(_receivedRawValueSyncObj)_receivedRawValue = value; }
		}

		public event NotifyDataReceivedDelegate NotifyDataReceived;

		public RelayParameterBlocking(string receiveName, ICmdListener<IList<byte>> cmdPartListener, Func<IList<byte>, IList<byte>> dataGetter) {
			ReceiveName = receiveName;
			_cmdPartListener = cmdPartListener;
			_dataGetter = dataGetter;

			_receivedRawValueSyncObj = new object();
			_cmdPartListener.DataReceived += CmdPartListenerOnDataReceived;
		}

		private void CmdPartListenerOnDataReceived(IList<byte> bytes, IList<byte> data) {
			Console.WriteLine("Parameter received from _cmdPartListener");
			SetReceivedValueFromCommandPart(data);
		}

		private void SetReceivedValueFromCommandPart(IList<byte> commandPartDataWithoutHeaderAndCrc) {
			ReceivedRawValue = _dataGetter(commandPartDataWithoutHeaderAndCrc); // TODO: Invoke when received, could lag driver thread!
			NotifyDataReceived?.Invoke();
		}
	}
}
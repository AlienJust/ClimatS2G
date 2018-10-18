using System;
using System.Collections.Generic;
using ParamControls.Vm;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm {
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T">Data output format</typeparam>
	/// <typeparam name="TR">Raw input format</typeparam>
	public sealed class RecvParam<T, TR> : IRecvParam<T> {
		private readonly Func<TR, T> _dataGetter;
		private readonly ICmdListener<TR> _cmdPartListener;
		public string ReceiveName { get; }

		private T _receivedRawValue;
		private readonly object _receivedRawValueSyncObj;

		public T ReceivedRawValue {
			get {
				lock (_receivedRawValueSyncObj) return _receivedRawValue;
			}
			private set {
				lock (_receivedRawValueSyncObj) _receivedRawValue = value;
			}
		}

		public event NotifyDataReceivedDelegate NotifyDataReceived;

		public RecvParam(string receiveName, ICmdListener<TR> cmdPartListener, Func<TR, T> dataGetter) {
			ReceiveName = receiveName;
			_cmdPartListener = cmdPartListener;
			_dataGetter = dataGetter;

			_receivedRawValueSyncObj = new object();
			_cmdPartListener.DataReceived += CmdPartListenerOnDataReceived;
		}

		private void CmdPartListenerOnDataReceived(IList<byte> bytes, TR data) {
			SetReceivedValueFromCommandPart(data);
		}

		private void SetReceivedValueFromCommandPart(TR commandPartDataWithoutHeaderAndCrc) {
			ReceivedRawValue = _dataGetter(commandPartDataWithoutHeaderAndCrc); // TODO: Invoke when received, could lag driver thread!
			NotifyDataReceived?.Invoke();
			//Console.WriteLine(ReceiveName + " value is " + ReceivedRawValue.ToText());
		}
	}
}
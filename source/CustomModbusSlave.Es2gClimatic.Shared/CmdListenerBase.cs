using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared {
	public abstract class CmdListenerBase<T> : ICmdListener<T> {
		private readonly byte _addrToCheck;
		private readonly byte _codeToCheck;
		private readonly int _length;

		protected CmdListenerBase(byte addrToCheck, byte codeToCheck, int length) {
			_addrToCheck = addrToCheck;
			_codeToCheck = codeToCheck;
			_length = length;
		}
		public event DataReceivedDelegate<T> DataReceived;

		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr == _addrToCheck && code == _codeToCheck && _length == data.Count) {
				OnDataReceived(data, BuildData(data));
			}
		}

		public abstract T BuildData(IList<byte> bytes);

		protected virtual void OnDataReceived(IList<byte> bytes, T data) {
			DataReceived?.Invoke(bytes, data);
		}
	}
}
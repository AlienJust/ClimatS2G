using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using System.Collections.Generic;
using CustomModbusSlave.MicroclimatEs2gApp.Common;

namespace CustomModbusSlave.MicroclimatEs2gApp.Bvs {

	public class BvsDataViewModel : ViewModelBase, ICommandListener {
		private readonly IThreadNotifier _notifier;
		private readonly byte _addr;
		private IBvsReply65Telemetry _reply65Telemetry;

		public BvsDataViewModel(IThreadNotifier notifier, byte addr) {
			_notifier = notifier;
			_addr = addr;
		}

		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (_addr == addr && code == 0x41 && data.Count == 7) {
				_notifier.Notify(() => {
					Reply65Telemetry = new BvsReply65TelemetryBuilder(data).Build();
				});
			}
		}

		public IBvsReply65Telemetry Reply65Telemetry {
			get { return _reply65Telemetry; }
			set {
				if (_reply65Telemetry != value) {
					_reply65Telemetry = value;
					RaisePropertyChanged(() => Reply65Telemetry);
				}
			}
		}
	}
}
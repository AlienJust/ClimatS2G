using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using System.Collections.Generic;

namespace CustomModbusSlave.MicroclimatEs2gApp.Bvs {

	internal class BvsDataViewModel : ViewModelBase, ICommandListener {
		private readonly IThreadNotifier _notifier;
		private IBvsReply65Telemetry _reply65Telemetry;

		private BvsDataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;
		}

		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr == 0x1E && code == 0x41) {
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
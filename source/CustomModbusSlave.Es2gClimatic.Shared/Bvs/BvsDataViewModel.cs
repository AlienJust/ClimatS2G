using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.Bvs {

	public class BvsDataViewModel : ViewModelBase {
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IBvsReply65Telemetry> _cmdListenerBvsReply65;
		private IBvsReply65Telemetry _reply65Telemetry;
		public AnyCommandPartViewModel BvsReply41TextVm { get; }

		public BvsDataViewModel(IThreadNotifier notifier, ICmdListener<IBvsReply65Telemetry> cmdListenerBvsReply65) {
			_notifier = notifier;
			_cmdListenerBvsReply65 = cmdListenerBvsReply65;
			BvsReply41TextVm = new AnyCommandPartViewModel();

			_cmdListenerBvsReply65.DataReceived += CmdListenerBvsReply65OnDataReceived;
		}

		private void CmdListenerBvsReply65OnDataReceived(IList<byte> bytes, IBvsReply65Telemetry data) {
			BvsReply41TextVm.Update(bytes);
			Reply65Telemetry = data;
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
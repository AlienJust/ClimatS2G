using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Build;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap
{
	class MukFlapDataViewModel : ViewModelBase, ICommandListener {
		private readonly IThreadNotifier _notifier;
		private string _reply;

		private IMukFlapReply03Telemetry _reply03Telemetry;

		public MukFlapDataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;
		}
		
		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr != 0x02) return;
			if (code == 0x03 && data.Count == 39) {
				_notifier.Notify(() => {
					Reply = data.ToText();
					Reply03Telemetry = new MukFlapReply03TelemetryBuilder(data).Build();
				});
			}
		}


		public IMukFlapReply03Telemetry Reply03Telemetry {
			get { return _reply03Telemetry; }
			set {
				if (_reply03Telemetry != value) {
					_reply03Telemetry = value;
					RaisePropertyChanged(() => Reply03Telemetry);
				}
			}
		}

		public string Reply
		{
			get { return _reply; }
			set
			{
				if (_reply != value)
				{
					_reply = value;
					RaisePropertyChanged(() => Reply);
				}
			}
		}
	}
}

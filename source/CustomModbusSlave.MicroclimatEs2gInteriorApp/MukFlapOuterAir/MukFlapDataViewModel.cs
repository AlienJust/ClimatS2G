using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Common;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Build;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.Request16;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir
{
	class MukFlapDataViewModel : ViewModelBase, ICommandListener {
		private readonly IThreadNotifier _notifier;
		private IMukFlapReply03Telemetry _reply03Telemetry;
		private IRequest16Data _reply16Telemetry;

		public MukFlapDataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;
			Reply03TelemetryText = new AnyCommandPartViewModel();
			Request16TelemetryText = new AnyCommandPartViewModel();
		}
		
		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr != 0x02) return;
			if (code == 0x03 && data.Count == 47) {
				_notifier.Notify(() => {
					Reply03TelemetryText.Update(data);
					Reply03Telemetry = new MukFlapReply03TelemetryBuilder(data).Build();
				});
			}
			else if (code == 0x10 && data.Count == 21) {
				_notifier.Notify(() => {
					Request16TelemetryText.Update(data);
					//Reply03Telemetry = new MukFlapRequest16TelemetryBuilder(data).Build();
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

		public IRequest16Data Reply16Telemetry {
			get { return _reply16Telemetry; }
			set {
				if (_reply16Telemetry != value) {
					_reply16Telemetry = value;
					RaisePropertyChanged(() => Reply16Telemetry);
				}
			}
		}

		public AnyCommandPartViewModel Reply03TelemetryText { get; }
		public AnyCommandPartViewModel Request16TelemetryText { get; }
	}
}

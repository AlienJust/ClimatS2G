﻿using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Common;
using CustomModbusSlave.MicroclimatEs2gApp.MukAirExhauster.Data.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukAirExhauster.ViewModel
{
	class MukAirExhausterDataViewModel : ViewModelBase, ICommandListener {
		private readonly IThreadNotifier _notifier;
		private IReply03Data _reply03Telemetry;

		public MukAirExhausterDataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;
			Reply03TelemetryText = new AnyCommandPartViewModel();
		}
		
		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr != 0x06) return;
			if (code == 0x03 && data.Count == 31) {
				_notifier.Notify(() => {
					Reply03TelemetryText.Update(data);
					Reply03Telemetry = new Reply03DataBuilder(data).Build();
				});
			}
		}


		public IReply03Data Reply03Telemetry {
			get { return _reply03Telemetry; }
			set {
				if (_reply03Telemetry != value) {
					_reply03Telemetry = value;
					RaisePropertyChanged(() => Reply03Telemetry);
				}
			}
		}

		public AnyCommandPartViewModel Reply03TelemetryText { get; }
	}
}
﻿using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Common;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm;
using CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapWinterSummer.DataModel.Builders;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapWinterSummer.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapWinterSummer.SetParameters;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapWinterSummer
{
	class MukFlapWinterSummerViewModel : ViewModelBase, ICommandListener {
		private readonly IThreadNotifier _notifier;
		private IMukFlapWinterSummerReply03Telemetry _reply03Telemetry;

		public MukFlapWinterSummerViewModel(IThreadNotifier notifier, IParameterSetter parameterSetter) {
			_notifier = notifier;
			Reply03TelemetryText = new AnyCommandPartViewModel();
			MukFlapWinterSummerSetParamsVm = new MukFlapWinterSummerSetParamsViewModel(notifier, parameterSetter);
		}
		
		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr != 0x08) return;
			if (code == 0x03 && data.Count == 47) {
				_notifier.Notify(() => {
					Reply03TelemetryText.Update(data);
					Reply03Telemetry = new MukFlapWinterSummerReply03TelemetryBuilder(data).Build();
				});
			}
		}


		public IMukFlapWinterSummerReply03Telemetry Reply03Telemetry {
			get { return _reply03Telemetry; }
			set {
				if (_reply03Telemetry != value) {
					_reply03Telemetry = value;
					RaisePropertyChanged(() => Reply03Telemetry);
				}
			}
		}

		public AnyCommandPartViewModel Reply03TelemetryText { get; }

		public MukFlapWinterSummerSetParamsViewModel MukFlapWinterSummerSetParamsVm { get; }
	}
}

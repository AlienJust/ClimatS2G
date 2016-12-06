﻿using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapReturnAir.DataModel.Builders;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapReturnAir.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapReturnAir.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapReturnAir
{
	class MukFlapReturnAirViewModel : ViewModelBase, ICommandListener {
		private readonly IThreadNotifier _notifier;
		private IMukFlapReturnAirReply03Telemetry _reply03Telemetry;

		public MukFlapReturnAirViewModel(IThreadNotifier notifier, IParameterSetter parameterSetter) {
			_notifier = notifier;
			Reply03TelemetryText = new AnyCommandPartViewModel();
			MukFlapReturnAirSetParamsVm = new MukFlapReturnAirSetParamsViewModel(notifier, parameterSetter);
		}
		
		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr != 0x07) return;
			if (code == 0x03 && data.Count == 43) {
				_notifier.Notify(() => {
					Reply03TelemetryText.Update(data);
					Reply03Telemetry = new MukFlapReturnAirReply03TelemetryBuilder(data).Build();
				});
			}
		}


		public IMukFlapReturnAirReply03Telemetry Reply03Telemetry {
			get { return _reply03Telemetry; }
			set {
				if (_reply03Telemetry != value) {
					_reply03Telemetry = value;
					RaisePropertyChanged(() => Reply03Telemetry);
				}
			}
		}

		public AnyCommandPartViewModel Reply03TelemetryText { get; }

		public MukFlapReturnAirSetParamsViewModel MukFlapReturnAirSetParamsVm { get; }
	}
}
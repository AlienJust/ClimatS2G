using System;
using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Request16;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap {
	class MukFlapDataViewModel : ViewModelBase {
		private readonly IThreadNotifier _notifier;

		private readonly ICmdListener<IMukFlapAirReply03Telemetry> _cmdListenerMukFlapOuterAirReply03;
		private readonly ICmdListener<IMukFlapAirRequest16Data> _cmdListenerMukFlapOuterAirRequest16;

		private IMukFlapAirReply03Telemetry _reply03Telemetry;
		private IMukFlapAirRequest16Data _request16Telemetry;

		public AnyCommandPartViewModel Reply03TelemetryText { get; }
		public AnyCommandPartViewModel Request16TelemetryText { get; }
		public MukFlapSetParamsViewModel MukFlapSetParamsVm { get; }

		public MukFlapDataViewModel(IThreadNotifier notifier, IParameterSetter parameterSetter, ICmdListener<IMukFlapAirReply03Telemetry> cmdListenerMukFlapOuterAirReply03,
		ICmdListener<IMukFlapAirRequest16Data> cmdListenerMukFlapOuterAirRequest16) {
			_notifier = notifier;

			_cmdListenerMukFlapOuterAirReply03 = cmdListenerMukFlapOuterAirReply03;
			_cmdListenerMukFlapOuterAirRequest16 = cmdListenerMukFlapOuterAirRequest16;

			Reply03TelemetryText = new AnyCommandPartViewModel();
			Request16TelemetryText = new AnyCommandPartViewModel();
			MukFlapSetParamsVm = new MukFlapSetParamsViewModel(_notifier, parameterSetter);

			_cmdListenerMukFlapOuterAirReply03.DataReceived += CmdListenerMukFlapOuterAirReply03OnDataReceived;
			_cmdListenerMukFlapOuterAirRequest16.DataReceived += CmdListenerMukFlapOuterAirRequest16OnDataReceived;
		}

		private void CmdListenerMukFlapOuterAirReply03OnDataReceived(IList<byte> bytes, IMukFlapAirReply03Telemetry data) {
			_notifier.Notify(() => {
				Reply03TelemetryText.Update(bytes);
				Reply03Telemetry = data;
			});
		}

		private void CmdListenerMukFlapOuterAirRequest16OnDataReceived(IList<byte> bytes, IMukFlapAirRequest16Data data) {
			_notifier.Notify(() => {
				Request16TelemetryText.Update(bytes);
				Request16Telemetry = data;
			});
		}
		

		public IMukFlapAirReply03Telemetry Reply03Telemetry {
			get => _reply03Telemetry;
			set {
				if (_reply03Telemetry != value) {
					_reply03Telemetry = value;
					RaisePropertyChanged(() => Reply03Telemetry);
				}
			}
		}

		public IMukFlapAirRequest16Data Request16Telemetry {
			get => _request16Telemetry;
			set {
				if (_request16Telemetry != value) {
					_request16Telemetry = value;
					RaisePropertyChanged(() => Request16Telemetry);
				}
			}
		}
	}
}

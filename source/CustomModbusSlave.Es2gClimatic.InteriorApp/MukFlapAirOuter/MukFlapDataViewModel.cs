using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter
{
	class MukFlapDataViewModel : ViewModelBase {
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IMukFlapOuterAirReply03Telemetry> _cmdListenerMukFlapOuterAirReply03;
		private readonly ICmdListener<IMukFlapOuterAirRequest16Data> _cmdListenerMukFlapOuterAirRequest16;

		private IMukFlapOuterAirReply03Telemetry _reply03Telemetry;
		private IMukFlapOuterAirRequest16Data _request16Telemetry;

		public AnyCommandPartViewModel Reply03TelemetryText { get; }
		public AnyCommandPartViewModel Request16TelemetryText { get; }

		public MukFlapOuterAirSetParamsViewModel MukFlapOuterAirSetParamsVm { get; }

		public MukFlapDataViewModel(
			IThreadNotifier notifier, 
			IParameterSetter parameterSetter, 
			ICmdListener<IMukFlapOuterAirReply03Telemetry> cmdListenerMukFlapOuterAirReply03, 
			ICmdListener<IMukFlapOuterAirRequest16Data> cmdListenerMukFlapOuterAirRequest16) {

			_notifier = notifier;
			_cmdListenerMukFlapOuterAirReply03 = cmdListenerMukFlapOuterAirReply03;
			_cmdListenerMukFlapOuterAirRequest16 = cmdListenerMukFlapOuterAirRequest16;
			Reply03TelemetryText = new AnyCommandPartViewModel();
			Request16TelemetryText = new AnyCommandPartViewModel();

			MukFlapOuterAirSetParamsVm = new MukFlapOuterAirSetParamsViewModel(notifier, parameterSetter);
			_cmdListenerMukFlapOuterAirReply03.DataReceived += CmdListenerMukFlapOuterAirReply03OnDataReceived;
			_cmdListenerMukFlapOuterAirRequest16.DataReceived += CmdListenerMukFlapOuterAirRequest16OnDataReceived;
		}

		private void CmdListenerMukFlapOuterAirRequest16OnDataReceived(IList<byte> bytes, IMukFlapOuterAirRequest16Data data) {
			_notifier.Notify(() => {
				Request16TelemetryText.Update(bytes);
				Request16Telemetry = data;
			});
		}

		private void CmdListenerMukFlapOuterAirReply03OnDataReceived(IList<byte> bytes, IMukFlapOuterAirReply03Telemetry data) {
			_notifier.Notify(() => {
				Reply03TelemetryText.Update(bytes);
				Reply03Telemetry = data;
			});
		}

		public IMukFlapOuterAirReply03Telemetry Reply03Telemetry {
			get => _reply03Telemetry;
			set {
				if (_reply03Telemetry != value) {
					_reply03Telemetry = value;
					RaisePropertyChanged(() => Reply03Telemetry);
				}
			}
		}

		public IMukFlapOuterAirRequest16Data Request16Telemetry {
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

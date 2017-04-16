using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir
{
	class MukFlapDataViewModel : ViewModelBase {
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IMukFlapReply03Telemetry> _cmdListenerMukFlapOuterAirReply03;
		private readonly ICmdListener<IMukFlapOuterAirRequest16Data> _cmdListenerMukFlapOuterAirRequest16;

		private IMukFlapReply03Telemetry _reply03Telemetry;
		private IMukFlapOuterAirRequest16Data _request16Telemetry;

		public MukFlapDataViewModel(
			IThreadNotifier notifier, 
			IParameterSetter parameterSetter, 
			ICmdListener<IMukFlapReply03Telemetry> cmdListenerMukFlapOuterAirReply03, 
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

		private void CmdListenerMukFlapOuterAirReply03OnDataReceived(IList<byte> bytes, IMukFlapReply03Telemetry data) {
			_notifier.Notify(() => {
				Reply03TelemetryText.Update(bytes);
				Reply03Telemetry = data;
			});
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

		public IMukFlapOuterAirRequest16Data Request16Telemetry {
			get { return _request16Telemetry; }
			set {
				if (_request16Telemetry != value) {
					_request16Telemetry = value;
					RaisePropertyChanged(() => Request16Telemetry);
				}
			}
		}

		public AnyCommandPartViewModel Reply03TelemetryText { get; }
		public AnyCommandPartViewModel Request16TelemetryText { get; }

		public MukFlapOuterAirSetParamsViewModel MukFlapOuterAirSetParamsVm { get; }
	}
}

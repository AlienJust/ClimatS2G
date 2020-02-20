using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle
{
	class MukFlapReturnAirViewModel : ViewModelBase {
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IMukFlapReturnAirReply03Telemetry> _cmdListenerMukFlapReturnAirReply03;
		private readonly ICmdListener<IMukFlapAirRecycleRequest16Data> _cmdListenerMukFlapAirRecycleRequest16;
		private IMukFlapReturnAirReply03Telemetry _reply03Telemetry;
		private IMukFlapAirRecycleRequest16Data _request16Data;

		public AnyCommandPartViewModel Reply03TelemetryText { get; }
		public AnyCommandPartViewModel Request16DataText { get; }

		public MukFlapReturnAirSetParamsViewModel MukFlapReturnAirSetParamsVm { get; }

		public MukFlapReturnAirViewModel(IThreadNotifier notifier, 
			IParameterSetter parameterSetter, 
			ICmdListener<IMukFlapReturnAirReply03Telemetry> cmdListenerMukFlapReturnAirReply03,
			ICmdListener<IMukFlapAirRecycleRequest16Data> cmdListenerMukFlapAirRecycleRequest16) {
			_notifier = notifier;
			_cmdListenerMukFlapReturnAirReply03 = cmdListenerMukFlapReturnAirReply03;
			_cmdListenerMukFlapAirRecycleRequest16 = cmdListenerMukFlapAirRecycleRequest16;
			Reply03TelemetryText = new AnyCommandPartViewModel();
			Request16DataText = new AnyCommandPartViewModel();
			MukFlapReturnAirSetParamsVm = new MukFlapReturnAirSetParamsViewModel(notifier, parameterSetter);

			_cmdListenerMukFlapReturnAirReply03.DataReceived += CmdListenerMukFlapReturnAirReply03OnDataReceived;
			_cmdListenerMukFlapAirRecycleRequest16.DataReceived += CmdListenerMukFlapAirRecycleRequest16OnDataReceived;
		}

		private void CmdListenerMukFlapReturnAirReply03OnDataReceived(IList<byte> bytes, IMukFlapReturnAirReply03Telemetry data) {
			_notifier.Notify(() => {
				Reply03TelemetryText.Update(bytes);
				Reply03Telemetry = data;
			});
		}

		private void CmdListenerMukFlapAirRecycleRequest16OnDataReceived(IList<byte> bytes, IMukFlapAirRecycleRequest16Data data) {
			_notifier.Notify(() => {
				Request16DataText.Update(bytes);
				Request16Data = data;
			});
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

		public IMukFlapAirRecycleRequest16Data Request16Data {
			get { return _request16Data; }
			set {
				if (_request16Data != value) {
					_request16Data = value;
					RaisePropertyChanged(() => Request16Data);
				}
			}
		}
	}
}

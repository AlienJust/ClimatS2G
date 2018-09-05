using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Data.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.ViewModel
{
	class MukAirExhausterDataViewModel : ViewModelBase {
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IMukAirExhausterReply03Data> _cmdListenerMukAirExhausterReply03;
		private readonly ICmdListener<IMukFanAirExhausterRequest16Data> _cmdListenerMukAirExhausterRequest16;

		private IMukAirExhausterReply03Data _reply03Telemetry;
		private IMukFanAirExhausterRequest16Data _request16Telemetry;

		public AnyCommandPartViewModel Reply03TelemetryText { get; }
		public AnyCommandPartViewModel Request16TelemetryText { get; }
		public MukAirExhausterSetParamsViewModel MukAirExhausterSetParamsVm { get; }

		public MukAirExhausterDataViewModel(IThreadNotifier notifier, IParameterSetter parameterSetter, ICmdListener<IMukAirExhausterReply03Data> cmdListenerMukAirExhausterReply03, ICmdListener<IMukFanAirExhausterRequest16Data> cmdListenerMukAirExhausterRequest16) {
			_notifier = notifier;
			_cmdListenerMukAirExhausterReply03 = cmdListenerMukAirExhausterReply03;
			_cmdListenerMukAirExhausterRequest16 = cmdListenerMukAirExhausterRequest16;

			Reply03TelemetryText = new AnyCommandPartViewModel();
			Request16TelemetryText = new AnyCommandPartViewModel();

			MukAirExhausterSetParamsVm = new MukAirExhausterSetParamsViewModel(notifier, parameterSetter);

			_cmdListenerMukAirExhausterReply03.DataReceived += CmdListenerMukAirExhausterReply03OnDataReceived;
			_cmdListenerMukAirExhausterRequest16.DataReceived += CmdListenerMukAirExhausterRequest16OnDataReceived;
		}
		
		private void CmdListenerMukAirExhausterReply03OnDataReceived(IList<byte> bytes, IMukAirExhausterReply03Data data) {
			_notifier.Notify(() => {
				Reply03TelemetryText.Update(bytes);
				Reply03Telemetry = data;
			});
		}

		private void CmdListenerMukAirExhausterRequest16OnDataReceived(IList<byte> bytes, IMukFanAirExhausterRequest16Data data) {
			_notifier.Notify(() => {
				Request16TelemetryText.Update(bytes);
				Request16Telemetry = data;
			});
		}

		public IMukAirExhausterReply03Data Reply03Telemetry {
			get { return _reply03Telemetry; }
			set {
				if (_reply03Telemetry != value) {
					_reply03Telemetry = value;
					RaisePropertyChanged(() => Reply03Telemetry);
				}
			}
		}

		public IMukFanAirExhausterRequest16Data Request16Telemetry {
			get { return _request16Telemetry; }
			set {
				if (_request16Telemetry != value) {
					_request16Telemetry = value;
					RaisePropertyChanged(() => Request16Telemetry);
				}
			}
		}
	}
}

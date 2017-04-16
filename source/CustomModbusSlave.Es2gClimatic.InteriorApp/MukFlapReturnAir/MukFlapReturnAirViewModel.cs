using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapReturnAir.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapReturnAir.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapReturnAir
{
	class MukFlapReturnAirViewModel : ViewModelBase {
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IMukFlapReturnAirReply03Telemetry> _cmdListenerMukFlapReturnAirReply03;
		private IMukFlapReturnAirReply03Telemetry _reply03Telemetry;

		public MukFlapReturnAirViewModel(IThreadNotifier notifier, IParameterSetter parameterSetter, ICmdListener<IMukFlapReturnAirReply03Telemetry> cmdListenerMukFlapReturnAirReply03) {
			_notifier = notifier;
			_cmdListenerMukFlapReturnAirReply03 = cmdListenerMukFlapReturnAirReply03;
			Reply03TelemetryText = new AnyCommandPartViewModel();
			MukFlapReturnAirSetParamsVm = new MukFlapReturnAirSetParamsViewModel(notifier, parameterSetter);
			_cmdListenerMukFlapReturnAirReply03.DataReceived += CmdListenerMukFlapReturnAirReply03OnDataReceived;
		}

		private void CmdListenerMukFlapReturnAirReply03OnDataReceived(IList<byte> bytes, IMukFlapReturnAirReply03Telemetry data) {
			_notifier.Notify(() => {
				Reply03TelemetryText.Update(bytes);
				Reply03Telemetry = data;
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

		public AnyCommandPartViewModel Reply03TelemetryText { get; }

		public MukFlapReturnAirSetParamsViewModel MukFlapReturnAirSetParamsVm { get; }
	}
}

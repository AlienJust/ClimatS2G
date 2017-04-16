using System;
using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Data;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Data.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.ViewModel
{
	class MukAirExhausterDataViewModel : ViewModelBase {
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IMukAirExhausterReply03Data> _cmdListenerMukAirExhausterReply03;
		private IMukAirExhausterReply03Data _reply03Telemetry;

		public MukAirExhausterDataViewModel(IThreadNotifier notifier, IParameterSetter parameterSetter, ICmdListener<IMukAirExhausterReply03Data> cmdListenerMukAirExhausterReply03) {
			_notifier = notifier;
			_cmdListenerMukAirExhausterReply03 = cmdListenerMukAirExhausterReply03;
			Reply03TelemetryText = new AnyCommandPartViewModel();
			MukAirExhausterSetParamsVm = new MukAirExhausterSetParamsViewModel(notifier, parameterSetter);

			_cmdListenerMukAirExhausterReply03.DataReceived += CmdListenerMukAirExhausterReply03OnDataReceived;
		}

		private void CmdListenerMukAirExhausterReply03OnDataReceived(IList<byte> bytes, IMukAirExhausterReply03Data data) {
			_notifier.Notify(() => {
				Reply03TelemetryText.Update(bytes);
				Reply03Telemetry = new MukAirExhausterReply03DataBuilder(bytes).Build();
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

		public AnyCommandPartViewModel Reply03TelemetryText { get; }

		public MukAirExhausterSetParamsViewModel MukAirExhausterSetParamsVm { get; }
	}
}

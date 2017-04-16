using System;
using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer
{
	class MukFlapWinterSummerViewModel : ViewModelBase {
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IMukFlapWinterSummerReply03Telemetry> _cmdListenerMukFlapWinterSummerReply03;
		private IMukFlapWinterSummerReply03Telemetry _reply03Telemetry;

		public MukFlapWinterSummerViewModel(IThreadNotifier notifier, IParameterSetter parameterSetter, ICmdListener<IMukFlapWinterSummerReply03Telemetry> _cmdListenerMukFlapWinterSummerReply03) {
			_notifier = notifier;
			this._cmdListenerMukFlapWinterSummerReply03 = _cmdListenerMukFlapWinterSummerReply03;
			Reply03TelemetryText = new AnyCommandPartViewModel();
			MukFlapWinterSummerSetParamsVm = new MukFlapWinterSummerSetParamsViewModel(notifier, parameterSetter);
			_cmdListenerMukFlapWinterSummerReply03.DataReceived += CmdListenerMukFlapWinterSummerReply03OnDataReceived;
		}

		private void CmdListenerMukFlapWinterSummerReply03OnDataReceived(IList<byte> bytes, IMukFlapWinterSummerReply03Telemetry data) {
			_notifier.Notify(() => {
				Reply03TelemetryText.Update(bytes);
				Reply03Telemetry = data;
			});
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

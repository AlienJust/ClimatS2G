using System;
using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirWinterSummer.Request16;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer
{
	class MukFlapWinterSummerViewModel : ViewModelBase {
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IMukFlapWinterSummerReply03Telemetry> _cmdListenerMukFlapWinterSummerReply03;
		private readonly ICmdListener<IMukFlapAirWinterSummerRequest16Data> _cmdListenerMukAirFlapWinterSummerRequest16;
		private IMukFlapWinterSummerReply03Telemetry _reply03Telemetry;
		private IMukFlapAirWinterSummerRequest16Data _request16Data;

		public AnyCommandPartViewModel Reply03TelemetryText { get; }
		public AnyCommandPartViewModel Request16DataText { get; }

		public MukFlapWinterSummerSetParamsViewModel MukFlapWinterSummerSetParamsVm { get; }

		public MukFlapWinterSummerViewModel(IThreadNotifier notifier, 
			IParameterSetter parameterSetter, 
			ICmdListener<IMukFlapWinterSummerReply03Telemetry> cmdListenerMukFlapWinterSummerReply03, 
			ICmdListener<IMukFlapAirWinterSummerRequest16Data> cmdListenerMukAirFlapWinterSummerRequest16) {
			_notifier = notifier;
			_cmdListenerMukFlapWinterSummerReply03 = cmdListenerMukFlapWinterSummerReply03;
			_cmdListenerMukAirFlapWinterSummerRequest16 = cmdListenerMukAirFlapWinterSummerRequest16;
			Reply03TelemetryText = new AnyCommandPartViewModel();
			Request16DataText = new AnyCommandPartViewModel();
			MukFlapWinterSummerSetParamsVm = new MukFlapWinterSummerSetParamsViewModel(notifier, parameterSetter);

			_cmdListenerMukFlapWinterSummerReply03.DataReceived += CmdListenerMukFlapWinterSummerReply03OnDataReceived;
			_cmdListenerMukAirFlapWinterSummerRequest16.DataReceived += CmdListenerMukAirFlapWinterSummerRequest16OnDataReceived;
		}
		
		private void CmdListenerMukFlapWinterSummerReply03OnDataReceived(IList<byte> bytes, IMukFlapWinterSummerReply03Telemetry data) {
			_notifier.Notify(() => {
				Reply03TelemetryText.Update(bytes);
				Reply03Telemetry = data;
			});
		}

		private void CmdListenerMukAirFlapWinterSummerRequest16OnDataReceived(IList<byte> bytes, IMukFlapAirWinterSummerRequest16Data data) {
			_notifier.Notify(() => {
				Request16DataText.Update(bytes);
				Request16Data = data;
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

		public IMukFlapAirWinterSummerRequest16Data Request16Data {
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

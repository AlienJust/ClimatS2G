using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Build;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Request16;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap
{
	class MukFlapDataViewModel : ViewModelBase, ICmdListenerStd {
		private readonly IThreadNotifier _notifier;
		private string _reply;

		private IMukFlapReply03Telemetry _reply03Telemetry;
		private IRequest16Data _request16Telemetry;

		public AnyCommandPartViewModel Reply03TelemetryText { get; }
		public AnyCommandPartViewModel Request16TelemetryText { get; }
		public MukFlapSetParamsViewModel MukFlapSetParamsVm { get; }

		public MukFlapDataViewModel(IThreadNotifier notifier, IParameterSetter parameterSetter) {
			_notifier = notifier;

			Reply03TelemetryText = new AnyCommandPartViewModel();
			Request16TelemetryText = new AnyCommandPartViewModel();
			MukFlapSetParamsVm = new MukFlapSetParamsViewModel(_notifier, parameterSetter);
		}
		
		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr != 0x02) return;
			if (code == 0x03 && data.Count == 39) {
				_notifier.Notify(() => {
					Reply = data.ToText();
					Reply03Telemetry = new MukFlapReply03TelemetryBuilder(data).Build();
				});
			}
			else if (code == 0x10 && data.Count == 21) {
				_notifier.Notify(() => {
					Request16TelemetryText.Update(data);
					Request16Telemetry = new Request16DataBuilder(data).Build();
				});
			}
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

		public IRequest16Data Request16Telemetry
		{
			get { return _request16Telemetry; }
			set
			{
				if (_request16Telemetry != value) {
					_request16Telemetry = value;
					RaisePropertyChanged(() => Request16Telemetry);
				}
			}
		}


		public string Reply
		{
			get { return _reply; }
			set
			{
				if (_reply != value)
				{
					_reply = value;
					RaisePropertyChanged(() => Reply);
				}
			}
		}
	}
}

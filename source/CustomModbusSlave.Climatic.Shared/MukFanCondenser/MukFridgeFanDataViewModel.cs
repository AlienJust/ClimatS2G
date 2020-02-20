using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser {
	public class MukFridgeFanDataViewModel : ViewModelBase {
		
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IMukCondensorFanReply03Data> _cmdListenerMukFridgeFanReply03;
		private readonly ICmdListener<IMukCondenserRequest16Data> _cmdListenerMukCondenserRequest16;

		// 03
		private IMukCondensorFanReply03Data _reply03Data;
		public AnyCommandPartViewModel Reply03DataText { get; }

		// 16
		private IMukCondenserRequest16Data _request16Data;
		public AnyCommandPartViewModel Request16DataText { get; }

		// set params
		public MukFridgeSetParamsViewModel MukFridgeSetParamsVm { get; }

		public MukFridgeFanDataViewModel(
			IThreadNotifier notifier, 
			IParameterSetter parameterSetter, 
			ICmdListener<IMukCondensorFanReply03Data> cmdListenerMukFridgeFanReply03,
			ICmdListener<IMukCondenserRequest16Data> cmdListenerMukCondenserRequest16) {

			_notifier = notifier;
			_cmdListenerMukFridgeFanReply03 = cmdListenerMukFridgeFanReply03;
			_cmdListenerMukCondenserRequest16 = cmdListenerMukCondenserRequest16;

			Reply03DataText = new AnyCommandPartViewModel();

			Request16DataText = new AnyCommandPartViewModel();
			MukFridgeSetParamsVm = new MukFridgeSetParamsViewModel(notifier, parameterSetter);
			
			_cmdListenerMukFridgeFanReply03.DataReceived += CmdListenerMukFridgeFanReply03OnDataReceived;
			_cmdListenerMukCondenserRequest16.DataReceived += CmdListenerMukCondenserRequest16OnDataReceived;
		}

		private void CmdListenerMukFridgeFanReply03OnDataReceived(IList<byte> bytes, IMukCondensorFanReply03Data data) {
			_notifier.Notify(() => {
				Reply03DataText.Update(bytes);
				Reply03Data = data;
			});
		}

		private void CmdListenerMukCondenserRequest16OnDataReceived(IList<byte> bytes, IMukCondenserRequest16Data data) {
			_notifier.Notify(() => {
				Request16DataText.Update(bytes);
				Request16Data = data;
			});
		}

		public IMukCondensorFanReply03Data Reply03Data {
			get => _reply03Data;
			set {
				if (_reply03Data != value) {
					_reply03Data = value;
					RaisePropertyChanged(() => Reply03Data);
				}
			}
		}

		public IMukCondenserRequest16Data Request16Data {
			get => _request16Data;
			set {
				if (_request16Data != value) {
					_request16Data = value;
					RaisePropertyChanged(() => Request16Data);
				}
			}
		}
	}
}

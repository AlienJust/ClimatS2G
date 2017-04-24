using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFridge;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFridge.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared.MukCondenser.Request16;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser.Reply03;
using CustomModbusSlave.Es2gClimatic.Shared.SensorIndications;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser {
	public class MukFridgeFanDataViewModel : ViewModelBase {
		
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IMukCondensorFanReply03Data> _cmdListenerMukFridgeFanReply03;
		private readonly ICmdListener<IMukCondenserRequest16Data> _cmdListenerMukCondenserRequest16;

		private string _fanPwm;
		private string _condensingPressure;
		private string _incomingSignals;
		private string _outgoingSignals;
		private string _analogInput;
		private string _automaticModeStage;
		private string _workMode;
		private string _diagnostic1;
		private string _diagnostic2;
		private string _fanSpeed;
		private string _firmwareBuildNumber;

		// 03
		private string _reply;
		private IMukCondensorFanReply03Data _data;
		
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

			Request16DataText = new AnyCommandPartViewModel();
			MukFridgeSetParamsVm = new MukFridgeSetParamsViewModel(notifier, parameterSetter);
			
			_cmdListenerMukFridgeFanReply03.DataReceived += CmdListenerMukFridgeFanReply03OnDataReceived;
			_cmdListenerMukCondenserRequest16.DataReceived += CmdListenerMukCondenserRequest16OnDataReceived;
		}

		private void CmdListenerMukFridgeFanReply03OnDataReceived(IList<byte> bytes, IMukCondensorFanReply03Data data) {
			_notifier.Notify(() => {
				_data = data;
				FanPwm = data.FanPwm.ToString("f2");

				CondensingPressure = data.CondensingPressure.NoLinkWithSensor ? SensorIndicationExt.NoLinkText: data.CondensingPressure.Indication.ToString("f2");

				IncomingSignals = data.IncomingSignals.ToString("X2");
				OutgoingSignals = data.OutgoingSignals.ToString("X2");

				AnalogInput = data.AnalogInput.ToString();
				AutomaticModeStage = data.AutomaticModeStage.ToString();
				WorkMode = data.WorkMode.ToString();
				Diagnostic1 = data.Diagnostic1.ToString("X4");
				Diagnostic2 = data.Diagnostic2.ToString("X4");
				FanSpeed = data.FanSpeed.ToString();

				FirmwareBuildNumber = new TextFormatterIntegerDotted().Format(data.FirmwareBuildNumber);

				RaisePropertyChanged(() => Stage1IsOn); // TODO: redo
				RaisePropertyChanged(() => Stage2IsOn); // TODO: redo

				Reply = bytes.ToText();
			});
		}

		private void CmdListenerMukCondenserRequest16OnDataReceived(IList<byte> bytes, IMukCondenserRequest16Data data) {
			_notifier.Notify(() => {
				Request16DataText.Update(bytes);
				Request16Data = data;
			});
		}

		public string FanPwm {
			get { return _fanPwm; }
			set {
				if (_fanPwm != value) {
					_fanPwm = value;
					RaisePropertyChanged(() => FanPwm);
				}
			}
		}

		public string CondensingPressure {
			get { return _condensingPressure; }
			set {
				if (_condensingPressure != value) {
					_condensingPressure = value;
					RaisePropertyChanged(() => CondensingPressure);
				}
			}
		}

		public string IncomingSignals {
			get { return _incomingSignals; }
			set {
				if (_incomingSignals != value) {
					_incomingSignals = value;
					RaisePropertyChanged(() => IncomingSignals);
				}
			}
		}


		public bool? Stage1IsOn => _data?.Stage1IsOn;
		public bool? Stage2IsOn => _data?.Stage2IsOn;

		public string OutgoingSignals {
			get { return _outgoingSignals; }
			set {
				if (_outgoingSignals != value) {
					_outgoingSignals = value;
					RaisePropertyChanged(() => OutgoingSignals);
				}
			}
		}

		public string AnalogInput {
			get { return _analogInput; }
			set {
				if (_analogInput != value) {
					_analogInput = value;
					RaisePropertyChanged(() => AnalogInput);
				}
			}
		}

		public string AutomaticModeStage {
			get { return _automaticModeStage; }
			set { if (_automaticModeStage != value) { _automaticModeStage = value; RaisePropertyChanged(() => AutomaticModeStage); } }
		}

		public string WorkMode {
			get { return _workMode; }
			set { if (_workMode != value) { _workMode = value; RaisePropertyChanged(() => WorkMode); } }
		}

		public string Diagnostic1 {
			get { return _diagnostic1; }
			set { if (_diagnostic1 != value) { _diagnostic1 = value; RaisePropertyChanged(() => Diagnostic1); } }
		}

		public string Diagnostic2 {
			get { return _diagnostic2; }
			set { if (_diagnostic2 != value) { _diagnostic2 = value; RaisePropertyChanged(() => Diagnostic2); } }
		}

		public string FanSpeed {
			get { return _fanSpeed; }
			set { if (_fanSpeed != value) { _fanSpeed = value; RaisePropertyChanged(() => FanSpeed); } }
		}

		public string FirmwareBuildNumber {
			get { return _firmwareBuildNumber; }
			set {
				if (_firmwareBuildNumber != value) {
					_firmwareBuildNumber = value;
					RaisePropertyChanged(() => FirmwareBuildNumber);
				}
			}
		}

		public string Reply {
			get { return _reply; }
			set {
				if (_reply != value) {
					_reply = value;
					RaisePropertyChanged(() => Reply);
				}
			}
		}

		public IMukCondenserRequest16Data Request16Data {
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

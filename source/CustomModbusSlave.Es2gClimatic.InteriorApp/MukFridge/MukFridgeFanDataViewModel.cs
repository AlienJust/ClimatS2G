using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Numeric.Bits;
using AlienJust.Support.Text;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFridge.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.SensorIndications;
using CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFridge {
	class MukFridgeFanDataViewModel : ViewModelBase {
		
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IMukFridgeFanReply03Data> _cmdListenerMukFridgeFanReply03;
		//private readonly string _header = "МУК вентилятора конденсатора";
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
		private bool _stage1IsOn;
		private bool _stage2IsOn;

		private string _reply;
		private IMukFridgeFanReply03Data _data;
		public MukFridgeSetParamsViewModel MukFridgeSetParamsVm { get; }

		public MukFridgeFanDataViewModel(IThreadNotifier notifier, IParameterSetter parameterSetter, ICmdListener<IMukFridgeFanReply03Data> cmdListenerMukFridgeFanReply03) {
			_notifier = notifier;
			_cmdListenerMukFridgeFanReply03 = cmdListenerMukFridgeFanReply03;
			MukFridgeSetParamsVm = new MukFridgeSetParamsViewModel(notifier, parameterSetter);
			
			_cmdListenerMukFridgeFanReply03.DataReceived += CmdListenerMukFridgeFanReply03OnDataReceived;
		}

		private void CmdListenerMukFridgeFanReply03OnDataReceived(IList<byte> bytes, IMukFridgeFanReply03Data data) {
			_notifier.Notify(() => {
				_data = data;
				FanPwm = data.FanPwm.ToString("f2");

				CondensingPressure = data.CondensingPressure.NoLinkWithSensor ? SensorIndicationExt.NoLinkText: data.CondensingPressure.Indication.ToString("f2");

				IncomingSignals = data.IncomingSignals.ToString("X2");
				OutgoingSignals = data.OutgoingSignals.ToString("X2");

				AnalogInput = data.AnalogInput.ToString("X4");
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
	}
}

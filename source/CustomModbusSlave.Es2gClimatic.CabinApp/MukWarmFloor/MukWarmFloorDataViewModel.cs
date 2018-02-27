using System;
using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Request16;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.TextPresenters;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor {
	internal class MukWarmFloorDataViewModel : ViewModelBase {
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IMukWarmFloorReply03Data> _reply03Listener;
		private readonly ICmdListener<IMukWarmFloorRequest16Data> _request16Listener;

		private string _heatingPwm;
		private string _analogInput;
		private string _temperatureRegulatorWorkMode;
		private string _incomingSignals;
		private string _outgoingSignals;
		private string _automaticModeStage;
		private string _calculatedTemperatureSetting;
		private string _firmwareBuildNumber;
		private string _reply;
		private string _diagnostic1;
		private string _diagnostic2;
		private IMukWarmFloorRequest16Data _request16;
		private IMukWarmFloorReply03Data _reply03;

		public AnyCommandPartViewModel Reply03BytesTextVm { get; set; }
		public AnyCommandPartViewModel Request16BytesTextVm { get; set; }
		
		public MukWarmFloorSetParamsViewModel MukWarmFloorSetParamsVm { get; }

		public MukWarmFloorDataViewModel(IThreadNotifier notifier, IParameterSetter parameterSetter, ICmdListener<IMukWarmFloorReply03Data> reply03Listener, ICmdListener<IMukWarmFloorRequest16Data> request16Listener) {
			_notifier = notifier;
			_reply03Listener = reply03Listener;
			_request16Listener = request16Listener;

			Reply03BytesTextVm = new AnyCommandPartViewModel();
			Request16BytesTextVm = new AnyCommandPartViewModel();

			MukWarmFloorSetParamsVm = new MukWarmFloorSetParamsViewModel(notifier, parameterSetter);

			_reply03Listener.DataReceived += Reply03ListenerOnDataReceived;
			_request16Listener.DataReceived += Request16ListenerOnDataReceived;
		}

		private void Reply03ListenerOnDataReceived(IList<byte> bytes, IMukWarmFloorReply03Data data)
		{
			//if (code == 0x03 && data.Count == 31) {
			_notifier.Notify(() =>
			{
				Reply03 = data;

				HeatingPwm = (bytes[3] * 256.0 + bytes[4]).ToString("f0");

				AnalogInput = new UshortTextPresenter(bytes[6], bytes[5], false).PresentAsText();
				TemperatureRegulatorWorkMode = new DataDoubleTextPresenter(bytes[8], bytes[7], 0.01, 0).PresentAsText();

				IncomingSignals = new ByteTextPresenter(bytes[10], false).PresentAsText();
				OutgoingSignals = new ByteTextPresenter(bytes[12], false).PresentAsText();

				AutomaticModeStage = new UshortTextPresenter(bytes[14], bytes[13], false).PresentAsText();
				CalculatedTemperatureSetting = new DataDoubleTextPresenter(bytes[16], bytes[15], 0.01, 2).PresentAsText();

				Diagnostic1 = new UshortTextPresenter(bytes[18], bytes[17], true).PresentAsText(); // TODO: show as bits
				Diagnostic2 = new UshortTextPresenter(bytes[20], bytes[19], true).PresentAsText(); // TODO: show as bits

				FirmwareBuildNumber = new DataDoubleTextPresenter(bytes[22], bytes[21], 1.0, 0).PresentAsText();

				Reply03BytesTextVm.Update(bytes);
			});
		}

		private void Request16ListenerOnDataReceived(IList<byte> bytes, IMukWarmFloorRequest16Data data)
		{
			_notifier.Notify(() => {
				var request16 = new MukWarmFloorRequest16BuilderFromBytes(bytes).Build();
				Request16 = request16;
				Request16BytesTextVm.Update(bytes);
			});
		}

		public IMukWarmFloorRequest16Data Request16 {
			get => _request16;
			set {
				if (_request16 != value) {
					_request16 = value;
					RaisePropertyChanged("Request16");
				}
			}
		}

		public IMukWarmFloorReply03Data Reply03 {
			get => _reply03;
			set {
				if (_reply03 != value) {
					_reply03 = value;
					RaisePropertyChanged("Reply03");
				}
			}
		}

		public string HeatingPwm {
			get => _heatingPwm;
			set {
				if (_heatingPwm != value) {
					_heatingPwm = value;
					RaisePropertyChanged(() => HeatingPwm);
				}
			}
		}


		public string AnalogInput {
			get => _analogInput;
			set {
				if (_analogInput != value) {
					_analogInput = value;
					RaisePropertyChanged(() => AnalogInput);
				}
			}
		}

		public string TemperatureRegulatorWorkMode {
			get => _temperatureRegulatorWorkMode;
			set {
				if (_temperatureRegulatorWorkMode != value) {
					_temperatureRegulatorWorkMode = value;
					RaisePropertyChanged(() => TemperatureRegulatorWorkMode);
				}
			}
		}

		public string IncomingSignals {
			get => _incomingSignals;
			set {
				if (_incomingSignals != value) {
					_incomingSignals = value;
					RaisePropertyChanged(() => IncomingSignals);
				}
			}
		}

		public string OutgoingSignals {
			get => _outgoingSignals;
			set {
				if (_outgoingSignals != value) {
					_outgoingSignals = value;
					RaisePropertyChanged(() => OutgoingSignals);
				}
			}
		}

		public string AutomaticModeStage {
			get => _automaticModeStage;
			set {
				if (_automaticModeStage != value) {
					_automaticModeStage = value;
					RaisePropertyChanged(() => AutomaticModeStage);
				}
			}
		}

		public string CalculatedTemperatureSetting {
			get => _calculatedTemperatureSetting;
			set {
				if (_calculatedTemperatureSetting != value) {
					_calculatedTemperatureSetting = value;
					RaisePropertyChanged(() => CalculatedTemperatureSetting);
				}
			}
		}

		public string Diagnostic1 {
			get => _diagnostic1;
			set {
				if (_diagnostic1 != value) {
					_diagnostic1 = value;
					RaisePropertyChanged(() => Diagnostic1);
				}
			}
		}

		public string Diagnostic2 {
			get => _diagnostic2;
			set {
				if (_diagnostic2 != value) {
					_diagnostic2 = value;
					RaisePropertyChanged(() => Diagnostic2);
				}
			}
		}

		public string FirmwareBuildNumber {
			get => _firmwareBuildNumber;
			set {
				if (_firmwareBuildNumber != value) {
					_firmwareBuildNumber = value;
					RaisePropertyChanged(() => FirmwareBuildNumber);
				}
			}
		}

		public string Reply {
			get => _reply;
			set {
				if (_reply != value) {
					_reply = value;
					RaisePropertyChanged(() => Reply);
				}
			}
		}
	}
}

using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using CustomModbus.Slave.FastReply.Contracts;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFridge.Request16;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFridge.SetParameters;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.TextPresenters;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFridge
{
	class MukFridgeFanDataViewModel : ViewModelBase, ICommandListener
	{
		private readonly IThreadNotifier _notifier;
		private readonly string _header = "МУК вентилятора конденсатора";
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

		private string _reply;
		private IRequest16Data _request16Telemetry;
		public MukFridgeFanDataViewModel(IThreadNotifier notifier, IParameterSetter parameterSetter) {
			_notifier = notifier;
			Request16TelemetryText = new AnyCommandPartViewModel();
			MukFridgeSetParamsVm = new MukFridgeSetParamsViewModel(notifier, parameterSetter);
		}

		public void ReceiveCommand(byte addr, byte code, IList<byte> data)
		{
			if (addr != 0x04) return;
			if (code == 0x03 && data.Count == 29)
			{
				_notifier.Notify(() =>
				{
					FanPwm = (data[3] * 256.0 + data[4]).ToString("f0");

					CondensingPressure = new DataDoubleTextPresenter(data[6], data[5], 1.0, 0).PresentAsText();

					IncomingSignals = new ByteTextPresenter(data[8], false).PresentAsText();
					OutgoingSignals = new ByteTextPresenter(data[10], false).PresentAsText();
					AnalogInput = new UshortTextPresenter(data[12], data[11], false).PresentAsText();

					AutomaticModeStage = new UshortTextPresenter(data[14], data[13], false).PresentAsText();
					WorkMode = new UshortTextPresenter(data[16], data[15], false).PresentAsText(); // TODO: present as bits
					Diagnostic1 = new UshortTextPresenter(data[18], data[17], true).PresentAsText(); // TODO: present as bits
					Diagnostic2 = new UshortTextPresenter(data[20], data[19], true).PresentAsText(); // TODO: present as bits
					FanSpeed = new UshortTextPresenter(data[22], data[21], false).PresentAsText();

					FirmwareBuildNumber = (new DataDoubleTextPresenter(data[24], data[23], 1.0, 0)).PresentAsText();

					Reply = data.ToText();
				});
			}
			else if (code == 0x10 && data.Count == 15) {
				_notifier.Notify(() => {
					Request16TelemetryText.Update(data);
					Request16Telemetry = new Request16DataBuilder(data).Build();
				});
			}
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

		public string IncomingSignals
		{
			get { return _incomingSignals; }
			set
			{
				if (_incomingSignals != value)
				{
					_incomingSignals = value;
					RaisePropertyChanged(() => IncomingSignals);
				}
			}
		}

		public string OutgoingSignals
		{
			get { return _outgoingSignals; }
			set
			{
				if (_outgoingSignals != value)
				{
					_outgoingSignals = value;
					RaisePropertyChanged(() => OutgoingSignals);
				}
			}
		}

		public string AnalogInput
		{
			get { return _analogInput; }
			set
			{
				if (_analogInput != value)
				{
					_analogInput = value;
					RaisePropertyChanged(() => AnalogInput);
				}
			}
		}

		public string AutomaticModeStage
		{
			get { return _automaticModeStage; }
			set { if (_automaticModeStage != value) { _automaticModeStage = value; RaisePropertyChanged(() => AutomaticModeStage); } }
		}

		public string WorkMode
		{
			get { return _workMode; }
			set { if (_workMode != value) { _workMode = value; RaisePropertyChanged(() => WorkMode); } }
		}

		public string Diagnostic1
		{
			get { return _diagnostic1; }
			set { if (_diagnostic1 != value) { _diagnostic1 = value; RaisePropertyChanged(() => Diagnostic1); } }
		}

		public string Diagnostic2
		{
			get { return _diagnostic2; }
			set { if (_diagnostic2 != value) { _diagnostic2 = value; RaisePropertyChanged(() => Diagnostic2); } }
		}

		public string FanSpeed
		{
			get { return _fanSpeed; }
			set { if (_fanSpeed != value) { _fanSpeed = value; RaisePropertyChanged(() => FanSpeed); } }
		}
		
		public string FirmwareBuildNumber
		{
			get { return _firmwareBuildNumber; }
			set
			{
				if (_firmwareBuildNumber != value)
				{
					_firmwareBuildNumber = value;
					RaisePropertyChanged(() => FirmwareBuildNumber);
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


		public IRequest16Data Request16Telemetry {
			get { return _request16Telemetry; }
			set {
				if (_request16Telemetry != value) {
					_request16Telemetry = value;
					RaisePropertyChanged(() => Request16Telemetry);
				}
			}
		}
		public AnyCommandPartViewModel Request16TelemetryText { get; }

		public MukFridgeSetParamsViewModel MukFridgeSetParamsVm { get; }
	}
}

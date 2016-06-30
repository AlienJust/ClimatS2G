using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;
using CustomModbusSlave.MicroclimatEs2gApp.Common;
using CustomModbusSlave.MicroclimatEs2gApp.Common.TextPresenters;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;
using CustomModbusSlave.MicroclimatEs2gApp.MukFridge.SetParameters;
using CustomModbusSlave.MicroclimatEs2gApp.TextPresenters;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFridge
{
	class MukFridgeFanDataViewModel : ViewModelBase, ICommandListener
	{
		private readonly IThreadNotifier _notifier;
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

		private string _reply;

		public MukFridgeFanDataViewModel(IThreadNotifier notifier, IParameterSetter parameterSetter)
		{
			_notifier = notifier;
			MukFridgeSetParamsVm = new MukFridgeSetParamsViewModel(notifier, parameterSetter);
		}

		public void ReceiveCommand(byte addr, byte code, IList<byte> data)
		{
			if (addr != 0x04) return;
			if (code == 0x03 && data.Count == 29)
			{
				_notifier.Notify(() =>
				{
					FanPwm = (data[3] * 256.0 + data[4]).ToString("f2");

					CondensingPressure = (new DataDoubleTextPresenter(data[6], data[5], 0.1, 2)).PresentAsText();

					IncomingSignals = (new ByteTextPresenter(data[8], true)).PresentAsText();
					OutgoingSignals = (new ByteTextPresenter(data[10], true)).PresentAsText();

					AnalogInput = (new UshortTextPresenter(data[12], data[11], true)).PresentAsText();
					AutomaticModeStage = (new UshortTextPresenter(data[14], data[13], false)).PresentAsText();
					WorkMode = (new UshortTextPresenter(data[16], data[15], false)).PresentAsText();
					Diagnostic1 = (new UshortTextPresenter(data[18], data[17], true)).PresentAsText();
					Diagnostic2 = (new UshortTextPresenter(data[20], data[19], true)).PresentAsText();
					FanSpeed = (new UshortTextPresenter(data[22], data[21], false)).PresentAsText();

					FirmwareBuildNumber = (new DataDoubleTextPresenter(data[24], data[23], 1.0, 0)).PresentAsText();

					Reply = data.ToText();
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


		public MukFridgeSetParamsViewModel MukFridgeSetParamsVm { get; }
	}
}

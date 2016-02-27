using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;

namespace CustomModbusSlave.MicroclimatEs2gApp
{
	class MukFlapDataViewModel : ViewModelBase, ICommandListener {
		private readonly IThreadNotifier _notifier;
		private readonly string _header = "МУК заслонки";

		private string _reply;
		
		private string _flapPosition;
		private string _temperatureAddress1;
		private string _temperatureAddress2;
		private string _incomingSignals;
		private string _outgoingSignals;
		private string _analogInput;
		private string _automaticModeStage;
		private string _diagnostic1;
		private string _diagnostic2;
		private string _diagnostic3;
		private string _diagnostic4;
		private string _emersonDiagnostic;
		private string _emersonTemperature;
		private string _emersonPressure;
		private string _emersonValveSetting;
		private string _firmwareBuildNumber;

		public MukFlapDataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;
		}
		
		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr != 0x02) return;
			if (code == 0x03 && data.Count == 39) {
				_notifier.Notify(() => {
					Reply = data.ToText();
					
					FlapPosition = (data[3] * 256.0 + data[4]).ToString("f2");

					TemperatureAddress1 = (new DataDoubleTextPresenter(data[6], data[5], 0.01, 2)).PresentAsText();
					TemperatureAddress2 = (new DataDoubleTextPresenter(data[8], data[7], 0.01, 2)).PresentAsText();
					
					IncomingSignals = (new ByteTextPresenter(data[10], true)).PresentAsText();
					OutgoingSignals = (new ByteTextPresenter(data[12], true)).PresentAsText();
					
					AnalogInput = (new UshortTextPresenter(data[14], data[13], true)).PresentAsText();
					AutomaticModeStage = (new UshortTextPresenter(data[16], data[15], true)).PresentAsText();

					Diagnostic1 = (new UshortTextPresenter(data[18], data[17], true)).PresentAsText(); // TODO: parse bits
					Diagnostic2 = (new UshortTextPresenter(data[20], data[19], true)).PresentAsText(); // TODO: parse bits
					Diagnostic3 = (new UshortTextPresenter(data[22], data[21], true)).PresentAsText(); // TODO: parse bits
					Diagnostic4 = (new UshortTextPresenter(data[24], data[23], true)).PresentAsText(); // TODO: parse bits

					EmersonDiagnostic = (new UshortTextPresenter(data[26], data[25], true)).PresentAsText(); // TODO: parse bits
					EmersonTemperature = (new DataDoubleTextPresenter(data[28], data[27], 0.01, 2)).PresentAsText();
					EmersonPressure = (new DataDoubleTextPresenter(data[30], data[29], 0.1, 2)).PresentAsText();
					EmersonValveSetting = (new UshortTextPresenter(data[32], data[31], false)).PresentAsText();


					FirmwareBuildNumber = (new UshortTextPresenter(data[34], data[33], false)).PresentAsText();
					// 33 34  35 36  37 38   
				});
			}
		}

		public string FlapPosition {
			get { return _flapPosition; }
			set {
				if (_flapPosition != value) {
					_flapPosition = value;
					RaisePropertyChanged(() => FlapPosition);
				}
			}
		}

		public string TemperatureAddress1 {
			get { return _temperatureAddress1; }
			set {
				if (_temperatureAddress1 != value) {
					_temperatureAddress1 = value;
					RaisePropertyChanged(() => TemperatureAddress1);
				}
			}
		}

		public string TemperatureAddress2
		{
			get { return _temperatureAddress2; }
			set
			{
				if (_temperatureAddress2 != value)
				{
					_temperatureAddress2 = value;
					RaisePropertyChanged(() => TemperatureAddress2);
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

		public string AutomaticModeStage {
			get { return _automaticModeStage; }
			set {
				if (_automaticModeStage != value) {
					_automaticModeStage = value;
					RaisePropertyChanged(() => AutomaticModeStage);
				}
			}
		}

		public string Diagnostic1
		{
			get { return _diagnostic1; }
			set
			{
				if (_diagnostic1 != value)
				{
					_diagnostic1 = value;
					RaisePropertyChanged(() => Diagnostic1);
				}
			}
		}

		public string Diagnostic2
		{
			get { return _diagnostic2; }
			set
			{
				if (_diagnostic2 != value)
				{
					_diagnostic2 = value;
					RaisePropertyChanged(() => Diagnostic2);
				}
			}
		}

		public string Diagnostic3
		{
			get { return _diagnostic3; }
			set
			{
				if (_diagnostic3 != value)
				{
					_diagnostic3 = value;
					RaisePropertyChanged(() => Diagnostic3);
				}
			}
		}

		public string Diagnostic4
		{
			get { return _diagnostic4; }
			set
			{
				if (_diagnostic4 != value)
				{
					_diagnostic4 = value;
					RaisePropertyChanged(() => Diagnostic4);
				}
			}
		}

		public string EmersonDiagnostic
		{
			get { return _emersonDiagnostic; }
			set { if (_emersonDiagnostic != value) { _emersonDiagnostic = value; RaisePropertyChanged(() => EmersonDiagnostic); } }
		}

		public string EmersonTemperature
		{
			get { return _emersonTemperature; }
			set { if (_emersonTemperature != value) { _emersonTemperature = value; RaisePropertyChanged(() => EmersonTemperature); } }
		}

		public string EmersonPressure
		{
			get { return _emersonPressure; }
			set { if (_emersonPressure != value) { _emersonPressure = value; RaisePropertyChanged(() => EmersonPressure); } }
		}

		public string EmersonValveSetting
		{
			get { return _emersonValveSetting; }
			set { if (_emersonValveSetting != value) { _emersonValveSetting = value; RaisePropertyChanged(() => EmersonValveSetting); } }
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
	}
}

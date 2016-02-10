using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;

namespace CustomModbusSlave.MicroclimatEs2gApp
{
	class MukVaporizerFanDataViewModel : ViewModelBase, ICommandListener {
		private readonly IThreadNotifier _notifier;
		private readonly string _header = "МУК вентилятора испарителя";
		private string _fanPwm;
		private string _temperatureAddress1;
		private string _temperatureAddress2;
		private string _incomingSignals;
		private string _outgoingSignals;
		private string _analogInput;
		private string _heatingPwm;

		private string _firmwareBuildNumber;
		private string _reply;
		


		public MukVaporizerFanDataViewModel(IThreadNotifier notifier)
		{
			_notifier = notifier;
		}
		
		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr != 0x03) return; 
			if (code == 0x03 && data.Count == 41)
			{
				_notifier.Notify(() => {
					FanPwm = (data[3] * 256.0 + data[4]).ToString("f2");

					TemperatureAddress1 = (new DataDoubleTextPresenter(data[6], data[5], 1.0, 2)).PresentAsText();
					TemperatureAddress2 = (new DataDoubleTextPresenter(data[8], data[7], 1.0, 2)).PresentAsText();

					IncomingSignals = (new ByteTextPresenter(data[10], true)).PresentAsText();
					OutgoingSignals = (new ByteTextPresenter(data[12], true)).PresentAsText();

					AnalogInput = (new UshortTextPresenter(data[14], data[13], true)).PresentAsText();
					HeatingPwm = (new UshortTextPresenter(data[16], data[15], true)).PresentAsText();
					
					FirmwareBuildNumber = (new DataDoubleTextPresenter(data[34], data[33], 1.0, 0)).PresentAsText();

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

		public string HeatingPwm
		{
			get { return _heatingPwm; }
			set { if (_heatingPwm != value) { _heatingPwm = value; RaisePropertyChanged(() => HeatingPwm); } }
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

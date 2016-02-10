using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;

namespace CustomModbusSlave.MicroclimatEs2gApp
{
	class MukVaporizerFanDataViewModel : ViewModelBase, ICommandListener {
		private readonly IThreadNotifier _notifier;
		private readonly string _header = "МУК вентилятора испарителя";
		private string _flapPosition;
		private string _temperatureAddress1;
		private string _temperatureAddress2;
		private string _incomingSignals;

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
					FlapPosition = (data[3] * 256.0 + data[4]).ToString("f2");
					TemperatureAddress1 = (new DataDoubleTextPresenter(data[5], data[6], 1.0, 2)).PresentAsText();
					TemperatureAddress2 = (new DataDoubleTextPresenter(data[7], data[8], 1.0, 2)).PresentAsText();
					
					FirmwareBuildNumber = (new DataDoubleTextPresenter(data[35], data[36], 1.0, 0)).PresentAsText();

					Reply = data.ToText();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;

namespace CustomModbusSlave.MicroclimatEs2gApp
{
	class MukFlapDataViewModel : ViewModelBase, ICommandListener {
		private readonly IThreadNotifier _notifier;
		private readonly string _header = "МУК заслонки";
		private string _flapPosition;
		private string _temperatureAddress1;
		private string _temperatureAddress2;
		private string _reply;

		public MukFlapDataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;
		}
		
		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr != 0x02) return;
			if (code == 0x03 && data.Count == 39) {
				_notifier.Notify(() => {
					FlapPosition = (data[3] * 256.0 + data[4]).ToString("f2");
					TemperatureAddress1 = (data[5] * 256.0 + data[6]).ToString("f2");
					TemperatureAddress2 = (data[7] * 256.0 + data[8]).ToString("f2");
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

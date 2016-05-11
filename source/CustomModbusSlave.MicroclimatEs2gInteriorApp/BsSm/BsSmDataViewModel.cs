using System.Collections.Generic;
using System.Linq;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm
{
	class BsSmDataViewModel : ViewModelBase, ICommandListener {
		private IBsSmDataCommand32Request _bsSmRequestDataVm;
		private IBsSmDataCommand32Reply _bsSmReplyDataVm;
		private readonly IThreadNotifier _notifier;

		public BsSmDataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;

			//_bsSmRequestDataVm = new BsSmRequestDataViewModel(notifier);
			//_bsSmReplyDataVm = new BsSmReplyDataViewModel(notifier);
		}
		
		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr != 0x08) return;
			if (code == 0x20 && data.Count == 27) { // request
				_notifier.Notify(() => {
					BsSmRequestDataVm = new BsSmDataCommand32RequestBuilderFromCommandPartDataBytes(data.Skip(2).Take(data.Count - 4).ToList()).Build();
					//RequestText = data.ToText();
				});
			}
			else if (code == 0x20 && data.Count == 47) { // request
				_notifier.Notify(() => {
					// TODO:
					//BsSmReplyDataVm = new BsSmDataCommand32RequestBuilderFromCommandPartDataBytes(data.Skip(2).Take(data.Count - 4).ToList()).Build();
					//Reply = data.ToText();
				});
			}
		}

		public IBsSmDataCommand32Request BsSmRequestDataVm
		{
			get
			{
				return _bsSmRequestDataVm;
			}
			set
			{
				if (_bsSmRequestDataVm != value) {
					_bsSmRequestDataVm = value;
					RaisePropertyChanged(()=>BsSmRequestDataVm);
				}
			}
		}


		public IBsSmDataCommand32Reply BsSmReplyDataVm {
			get {
				return _bsSmReplyDataVm;
			}
			set {
				if (_bsSmReplyDataVm != value) {
					_bsSmReplyDataVm = value;
					RaisePropertyChanged(() => BsSmReplyDataVm);
				}
			}
		}
	}
}

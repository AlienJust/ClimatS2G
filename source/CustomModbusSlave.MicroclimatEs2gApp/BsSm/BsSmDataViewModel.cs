using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Common;

namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm
{
	class BsSmDataViewModel : ViewModelBase, ICommandListener {
		private readonly BsSmRequestDataViewModel _bsSmRequestDataVm;
		private readonly BsSmReplyDataViewModel _bsSmReplyDataVm;
		
		public BsSmDataViewModel(IThreadNotifier notifier) {
			_bsSmRequestDataVm = new BsSmRequestDataViewModel(notifier);
			_bsSmReplyDataVm = new BsSmReplyDataViewModel(notifier);
		}
		
		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			_bsSmRequestDataVm.ReceiveCommand(addr, code, data);
			_bsSmReplyDataVm.ReceiveCommand(addr, code, data);
		}

		public BsSmRequestDataViewModel BsSmRequestDataVm {
			get { return _bsSmRequestDataVm; }
		}

		public BsSmReplyDataViewModel BsSmReplyDataVm {
			get { return _bsSmReplyDataVm; }
		}
	}
}

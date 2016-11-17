using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm
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

		public BsSmRequestDataViewModel BsSmRequestDataVm => _bsSmRequestDataVm;

		public BsSmReplyDataViewModel BsSmReplyDataVm => _bsSmReplyDataVm;
	}
}

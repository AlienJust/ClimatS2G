using System;
using System.Collections.Generic;
using System.Linq;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Build;
using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm {
	class BsSmDataViewModel : ViewModelBase {
		private IBsSmAndKsm1DataCommand32Request _bsSmAndKsm1RequestDataVm;
		private IBsSmAndKsm1DataCommand32Reply _bsSmAndKsm1ReplyDataVm;
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IBsSmAndKsm1DataCommand32Request> _cmdListenerBsSmRequest32;
		private readonly ICmdListener<IBsSmAndKsm1DataCommand32Reply> _cmdListenerBsSmReply32;


		public BsSmDataViewModel(IThreadNotifier notifier, ICmdListener<IBsSmAndKsm1DataCommand32Request> cmdListenerBsSmRequest32, ICmdListener<IBsSmAndKsm1DataCommand32Reply> cmdListenerBsSmReply32) {
			_notifier = notifier;

			BsSmAndKsm1RequestDataTextVm = new AnyCommandPartViewModel();
			BsSmAndKsm1ReplyDataTextVm = new AnyCommandPartViewModel();

			_cmdListenerBsSmRequest32 = cmdListenerBsSmRequest32;
			_cmdListenerBsSmReply32 = cmdListenerBsSmReply32;

			_cmdListenerBsSmRequest32.DataReceived += CmdListenerBsSmRequest32OnDataReceived;
			_cmdListenerBsSmReply32.DataReceived += CmdListenerBsSmReply32OnDataReceived;
		}

		private void CmdListenerBsSmRequest32OnDataReceived(IList<byte> bytes, IBsSmAndKsm1DataCommand32Request data) {
			_notifier.Notify(() => {
				BsSmAndKsm1RequestDataVm = data;
				BsSmAndKsm1RequestDataTextVm.Update(bytes);
			});
		}

		private void CmdListenerBsSmReply32OnDataReceived(IList<byte> bytes, IBsSmAndKsm1DataCommand32Reply data) {
			_notifier.Notify(() => {
				BsSmAndKsm1ReplyDataVm = data;
				BsSmAndKsm1ReplyDataTextVm.Update(bytes);
			});
		}

		public IBsSmAndKsm1DataCommand32Request BsSmAndKsm1RequestDataVm {
			get {
				return _bsSmAndKsm1RequestDataVm;
			}
			set {
				if (_bsSmAndKsm1RequestDataVm != value) {
					_bsSmAndKsm1RequestDataVm = value;
					RaisePropertyChanged(() => BsSmAndKsm1RequestDataVm);
				}
			}
		}

		public AnyCommandPartViewModel BsSmAndKsm1RequestDataTextVm { get; }

		public IBsSmAndKsm1DataCommand32Reply BsSmAndKsm1ReplyDataVm {
			get {
				return _bsSmAndKsm1ReplyDataVm;
			}
			set {
				if (_bsSmAndKsm1ReplyDataVm != value) {
					_bsSmAndKsm1ReplyDataVm = value;
					RaisePropertyChanged(() => BsSmAndKsm1ReplyDataVm);
				}
			}
		}

		public AnyCommandPartViewModel BsSmAndKsm1ReplyDataTextVm { get; }
	}
}

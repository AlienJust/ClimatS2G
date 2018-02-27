using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Reply32;
using CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Request32;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm {
	class BsSmDataViewModel : ViewModelBase {
		private readonly IThreadNotifier _notifier;
		private readonly ICmdListener<IBsSmRequest32Data> _cmdListenerBsSmRequest32;
		private readonly ICmdListener<IBsSmReply32Data> _cmdListenerBsSmReply32;
		private IBsSmRequest32Data _bsSmRequest32Data;
		private IBsSmReply32Data _bsSmReply32Data;

		public AnyCommandPartViewModel BsSmRequest32DataBytes { get; }
		public AnyCommandPartViewModel BsSmReply32DataBytes { get; }

		public BsSmDataViewModel(IThreadNotifier notifier, ICmdListener<IBsSmRequest32Data> cmdListenerBsSmRequest32, ICmdListener<IBsSmReply32Data> cmdListenerBsSmReply32) {
			_notifier = notifier;
			_cmdListenerBsSmRequest32 = cmdListenerBsSmRequest32;
			_cmdListenerBsSmReply32 = cmdListenerBsSmReply32;

			BsSmRequest32DataBytes = new AnyCommandPartViewModel();
			BsSmReply32DataBytes = new AnyCommandPartViewModel();

			_cmdListenerBsSmRequest32.DataReceived += CmdListenerBsSmRequest32OnDataReceived;
			_cmdListenerBsSmReply32.DataReceived += CmdListenerBsSmReply32OnDataReceived;
		}

		private void CmdListenerBsSmRequest32OnDataReceived(IList<byte> bytes, IBsSmRequest32Data data) {
			_notifier.Notify(() =>
			{
				BsSmRequest32Data = data;
				BsSmRequest32DataBytes.Update(bytes);
			});
		}

		private void CmdListenerBsSmReply32OnDataReceived(IList<byte> bytes, IBsSmReply32Data data) {
			_notifier.Notify(() =>
			{
				BsSmReply32Data = data;
				BsSmReply32DataBytes.Update(bytes);
			});
		}

		public IBsSmRequest32Data BsSmRequest32Data {
			get => _bsSmRequest32Data;
			set {
				if (value != _bsSmRequest32Data) {
					_bsSmRequest32Data = value;
					RaisePropertyChanged(() => BsSmRequest32Data);
				}
			}
		}

		public IBsSmReply32Data BsSmReply32Data {
			get => _bsSmReply32Data;
			set {
				if (value != _bsSmReply32Data) {
					_bsSmReply32Data = value;
					RaisePropertyChanged(() => BsSmReply32Data);
				}
			}
		}
	}
}

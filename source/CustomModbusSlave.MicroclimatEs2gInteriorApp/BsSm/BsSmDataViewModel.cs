using System.Collections.Generic;
using System.Linq;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.BsSm.Build;
using CustomModbusSlave.MicroclimatEs2gApp.BsSm.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Common;

namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm
{
	class BsSmDataViewModel : ViewModelBase, ICommandListener {
		private IBsSmAndKsm1DataCommand32Request _bsSmAndKsm1RequestDataVm;
		private IBsSmAndKsm1DataCommand32Reply _bsSmAndKsm1ReplyDataVm;
		private readonly IThreadNotifier _notifier;

		public BsSmDataViewModel(IThreadNotifier notifier) {
			_notifier = notifier;

			BsSmAndKsm1RequestDataTextVm = new AnyCommandPartViewModel();
			BsSmAndKsm1ReplyDataTextVm = new AnyCommandPartViewModel();
		}
		
		public void ReceiveCommand(byte addr, byte code, IList<byte> data) {
			if (addr != 0x0A) return;
			if (code == 0x20 && data.Count == 27) { // request KSM1
				_notifier.Notify(() => {
					BsSmAndKsm1RequestDataVm = new BsSmAndKsm1DataCommand32RequestBuilderFromCommandPartDataBytes(data.Skip(2).Take(data.Count - 4).ToList()).Build();
					BsSmAndKsm1RequestDataTextVm.Update(data);
				});
			}
			else if (code == 0x20 && data.Count == 47) { // reply KSM1
				_notifier.Notify(() => {
					// TODO:
					//BsSmAndKsm1ReplyDataVm = new BsSmAndKsm1DataCommand32RequestBuilderFromCommandPartDataBytes(data.Skip(2).Take(data.Count - 4).ToList()).Build();
					//Reply = data.ToText();
					BsSmAndKsm1ReplyDataVm = new BsSmAndKsm1DataCommand32ReplyBuilderFromCommandPartDataBytes(data.Skip(2).Take(data.Count - 4).ToList()).Build();
					BsSmAndKsm1ReplyDataTextVm.Update(data);
				});
			}
		}

		public IBsSmAndKsm1DataCommand32Request BsSmAndKsm1RequestDataVm
		{
			get
			{
				return _bsSmAndKsm1RequestDataVm;
			}
			set
			{
				if (_bsSmAndKsm1RequestDataVm != value) {
					_bsSmAndKsm1RequestDataVm = value;
					RaisePropertyChanged(()=>BsSmAndKsm1RequestDataVm);
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

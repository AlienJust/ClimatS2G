using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Reply32;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.TopContent
{
    class TopContentViewModel : ViewModelBase
    {
        private readonly IThreadNotifier _notifier;
        private readonly ICmdListener<IBsSmReply32Data> _bsSmCmdListener;

        private string _astroTime;

        public TopContentViewModel(IThreadNotifier notifier, ICmdListener<IBsSmReply32Data> bsSmCmdListener)
        {
            _notifier = notifier;
            _bsSmCmdListener = bsSmCmdListener;
            _bsSmCmdListener.DataReceived += BsSmCmdListenerOnDataReceived;
        }


        private void BsSmCmdListenerOnDataReceived(IList<byte> bytes, IBsSmReply32Data data)
        {
            _notifier.Notify(() => AstroTime = data.AstronomicTime.ToString("yyyy.MM.dd HH:mm:ss.fff"));
        }


        public string AstroTime
        {
            get => _astroTime;
            set => SetProp(() => _astroTime != value, () => _astroTime = value, () => AstroTime);
        }
    }
}

using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.TopContent
{
    class TopContentViewModel : ViewModelBase
    {
        private readonly IThreadNotifier _notifier;
        private readonly ICmdListener<IBsSmAndKsm1DataCommand32Reply> _bsSmCmdListener;

        private string _astroTime;

        public TopContentViewModel(IThreadNotifier notifier, ICmdListener<IBsSmAndKsm1DataCommand32Reply> bsSmCmdListener)
        {
            _notifier = notifier;
            _bsSmCmdListener = bsSmCmdListener;
            _bsSmCmdListener.DataReceived += BsSmCmdListenerOnDataReceived;
        }


        private void BsSmCmdListenerOnDataReceived(IList<byte> bytes, IBsSmAndKsm1DataCommand32Reply data)
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

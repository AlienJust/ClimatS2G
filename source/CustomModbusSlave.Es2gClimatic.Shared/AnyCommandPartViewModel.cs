using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;

namespace CustomModbusSlave.Es2gClimatic.Shared
{
    public class AnyCommandPartViewModel : ViewModelBase
    {
        private IList<byte> _bytes;
        private readonly RelayCommand _copyCmd;

        public AnyCommandPartViewModel()
        {
            _copyCmd = new RelayCommand(() =>
            {
                Clipboard.SetDataObject(CommandBytesAsText);
            });
        }

        public void Update(IList<byte> bytes)
        {
            _bytes = bytes;

            RaisePropertyChanged(() => CommandBytesAsText);
        }

        public string CommandBytesAsText => _bytes != null ? _bytes.ToText() : string.Empty;

        public ICommand CopyCmd => _copyCmd;
    }
}
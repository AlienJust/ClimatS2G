using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.SystemDiagnostic
{
    class AutoViewModel : ViewModelBase
    {
        private bool _isOk;

        public AutoViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public bool IsOk
        {
            get => _isOk;
            set
            {
                if (_isOk != value)
                {
                    _isOk = value;
                    RaisePropertyChanged(() => IsOk);
                }
            }
        }
    }
}
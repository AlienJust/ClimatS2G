using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.AppWindow;
using System.Collections.Generic;

namespace CustomModbusSlave.Climatic.Tram.InteriorApp
{
    public class MainContentViewModel : ViewModelBase
    {
        private bool _tabHeadersAreLong;
        private ISharedMainViewModel _mainVm;

        public Dictionary<string, IParameterViewModel> Parameters { get; }

        public IThreadNotifier UiNotifier { get; }

        public MainContentViewModel(Dictionary<string, IParameterViewModel> parameters, IThreadNotifier uiNotifier, ISharedMainViewModel mainVm)
        {
            Parameters = parameters;
            UiNotifier = uiNotifier;
            _tabHeadersAreLong = mainVm.TabHeadersAreLong;
            _mainVm = mainVm;
            _mainVm.PropertyChanged += MainVmPropertyChanged;
        }

        private void MainVmPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TabHeadersAreLong")
            {
                TabHeadersAreLong = _mainVm.TabHeadersAreLong;
            }
        }

        public bool TabHeadersAreLong
        {
            get => _tabHeadersAreLong;
            set
            {
                if (_tabHeadersAreLong != value)
                {
                    _tabHeadersAreLong = value;

                    RaisePropertyChanged(() => TabHeadersAreLong);
                }
            }
        }
    }
}

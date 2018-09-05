using System.Windows;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow {
	public sealed class TabItemViewModel : ViewModelBase {
		private bool _tabHeadersAreLong;
		public string ShortHeader { get; set; }
		public string FullHeader { get; set; }

		public FrameworkElement Content { get; set; }

		public TabItemViewModel() {
			_tabHeadersAreLong = false;
		}

		public bool TabHeadersAreLong {
			get => _tabHeadersAreLong;
			set {
				if (_tabHeadersAreLong != value) {
					_tabHeadersAreLong = value;
					RaisePropertyChanged(() => TabHeadersAreLong);
				}
			}
		}
	}
}

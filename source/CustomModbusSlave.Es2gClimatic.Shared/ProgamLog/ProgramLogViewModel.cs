using System.Collections.ObjectModel;
using System.Windows.Input;
using AlienJust.Support.Loggers.Contracts;
using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.ProgamLog {
	public class ProgramLogViewModel : ViewModelBase, ILogger {
		private readonly IUserInterfaceRoot _userInterfaceRoot;
		private readonly ObservableCollection<ILogLine> _logLines;

		private bool _scrollAutomaticly;
		private bool _isEnabled;
		private ILogLine _selectedLine;

		public ProgramLogViewModel(IUserInterfaceRoot userInterfaceRoot) {
			_userInterfaceRoot = userInterfaceRoot;
			_logLines = new ObservableCollection<ILogLine>();

			ClearLogCmd = new RelayCommand(ClearLog);
			_scrollAutomaticly = true;
			_isEnabled = false;
		}

		private void ClearLog() {
			_logLines.Clear();
		}

		public ObservableCollection<ILogLine> LogLines => _logLines;

		public ICommand ClearLogCmd { get; }

		public bool ScrollAutomaticly {
			get => _scrollAutomaticly;
			set {
				if (_scrollAutomaticly != value) {
					_scrollAutomaticly = value;
					RaisePropertyChanged(() => ScrollAutomaticly);
				}
			}
		}

		public bool IsEnabled {
			get => _isEnabled;
			set {
				if (_isEnabled != value) {
					_isEnabled = value;
					RaisePropertyChanged(() => IsEnabled);
				}
			}
		}


		public void Log(string text) {
			_userInterfaceRoot.Notifier.Notify(() => {
				if (_isEnabled) {
					var logLine = new LogLineSimple(text);
					LogLines.Add(logLine);
					if (ScrollAutomaticly) SelectedLine = logLine;
				}
			});
		}

		public void Log(object obj) {
			Log(obj.ToString());
		}

		public ILogLine SelectedLine {
			get => _selectedLine;
			set {
				if (_selectedLine != value) {
					_selectedLine = value;
					RaisePropertyChanged(() => SelectedLine);
				}
			}
		}
	}
}
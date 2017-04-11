using System.Collections.Generic;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.MukVaporizer.Request16;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.SystemDiagnostic {
	class SystemDiagnosticViewModel : ViewModelBase {
		private readonly IThreadNotifier _uiNotifier;
		private readonly CmdListenerMukVaporizerRequest16 _cmdListenerMukVaporizerRequest16;
		private bool? _isSlave;

		public SystemDiagnosticViewModel(IThreadNotifier uiNotifier, CmdListenerMukVaporizerRequest16 cmdListenerMukVaporizerRequest16) {
			_uiNotifier = uiNotifier;
			_cmdListenerMukVaporizerRequest16 = cmdListenerMukVaporizerRequest16;
			_cmdListenerMukVaporizerRequest16.DataReceived += CmdListenerMukVaporizerRequest16DataReceived;
		}

		private void CmdListenerMukVaporizerRequest16DataReceived(IList<byte> bytes, IRequest16Data data) {
			_uiNotifier.Notify(() => {

				IsSlave = data?.IsSlave;
			});
		}

		public bool? IsSlave {
			get { return _isSlave; }
			set {
				if (_isSlave != value) {
					_isSlave = value;
					RaisePropertyChanged(()=>IsSlave);
				}
			}
		}
	}
}

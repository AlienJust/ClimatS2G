using System.Collections.Generic;
using AlienJust.Support.ModelViewViewModel;
using AlienJust.Support.Text;

namespace CustomModbusSlave.Es2gClimatic.Shared {
	public class AnyCommandPartViewModel : ViewModelBase {
		private IList<byte> _bytes;

		public void Update(IList<byte> bytes) {
			_bytes = bytes;
			RaisePropertyChanged(() => CommandBytesAsText);
		}


		public string CommandBytesAsText => _bytes != null ? _bytes.ToText() : string.Empty;
	}
}
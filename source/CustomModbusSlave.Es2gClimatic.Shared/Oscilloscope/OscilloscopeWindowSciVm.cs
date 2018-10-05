using System;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.AppWindow;

namespace CustomModbusSlave.Es2gClimatic.Shared.Oscilloscope {
	public class OscilloscopeWindowSciVm : ViewModelBase, IClosableVm {
		public void NotifyWindowIsClosed() {
			Console.WriteLine("OscilloscopeWindowSciVm was notified about window is closed");
		}
	}
}

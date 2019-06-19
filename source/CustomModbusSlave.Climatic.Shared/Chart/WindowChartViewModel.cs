using System;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.AppWindow;

namespace CustomModbusSlave.Es2gClimatic.Shared.Chart {
	public class WindowChartViewModel :ViewModelBase, IClosableVm {
		public WindowChartViewModel(ChartViewModel chartVm) {
			ChartVm = chartVm;
		}

		public ChartViewModel ChartVm { get; }
		public void NotifyWindowIsClosed() {
			ChartVm?.NotifyWindowIsClosed();
		}
	}
}

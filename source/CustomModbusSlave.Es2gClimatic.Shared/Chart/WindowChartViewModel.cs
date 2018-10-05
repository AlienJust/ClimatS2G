using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.Es2gClimatic.Shared.Chart;

namespace DrillingRig.ConfigApp.LookedLikeAbb.Chart {
	public class WindowChartViewModel :ViewModelBase {
		public WindowChartViewModel(ChartViewModel chartVm) {
			ChartVm = chartVm;
		}

		public ChartViewModel ChartVm { get; }
	}
}

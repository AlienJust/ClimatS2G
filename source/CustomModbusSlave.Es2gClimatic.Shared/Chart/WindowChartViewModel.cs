using AlienJust.Support.ModelViewViewModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.Chart {
	public class WindowChartViewModel :ViewModelBase {
		public WindowChartViewModel(ChartViewModel chartVm) {
			ChartVm = chartVm;
		}

		public ChartViewModel ChartVm { get; }
	}
}

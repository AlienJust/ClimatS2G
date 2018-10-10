using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Abt.Controls.SciChart.Visuals;
using AlienJust.Adaptation.WindowsPresentation;
using AlienJust.Support.Concurrent.Contracts;
using DrillingRig.ConfigApp.LookedLikeAbb.Chart;
using MahApps.Metro.Controls;

namespace CustomModbusSlave.Es2gClimatic.Shared.Chart {
	public partial class WindowChart : MetroWindow, IUpdatable {
		private readonly IThreadNotifier _uiNotifier;
		private SciChartSurface _sciChartSurface;
		public WindowChart() {
			Console.WriteLine("WindowChart .ctor() called");
			InitializeComponent();
			_uiNotifier = new WpfUiNotifierAsync(Dispatcher);
			Console.WriteLine("WindowChart .ctor() complete");
		}

		public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject {
			Console.WriteLine("WindowChart.FindVisualChildren() called");
			if (depObj != null) {
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
					DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
					if (child is T children) {
						yield return children;
					}

					foreach (T childOfChild in FindVisualChildren<T>(child)) {
						yield return childOfChild;
					}
				}
			}
		}

		private void MetroWindow_Loaded(object sender, RoutedEventArgs e) {
			Console.WriteLine("WindowChart.MetroWindow_Loaded() called");
			var cvm = DataContext as WindowChartViewModel;
			cvm?.ChartVm.SetUpdatable(this);

			foreach (var child in FindVisualChildren<SciChartSurface>(ChartView1)) {
				_sciChartSurface = child;
				break;
			}
			Console.WriteLine("WindowChart.MetroWindow_Loaded() complete");
		}

		public void Update() {
			_uiNotifier.Notify(() => {
				if (CheckBox1.IsChecked.HasValue && CheckBox1.IsChecked.Value) {
					foreach (var child in FindVisualChildren<SciChartSurface>(ChartView1)) {
						child.ZoomExtents();
					}
					_sciChartSurface.ZoomExtents();
				}
			});
		}
	}
}

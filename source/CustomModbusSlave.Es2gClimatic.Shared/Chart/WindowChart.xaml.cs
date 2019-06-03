using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Abt.Controls.SciChart.Visuals;
using DrillingRig.ConfigApp.LookedLikeAbb.Chart;
using MahApps.Metro.Controls;

namespace CustomModbusSlave.Es2gClimatic.Shared.Chart {
	public partial class WindowChart : MetroWindow, IUpdatable {
		private SciChartSurface _sciChartSurface;
		public WindowChart() {
			InitializeComponent();
		}

		public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject {
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
			foreach (var child in FindVisualChildren<SciChartSurface>(ChartView1)) {
				_sciChartSurface = child;
				break;
			}
		}

		public void Update() {
				if (CheckBox1.IsChecked.HasValue && CheckBox1.IsChecked.Value) {
					foreach (var child in FindVisualChildren<SciChartSurface>(ChartView1)) {
						child.ZoomExtents();
					}
					_sciChartSurface.ZoomExtents();
				}
		}
	}
}

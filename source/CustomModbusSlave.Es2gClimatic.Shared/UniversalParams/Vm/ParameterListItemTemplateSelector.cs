using System;
using System.Windows;
using System.Windows.Controls;

namespace ParamControls.Vm {
	public class ParameterListItemTemplateSelector : DataTemplateSelector {
		public DataTemplate DisplayGroupTemplate { get; set; }
		public DataTemplate ChartParameterTemplate { get; set; }
		public DataTemplate CannotDisplayMessageTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			//получаем вызывающий контейнер
			//if (container is FrameworkElement element) {
			if (item is IDisplayGroup) {
				return //element.FindResource("DisplayGroupTemplate") as DataTemplate;
					DisplayGroupTemplate;
			}

			if (item is IChartReadyParameterVm) {
				Console.WriteLine("======= item is IChartReadyParameterVm");
				return //element.FindResource("ChartParameterTemplate") as DataTemplate;
					ChartParameterTemplate;
			}

			return //element.FindResource("CannotDisplayMessageTemplate") as DataTemplate;
				CannotDisplayMessageTemplate;
		}
		//return null;
	}
}
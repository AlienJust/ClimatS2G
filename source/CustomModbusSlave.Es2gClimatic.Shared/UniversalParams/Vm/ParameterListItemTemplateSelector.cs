using System;
using System.Windows;
using System.Windows.Controls;

namespace ParamControls.Vm {
	public class ParameterListItemTemplateSelector : DataTemplateSelector {
		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			//получаем вызывающий контейнер
			if (container is FrameworkElement element) {
				if (item is IDisplayGroup) {
					return element.FindResource("DisplayGroupTemplate") as DataTemplate;
				}

				if (item is IChartReadyParameter) {
					Console.WriteLine("======= item is IChartReadyParameter");
					return element.FindResource("ChartParameterTemplate") as DataTemplate;
				}

				return element.FindResource("CannotDisplayMessageTemplate") as DataTemplate;
			}
			return null;
		}
	}
}
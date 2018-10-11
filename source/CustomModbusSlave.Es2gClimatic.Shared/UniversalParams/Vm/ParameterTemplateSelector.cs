using System.Windows;
using System.Windows.Controls;

namespace ParamControls.Vm {
	public class ParameterListItemTemplateSelector : DataTemplateSelector {
		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			//получаем вызывающий контейнер
			if (container is FrameworkElement element) {
				if (item is IChartReadyParameter) {
					return element.FindResource("ChartParameterTemplate") as DataTemplate;
				}

				if (item is IDisplayGroup) {
					return element.FindResource("DisplayGroupTemplate") as DataTemplate;
				}

				return element.FindResource("CannotDisplayMessageTemplate") as DataTemplate;
			}
			return null;
		}
	}

	public class ChartParameterContentTemplateSelector : DataTemplateSelector {
		private readonly DisplayParameterTemplateSelector _displayParamSelector;
		public ChartParameterContentTemplateSelector() {
			_displayParamSelector = new DisplayParameterTemplateSelector();
		}

		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			return _displayParamSelector.SelectTemplate(item, container);
		}
	}

	public class DisplayParameterTemplateSelector : DataTemplateSelector {
		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			// getting calling container
			if (container is FrameworkElement element) {
				if (item is IDisplayParameter<int>) {
					return element.FindResource("IntegerParameterDisplayTemplate") as DataTemplate;
				}
				return element.FindResource("CannotDisplayMessageTemplate") as DataTemplate;
			}
			return null;
		}
	}
}
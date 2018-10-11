using System.Windows;
using System.Windows.Controls;

namespace ParamControls.Vm {
	public class TemplateSelector : DataTemplateSelector {
		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			//получаем вызывающий контейнер
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
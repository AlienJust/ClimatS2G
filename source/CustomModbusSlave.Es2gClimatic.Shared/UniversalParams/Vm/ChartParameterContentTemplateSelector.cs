using System;
using System.Windows;
using System.Windows.Controls;

namespace ParamControls.Vm {
	public class ChartParameterContentTemplateSelector : DataTemplateSelector {
		public DataTemplate IntegerParameterDisplayTemplate { get; set; }
		public DataTemplate CannotDisplayMessageTemplate { get; set; }

		public ChartParameterContentTemplateSelector() {
			Console.WriteLine("-------------------    ChartParameterContentTemplateSelector .ctor called");
		}

		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			Console.WriteLine("-------------------    SelectTemplate() called");
			// getting calling container
			//if (container is FrameworkElement element) {
			if (item is IDisplayParameter<int>) {
				Console.WriteLine("-------------------    item is IDisplayParameter<int>");
				return IntegerParameterDisplayTemplate;
			}
			Console.WriteLine("-------------------    item is NOT IDisplayParameter<int>");
			return CannotDisplayMessageTemplate;
			//}
			//return null;
		}
	}
}
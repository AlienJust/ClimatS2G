using System.Windows;
using System.Windows.Controls;

namespace ParamControls.Vm {
	public class DisplayParameterTemplateSelector : DataTemplateSelector {
		public DataTemplate IntegerParameterDisplayTemplate { get; set; }
		public DataTemplate CannotDisplayMessageTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			// getting calling container
			//if (container is FrameworkElement element) {
				if (item is IDisplayParameter<int>) {
					return IntegerParameterDisplayTemplate;
				}
				return CannotDisplayMessageTemplate;
			//}
			//return null;
		}
	}
}
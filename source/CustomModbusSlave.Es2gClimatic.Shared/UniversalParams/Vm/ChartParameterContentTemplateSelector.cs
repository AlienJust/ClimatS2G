using System.Windows;
using System.Windows.Controls;
using ParamControls.Vm;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm {
	public class ChartParameterContentTemplateSelector : DataTemplateSelector {
		public DataTemplate IntegerParameterDisplayTemplate { get; set; }
		public DataTemplate StringParameterDisplayTemplate { get; set; }
		public DataTemplate BooleanParameterDisplayTemplate { get; set; }
		public DataTemplate CannotDisplayMessageTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			switch (item) {
				case IDisplayParameter<int> _:
					return IntegerParameterDisplayTemplate;
				case IDisplayParameter<string> _:
					return StringParameterDisplayTemplate;
				case IDisplayParameter<bool> _:
					return BooleanParameterDisplayTemplate;
				default:
					return CannotDisplayMessageTemplate;
			}
		}
	}
}
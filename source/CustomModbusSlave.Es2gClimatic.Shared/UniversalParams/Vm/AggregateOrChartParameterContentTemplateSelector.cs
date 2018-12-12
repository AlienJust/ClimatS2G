using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm {
	public class AggregateOrChartParameterContentTemplateSelector : DataTemplateSelector {
		public DataTemplate IntegerParameterDisplayTemplate { get; set; }
		public DataTemplate StringParameterDisplayTemplate { get; set; }
		public DataTemplate BooleanParameterDisplayTemplate { get; set; }
		
		
		
		public DataTemplate IntegerParameterSettableTemplate { get; set; }
		
		
		
		public DataTemplate StringIntegerDispsetTemplate { get; set; }
		

		
		
		
		public DataTemplate CannotDisplayMessageTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			if (item is IDispsetParameter<int>) return IntegerParameterSettableTemplate;
			
			switch (item) {
				case IDisplayParameter<int> _:
					return IntegerParameterDisplayTemplate;
				
				case IDisplayParameter<string> _:
					if (item is IDispsetParameter<int>) {
						return StringIntegerDispsetTemplate;
					}
					return StringParameterDisplayTemplate;
				
				case IDisplayParameter<bool> _:
					return BooleanParameterDisplayTemplate;
				
				case IDispsetParameter<int> _:
					return IntegerParameterSettableTemplate;
				
				default:
					return CannotDisplayMessageTemplate;
			}
		}
	}
}
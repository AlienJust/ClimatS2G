using System.Windows;
using System.Windows.Controls;

namespace ParamControls.Vm {
	public class ParameterListItemTemplateSelector : DataTemplateSelector {
		public DataTemplate DisplayGroupTemplate { get; set; }
		public DataTemplate ChartParameterTemplate { get; set; }
		public DataTemplate CannotDisplayMessageTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			if (item is IDisplayGroup) {
				return DisplayGroupTemplate;
			}

			if (item is IChartReadyParameterViewModel) {
				return ChartParameterTemplate;
			}

			return CannotDisplayMessageTemplate;
		}
	}
}
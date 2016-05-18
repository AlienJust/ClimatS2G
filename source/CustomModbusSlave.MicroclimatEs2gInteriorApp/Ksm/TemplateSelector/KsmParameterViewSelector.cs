using System.Windows;
using System.Windows.Controls;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm.ViewModel;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm.TemplateSelector {
	class KsmParameterViewSelector : DataTemplateSelector {
		public DataTemplate KsmCommonWritableParameterDataTemplate { get; set; }
		public DataTemplate KsmBitsParameterDataTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			if (item is KsmCommonWritableParameterViewModel) {
				return KsmCommonWritableParameterDataTemplate;
			}
			else if (item is KsmBitsParameterViewModel) {
				return KsmBitsParameterDataTemplate;
			}

			return null;
		}
	}
}

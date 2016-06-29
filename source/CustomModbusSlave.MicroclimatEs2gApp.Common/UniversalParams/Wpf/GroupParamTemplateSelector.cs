using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.UniversalParams.Wpf {
	class GroupParamTemplateSelector : DataTemplateSelector {
		private static readonly Dictionary<string, DataTemplate> DataTemplates = new Dictionary<string, DataTemplate>();
		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			var obj = item as IViewable;
			if (obj != null) {
				return DataTemplates[obj.ViewName];
			}
			return null;
		}

		public  static void RegisterTemplate(string name, DataTemplate template) {
			DataTemplates.Add(name, template);
		}
	}

	public interface IViewable {
		 string ViewName { get; }
	}
}

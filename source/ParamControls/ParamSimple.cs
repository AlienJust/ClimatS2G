using System.Windows;
using System.Windows.Controls;

namespace ParamControls
{
	public class ParamSimple : Control {
		static ParamSimple() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ParamSimple), new FrameworkPropertyMetadata(typeof(ParamSimple)));
		}


		public static DependencyProperty TextProperty = DependencyProperty.Register(
			"Text"
			, typeof(string)
			, typeof(ParamSimple));
		//, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		public string Text {
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		public static DependencyProperty ValueProperty =
			DependencyProperty.Register(
				"Value"
				, typeof(string)
				, typeof(ParamSimple));
		//, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		public string Value {
			get => (string)GetValue(ValueProperty);
			set => SetValue(ValueProperty, value);
		}
	}
}

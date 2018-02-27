using System.Windows;
using System.Windows.Controls;

namespace ParamControls
{
	public class ParamBool : Control {
		static ParamBool() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ParamBool), new FrameworkPropertyMetadata(typeof(ParamBool)));
		}

		public static DependencyProperty TextProperty = DependencyProperty.Register(
			"Text"
			, typeof(string)
			, typeof(ParamBool));
		//, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		public string Text {
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		public static DependencyProperty IsCheckedProperty =
			DependencyProperty.Register(
				"IsChecked"
				, typeof(bool)
				, typeof(ParamBool));
		//, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		public string IsChecked {
			get => (string)GetValue(IsCheckedProperty);
			set => SetValue(IsCheckedProperty, value);
		}
	}
}
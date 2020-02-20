using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ParamControls
{
    public class ParamSimple : Control
    {
        static ParamSimple()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ParamSimple),
                new FrameworkPropertyMetadata(typeof(ParamSimple)));
        }


        public static DependencyProperty TextProperty = DependencyProperty.Register(
            "Text"
            , typeof(string)
            , typeof(ParamSimple));
        //, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value"
                , typeof(string)
                , typeof(ParamSimple));
        //, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Value
        {
            get => (string) GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }


        public static DependencyProperty ValueForegroundProperty =
            DependencyProperty.Register(
                "ValueForeground"
                , typeof(Brush)
                , typeof(ParamSimple));
        //, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Brush ValueForeground
        {
            get => (Brush) GetValue(ValueForegroundProperty);
            set => SetValue(ValueForegroundProperty, value);
        }

        public static DependencyProperty ValueFontWeightProperty =
            DependencyProperty.Register(
                "ValueFontWeight"
                , typeof(FontWeight)
                , typeof(ParamSimple));
        //, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public FontWeight ValueFontWeight
        {
            get => (FontWeight) GetValue(ValueFontWeightProperty);
            set => SetValue(ValueForegroundProperty, value);
        }


        public static DependencyProperty ValueColumnWidthProperty =
            DependencyProperty.Register(
                "ValueColumnWidth"
                , typeof(GridLength)
                , typeof(ParamSimple));
        //, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public GridLength ValueColumnWidth
        {
            get => (GridLength) GetValue(ValueColumnWidthProperty);
            set => SetValue(ValueColumnWidthProperty, value);
        }

        public static DependencyProperty TextColumnWidthProperty =
            DependencyProperty.Register(
                "TextColumnWidth"
                , typeof(GridLength)
                , typeof(ParamSimple));
        //, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public GridLength TextColumnWidth
        {
            get => (GridLength) GetValue(TextColumnWidthProperty);
            set => SetValue(TextColumnWidthProperty, value);
        }
    }
}
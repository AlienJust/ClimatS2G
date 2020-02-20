using System.Windows;
using System.Windows.Controls;

namespace ParamControls
{
    public class ParamRawAndConverted : Control
    {
        static ParamRawAndConverted()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ParamRawAndConverted),
                new FrameworkPropertyMetadata(typeof(ParamRawAndConverted)));
        }


        public static DependencyProperty TextProperty = DependencyProperty.Register(
            "Text"
            , typeof(string)
            , typeof(ParamRawAndConverted));
        //, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static DependencyProperty RawValueProperty =
            DependencyProperty.Register(
                "RawValue"
                , typeof(string)
                , typeof(ParamRawAndConverted));
        //, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string RawValue
        {
            get => (string) GetValue(RawValueProperty);
            set => SetValue(RawValueProperty, value);
        }

        public static DependencyProperty ConvertedValueProperty =
            DependencyProperty.Register(
                "ConvertedValue"
                , typeof(string)
                , typeof(ParamRawAndConverted));
        //, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string ConvertedValue
        {
            get => (string) GetValue(ConvertedValueProperty);
            set => SetValue(ConvertedValueProperty, value);
        }
    }
}
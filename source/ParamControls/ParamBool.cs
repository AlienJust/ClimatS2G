using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ParamControls
{
    public class ParamBool : Control
    {
        static ParamBool()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ParamBool), new FrameworkPropertyMetadata(typeof(ParamBool)));
        }

        public static DependencyProperty TextProperty = DependencyProperty.Register(
            "Text"
            , typeof(string)
            , typeof(ParamBool));
        //, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static DependencyProperty IsCheckedProperty =
            DependencyProperty.Register(
                "IsChecked"
                , typeof(bool)
                , typeof(ParamBool));
        //, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string IsChecked
        {
            get => (string) GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }
    }
    /*
    public class ParamDouble : Control
    {
        static ParamDouble()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ParamDouble), new FrameworkPropertyMetadata(typeof(ParamDouble)));
        }

        #region StringFormat prop
        public static DependencyProperty StringFormatProperty = DependencyProperty.Register(
            "StringFormat"
            , typeof(string)
            , typeof(ParamDouble));
        //, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string StringFormat
        {
            get => (string)GetValue(StringFormatProperty);
            set => SetValue(StringFormatProperty, value);
        }
        #endregion

        #region Value prop
        public static DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value"
            , typeof(double)
            , typeof(ParamDouble));
        //, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        #endregion

        #region MaxmimumValue prop
        public static DependencyProperty MaximumValueProperty = DependencyProperty.Register(
            "MaxmimumValue"
            , typeof(double)
            , typeof(ParamDouble));
        //, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double MaximumValue
        {
            get => (double)GetValue(MaximumValueProperty);
            set => SetValue(MaximumValueProperty, value);
        }
        #endregion

        #region MinimumValue prop
        public static DependencyProperty MiminumValueProperty = DependencyProperty.Register(
            "MiminumValue"
            , typeof(double)
            , typeof(ParamDouble));
        //, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double MiminumValue
        {
            get => (double)GetValue(MiminumValueProperty);
            set => SetValue(MiminumValueProperty, value);
        }
        #endregion


        #region ButtonBackgoround property
        public static DependencyProperty ButtonBackgroundProperty =
            DependencyProperty.Register(
                "ButtonBackground"
                , typeof(Brush)
                , typeof(ParamDouble));
        //, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Brush ButtonBackground
        {
            get => (Brush)GetValue(ButtonBackgroundProperty);
            set => SetValue(ButtonBackgroundProperty, value);
        }
        #endregion

        #region ButtonBackgoround property
        public static DependencyProperty ButtonCommandProperty =
            DependencyProperty.Register(
                "ButtonCommand"
                , typeof(ICommand)
                , typeof(ParamDouble));
        //, new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public ICommand ButtonCommand
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }
        #endregion
    }*/
}
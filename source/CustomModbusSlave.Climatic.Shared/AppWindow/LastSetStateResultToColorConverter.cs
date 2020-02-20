using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public sealed class LastSetStateResultToColorConverter : IValueConverter
    {
        public Color UnknownColor { get; set; }
        public Color UnsuccessColor { get; set; }
        public Color SuccessColor { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var lastValue = (LastSetStateResult)value;
            switch (lastValue)
            {
                case LastSetStateResult.Unknown:
                    return UnknownColor;
                case LastSetStateResult.Success:
                    return SuccessColor;
                case LastSetStateResult.Unsuccess:
                    return UnsuccessColor;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var lastValue = (Color)value;
            if (lastValue == UnknownColor)
                return LastSetStateResult.Unknown;
            if (lastValue == SuccessColor)
                return LastSetStateResult.Success;
            if (lastValue == UnsuccessColor)
                return LastSetStateResult.Unsuccess;
            
            throw new ArgumentOutOfRangeException(nameof(value));
        }
    }
}

using System;
using System.Globalization;
using System.Windows.Data;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03
{
    [ValueConversion(typeof(MukFanEvaporatorWorkstage), typeof(string))]
    public class MukFanEvaporatorWorkstageToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enumedValue = (MukFanEvaporatorWorkstage) value; // TODO: might throw exception?
            return enumedValue.ToText();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // TODO: if needed
            throw new NotImplementedException("implement if needed");
        }
    }
}
﻿using System;
using System.Globalization;
using System.Windows.Data;
using AlienJust.Support.Collections;
using NCalc;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.BytesPairConverters.WpfValueConverters
{
    [ValueConversion(typeof(BytesPair), typeof(string))]
    public class BytesPairToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (targetType != typeof(BytesPair))
                    throw new Exception($"Wrong type, expected {typeof(BytesPair).FullName}");

                var ns = (BytesPair) value; // TODO: might throw exception?
                var expr = new Expression(MathExpression);
                expr.Parameters.Add("b1", ns.First);
                expr.Parameters.Add("b2", ns.Second);
                expr.Parameters.Add("u1hi", ns.HighFirstUnsignedValue);
                expr.Parameters.Add("u1lo", ns.LowFirstUnsignedValue);
                expr.Parameters.Add("s1hi", ns.HighFirstSignedValue);
                expr.Parameters.Add("s1lo", ns.LowFirstSignedValue);

                double result = (double) expr.Evaluate();
                return result.ToString(Format);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public string MathExpression { get; set; }

        public string Format { get; set; }
    }
}
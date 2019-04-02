using System.Windows;
using System.Windows.Controls;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm
{
    public class AggregateOrChartParameterContentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate IntegerParameterDisplayTemplate { get; set; }
        public DataTemplate StringParameterDisplayTemplate { get; set; }
        public DataTemplate BooleanParameterDisplayTemplate { get; set; }


        public DataTemplate IntegerParameterSettableTemplate { get; set; }
        public DataTemplate BooleanParameterSettableTemplate { get; set; }


        public DataTemplate StringIntegerDispsetTemplate { get; set; }
        public DataTemplate IntegerIntegerDispsetTemplate { get; set; }
        public DataTemplate StringDoubleDispsetTemplate { get; set; }


        public DataTemplate CannotDisplayMessageTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is IDisplayAndSettableParameter<int, int>) return IntegerIntegerDispsetTemplate;
            if (item is IDisplayAndSettableParameter<string, int>) return StringIntegerDispsetTemplate;
            if (item is IDisplayAndSettableParameter<string, double>)
            {
                return StringDoubleDispsetTemplate;
            }

            if (item is ISettParameter<int>) return IntegerParameterSettableTemplate;
            if (item is ISettParameter<int?>) return IntegerParameterSettableTemplate;

            if (item is ISettParameter<bool>) return BooleanParameterSettableTemplate;
            if (item is ISettParameter<bool?>) return BooleanParameterSettableTemplate;

            switch (item)
            {
                case IDisplayParameter<int> _:
                    return IntegerParameterDisplayTemplate;

                case IDisplayParameter<string> _:
                    return StringParameterDisplayTemplate;

                case IDisplayParameter<bool> _:
                    return BooleanParameterDisplayTemplate;

                case ISettParameter<int> _:
                    return IntegerParameterSettableTemplate;

                default:
                    return CannotDisplayMessageTemplate;
            }
        }
    }
}
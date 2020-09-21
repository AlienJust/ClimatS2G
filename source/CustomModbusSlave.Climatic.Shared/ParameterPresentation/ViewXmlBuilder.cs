using System.Collections.Generic;
using System.Xml.Linq;

namespace CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation
{
    internal sealed class ViewXmlBuilder
    {
        public static IList<IParameterView> GetViews(XElement parameterElement)
        {
            var resultList = new List<IParameterView>();
            var boolViewElements = parameterElement.Elements("BooleanView");
            foreach (var e in boolViewElements)
            {
                resultList.Add(GetBooleanView(e));
            }

            var numberViewElements = parameterElement.Elements("NumberView");
            foreach (var e in numberViewElements)
            {
                resultList.Add(GetNumberView(e));
            }

            var multiViewElements = parameterElement.Elements("MultiView");
            foreach (var e in multiViewElements)
            {
                resultList.Add(GetMultiView(e));
            }

            return resultList;
        }

        private static IParameterView GetBooleanView(XElement viewElement)
        {
            return new BooleanView(
                viewElement.Attribute("Expression").Value,
                viewElement.Attribute("ResultTrue").Value,
                viewElement.Attribute("ResultFalse").Value,
                viewElement.Attribute("Name").Value);
        }

        private static IParameterView GetNumberView(XElement viewElement)
        {
            return new NumberView(
                viewElement.Attribute("Expression").Value,
                viewElement.Attribute("StringFormat").Value,
                viewElement.Attribute("Name").Value);
        }

        private static IParameterView GetMultiView(XElement viewElement)
        {
            var subElements = GetViews(viewElement);
            return new MultiView(viewElement.Attribute("Expression").Value, subElements, viewElement.Attribute("Name").Value);
        }
    }

    internal sealed class XmlBuilderEvents
    {
        public static IList<IParameterEvent> GetEvents(XElement parameterElement)
        {
            var resultList = new List<IParameterView>();
            var boolViewElements = parameterElement.Elements("BooleanEvent");
            foreach (var e in boolViewElements)
            {
                resultList.Add(GetBooleanEvent(e));
            }
        }

        private static IParameterView GetBooleanEvent(XElement viewElement)
        {
            return new BooleanEvent(
                viewElement.Attribute("Expression").Value,
                viewElement.Attribute("Name").Value, 
                bool.Parse(viewElement.Attribute("InvertCondition").Value),
                (EventLevel)Enum.Parse(typeof(EventLevel), viewElement.Attribute("Level").Value));
        }
    }
}
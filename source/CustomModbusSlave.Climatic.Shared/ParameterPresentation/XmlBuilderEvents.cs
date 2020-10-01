using System.Collections.Generic;
using System.Xml.Linq;

namespace CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation
{
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

            return resultList;
        }

        private static IParameterView GetBooleanEvent(XElement viewElement)
        {
            return new BooleanEvent(
                viewElement.Attribute("Expression").Value,
                viewElement.Attribute("Name").Value, 
                bool.Parse(viewElement.Attribute("InvertIf").Value),
                (EventLevel)Enum.Parse(typeof(EventLevel), viewElement.Attribute("Level").Value));
        }
    }
}
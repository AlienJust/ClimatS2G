using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation
{
    public sealed class ParametersPresenterXmlBuilder
    {
        static public IParametersPresenter BuildParametersPresentationFromXml(string filename)
        {
            var xdoc = XDocument.Load(filename);
            return new ParametersPresenterSimple
            {
                Parameters =
                xdoc.Element("Parameters").Elements("Parameter").Select(xmlTagParameter =>
                    {
                        var customNameXmlAttribute = xmlTagParameter.Attribute("CustomName");

                        return (IParameterDescription)new ParameterDescriptionSimple
                        {
                            Key = xmlTagParameter.Attribute("Value").Value,
                            Identifier = xmlTagParameter.Attribute("ParameterId").Value,
                            CustomName = customNameXmlAttribute?.Value,
                            View = ViewXmlBuilder.GetViews(xmlTagParameter).FirstOrDefault() ?? new DefaultView()
                        };
                    }).ToDictionary(p => p.Key, p => p)
            };
        }
    }
}
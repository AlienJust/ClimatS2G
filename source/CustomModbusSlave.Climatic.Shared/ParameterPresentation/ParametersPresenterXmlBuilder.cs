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
                            Key = xmlTagParameter.Attribute("Key").Value,
                            Identifier = xmlTagParameter.Attribute("Identifier").Value,
                            CustomName = customNameXmlAttribute?.Value,
                            View = ViewXmlBuilder.GetViews(xmlTagParameter).FirstOrDefault() ?? new DefaultView(), // TODO: move away default view, use null here or NOT?
                            Injection = InjectionXmlBuilder.GetInjectionConfiguration(xmlTagParameter)
                        };
                    }).ToDictionary(p => p.Key, p => p)
            };
        }
    }
}
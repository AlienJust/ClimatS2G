using DataAbstractionLevel.Low.PsnConfig.Contracts;
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
                            Identifier = xmlTagParameter.Attribute("ParameterId").Value,
                            CustomName = customNameXmlAttribute?.Value,
                            View = ViewXmlBuilder.GetViews(xmlTagParameter).FirstOrDefault() ?? new DefaultView()
                        };
                    }).ToDictionary(p => p.Key, p => p)
            };
        }
    }


    public sealed class ParametersPresenterXmlSerializer
    {
        private static void AddChildXmlNodesWithParameters(XElement parametersRootElement, IPsnProtocolConfiguration config, bool includeCustomName)
        {
            foreach (var commandPart in config.CommandParts)
            {
                int address = (int)commandPart.Address.DefinedValue;
                int command = (int)commandPart.CommandCode.DefinedValue;
                int signalNumber = 1;
                foreach (var varParam in commandPart.VarParams)
                {
                    if (varParam.Name.StartsWith("#")) continue;
                    
                    var key = "param." + address.ToString("d3") + "." + command.ToString("d3") + "." + signalNumber.ToString("d3");
                    var paramDesc = new ParameterDescriptionSimple { Key = key, CustomName = null, Identifier = varParam.Id.IdentyString, View = null };
                    
                    if (!includeCustomName) parametersRootElement.Add(new XElement("Parameter", new XAttribute("Key", key), new XAttribute("Identifier", varParam.Id.IdentyString)));
                    else parametersRootElement.Add(new XElement("Parameter", new XAttribute("Key", key), new XAttribute("Identifier", varParam.Id.IdentyString), new XAttribute("CustomName", varParam.Name)));
                }
            }
        }

        public static void Serialize(string filename, IPsnProtocolConfiguration config, bool includeCustomName)
        {
            var xDoc = new XDocument();
            var rootElement = new XElement("Parameters");


            AddChildXmlNodesWithParameters(rootElement, config, includeCustomName);

            xDoc.Add(rootElement);
            xDoc.Save(filename);
        }
    }
}
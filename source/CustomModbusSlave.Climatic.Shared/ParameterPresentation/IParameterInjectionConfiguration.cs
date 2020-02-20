using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation
{
    public interface IParameterInjectionConfiguration
    {
        IList<ParameterPreselectedValue> PreselectedValueList {get;}
        int ZeroBasedParameterNumber { get; }
        ushort GetValue(double value);
    }
}
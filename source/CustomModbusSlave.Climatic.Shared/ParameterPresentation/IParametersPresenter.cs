using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation
{
    public interface IParametersPresenter
    {
        Dictionary<string, IParameterDescription> Parameters { get; }
    }
}

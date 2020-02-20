using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation
{
    internal sealed class ParametersPresenterSimple : IParametersPresenter
    {
        public Dictionary<string, IParameterDescription> Parameters { get; set; }
    }
}

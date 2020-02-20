using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm
{
    public interface IAggregateParameterViewModel : IDisplayParameter
    {
        IList<IDisplayParameter> DisplayParameters { get; } //
    }
}
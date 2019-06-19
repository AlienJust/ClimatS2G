using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm
{
    public interface IChartReadyParameterViewModel : IDisplayParameter
    {
        bool IsChecked { get; set; }
        IList<IDisplayParameter> DisplayParameters { get; } //
    }
}
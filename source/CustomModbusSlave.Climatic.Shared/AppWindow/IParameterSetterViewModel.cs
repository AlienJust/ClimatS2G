using CustomModbusSlave.Es2gClimatic.Shared.ParameterPresentation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public interface IParameterSetterViewModel : INotifyPropertyChanged
    {
        IList<ParameterPreselectedValue> CustomValueList { get; }
        double Value { get; set; }

        ICommand SetValue { get; }
    }
}
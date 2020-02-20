using System.ComponentModel;
using System.Windows.Input;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm
{
    public interface ISettParameter<TSet> : IDisplayParameter, INotifyPropertyChanged
    {
        TSet SettValue { get; set; }
        bool IsSettValueFallbackOrUnknown { get; }
        ICommand SetCommand { get; }
    }
}
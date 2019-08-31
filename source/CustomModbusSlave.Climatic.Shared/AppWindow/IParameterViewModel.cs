using System.ComponentModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public interface IParameterViewModel : INotifyPropertyChanged
    {
        string Name { get; }

        string Value { get; set; }
    }
}
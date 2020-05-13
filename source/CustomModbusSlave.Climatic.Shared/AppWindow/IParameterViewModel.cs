using System;
using System.ComponentModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public interface IParameterViewModel : INotifyPropertyChanged, IDisposable
    {
        string Name { get; }

        IParameterGetterViewModel Getter { get; }
        IParameterSetterViewModel Setter { get; }
    }
}
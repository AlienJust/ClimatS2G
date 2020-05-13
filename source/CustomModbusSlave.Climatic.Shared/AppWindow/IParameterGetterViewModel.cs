using System;
using System.ComponentModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public interface IParameterGetterViewModel : INotifyPropertyChanged, IDisposable
    {
        string Value { get; set; }

        bool ValueIsOld { get; }
    }
}
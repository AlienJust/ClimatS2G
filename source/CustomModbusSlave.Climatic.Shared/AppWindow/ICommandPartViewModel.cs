using System;
using System.ComponentModel;

namespace CustomModbusSlave.Es2gClimatic.Shared.AppWindow
{
    public interface ICommandPartViewModel : INotifyPropertyChanged
    {
        string ReceiveTimeText { get; }
        string DataText { get; }
    }
}
using System.Windows;

namespace CustomModbusSlave.Es2gClimatic.Shared
{
    public interface ITabItem
    {
        string ShortHeader { get; }
        string FullHeader { get; }
        FrameworkElement Content { get; }
        void OnWindowClose();
    }
}